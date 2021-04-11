using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienteScript : MonoBehaviour
{
    public static AmbienteScript AmbienteInstance;
    public AudioSource Audio;
    public AudioClip normalAmbient, tenseAmbient;
    private void Awake()
    {
        if (AmbienteInstance != null && AmbienteInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        AmbienteInstance = this;
        DontDestroyOnLoad(this);
    }
}
