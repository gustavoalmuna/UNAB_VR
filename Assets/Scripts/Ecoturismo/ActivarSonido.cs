using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarSonido : MonoBehaviour
{
    public AudioSource sonido;
    //al entrar al trigger, activa el sonido
    void OnTriggerEnter(Collider other)
    {
        sonido.Play();
    }

}
