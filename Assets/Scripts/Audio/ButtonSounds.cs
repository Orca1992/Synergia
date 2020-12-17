using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public AudioSource mySound;
    public AudioClip soundHover;

   public void SoundHover()
    {
        mySound.PlayOneShot(soundHover);
    }


}
