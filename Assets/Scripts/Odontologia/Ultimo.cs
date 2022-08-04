using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimo : MonoBehaviour
{

    public GameObject[] dientes;
    public AudioSource audioSource;
   
   //si todos los dientes son destruidos activar audio de fin de juego
    void Update()
    {
        if (dientes.Length == 0)
        {
            audioSource.Play();
        }
    }
}
