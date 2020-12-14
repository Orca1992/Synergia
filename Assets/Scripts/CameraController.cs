using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;
    public int minXPosition = 0;
    public int maxXPosition = 65;
    public int minZPosition = -50;
    public int maxZPosition = -100;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }

        // Steurung durch WASD oder wennn die Maus den Rand des Spielefensters berührt in dem Fall 5 pixel weg
        //if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        //{
        //    transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        //}
        //if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        //{
        //    transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        //}
        //if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        //{
        //    transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        //}
        //if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        //{
        //    transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        //}
        //Steurung am rand des fenster ausgeschaltet, NERVT BEIM TESTEN
        if (Input.GetKey("w"))
        {
            if (transform.position.z < minZPosition)
            {
                transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World); 
            }
        }
        if (Input.GetKey("s"))
        {
            if (transform.position.z > maxZPosition)
            {
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
            }

        }
        if (Input.GetKey("d"))
        {
            if (transform.position.x < maxXPosition)
            {
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            }
        }
        if (Input.GetKey("a"))
        { 
            if (transform.position.x > minXPosition)
            {
                transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            }
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Camera.main.fieldOfView -= scroll * 100 * scrollSpeed * Time.deltaTime;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minY, maxY);


        
    }
}
