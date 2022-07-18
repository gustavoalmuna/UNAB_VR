using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicio : MonoBehaviour
{
   //buscamos un audio
    public AudioSource audio;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke ("activar", 1.5f);
    }
    void activar()
    {
        audio.Play();
    }
}
