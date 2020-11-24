using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class higlighttest : MonoBehaviour
{

    public Color highlightColor;
    private Material[] ObjectsMaterials;
    private float counter;
    private bool turn = false;
    private bool doCounting = false;
    //der Wert darf nur zwischen 0f - 1f
    public float maxAlpha = 0.8f;

    [SerializeField]
    private GameObject objectToHighlight;


    public HighlightMode mode;
    private HighlightMode oldMode;

    /// <summary>
    /// 1 equals 1 second between alpha 0 and alpha 1
    /// </summary>
    public float speed = 1; 

    public enum HighlightMode
    {
        /// <summary>
        /// Highlight is off
        /// </summary>
        None,
        /// <summary>
        /// Highlight is on
        /// </summary>
        Highlighted,
        /// <summary>
        /// Blinking mode works in dependence on speed
        /// </summary>
        Blinking,
        /// <summary>
        /// Lerps from Zero to Highlighted State in dependence on speed
        /// </summary>
        LerpToHighlighted,
        /// <summary>
        /// Lerps from Zero to Highlighted State in dependence on speed, jumps back to Zero and Repeat
        /// </summary>
        LerpToHighlightedRepeated,
        /// <summary>
        /// Lerps from Highlighted to Zero State in dependence on speed
        /// </summary>
        LerpToZero,
        /// <summary>
        /// Lerps from Highlighted to Zero State in dependence on speed, jumps back to Highlighted and Repeat
        /// </summary>
        LerpToZeroRepeated,

    }


    public void SetMode(HighlightMode modeToSet)
    {
        mode = modeToSet;
    }

    public void ResetCounter()
    {
        counter = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(objectToHighlight != null)
        {
            ObjectsMaterials = objectToHighlight.GetComponent<Renderer>().materials;
        }
        else
        {
            ObjectsMaterials = GetComponent<Renderer>().materials;
        }
        oldMode = HighlightMode.None;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (doCounting)
        {

            if (!turn)
            {
                counter += Time.deltaTime * speed;
                if (counter >= maxAlpha) turn = !turn;
            }
            else
            {
                counter -= Time.deltaTime * speed;
                if (counter <= 0) turn = !turn;
            }

        }


        if (oldMode != mode)
        {

            switch (mode)
            {
                case HighlightMode.None:                         SetToZero();                    break;
                case HighlightMode.Highlighted:                  SetToHighlighted();             break;
                case HighlightMode.Blinking:                     Blinking();                     break;
                case HighlightMode.LerpToHighlighted:            LerpToHighlighted();            break;
                case HighlightMode.LerpToZero:                   LerpToZero();                   break;
                case HighlightMode.LerpToHighlightedRepeated:    LerpToHighlightedRepeated();    break;
                case HighlightMode.LerpToZeroRepeated:           LerpToZeroRepeated();           break;
            }

        }

    }

    private void SetAlpha(float alpha)
    {
        foreach (var mat in ObjectsMaterials)
        {
            mat.color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, alpha);
        }
    }

    private void SetToZero()
    {

        SetAlpha(0f);
        oldMode = mode;
        DisableCounter();

    }

    private void SetToHighlighted()
    {

        SetAlpha(maxAlpha);
        oldMode = mode;
        DisableCounter();
    }

    private void Blinking()
    {
        if (!doCounting) doCounting = true;
        SetAlpha(counter);
    }

    private void LerpToHighlighted()
    {
        if (!doCounting) doCounting = true;
        if (counter < maxAlpha)
        {
            SetAlpha(counter);
        }
        else
        {
            DisableCounter();
            mode = HighlightMode.None;
            SetAlpha(maxAlpha);
            oldMode = mode;
        }
    }
    private void LerpToZero()
    {

        if (!doCounting)
        {
            counter = maxAlpha;
            doCounting = true;
        }
        if (counter > 0)
        {
            SetAlpha(counter);
        }
        else
        {
            DisableCounter();
            mode = HighlightMode.None;
            SetAlpha(0f);
            oldMode = mode;
        }
    }


    private void LerpToHighlightedRepeated()
    {
        if (!doCounting) doCounting = true;
        if (counter < maxAlpha)
        {
            SetAlpha(counter);
        }
        else
        {
            counter = 0f;
            SetAlpha(0f);
        }
    }
    private void LerpToZeroRepeated()
    {

        if (!doCounting)
        {
            counter = maxAlpha;
            doCounting = true;
        }
        if (counter > 0f)
        {
            SetAlpha(counter);
        }
        else
        {
            counter = maxAlpha;
            SetAlpha(maxAlpha);
        }
    }


    private void DisableCounter()
    {
        doCounting = false;
        counter = 0f;
    }

}
