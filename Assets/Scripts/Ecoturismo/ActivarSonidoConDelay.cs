using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarSonidoConDelay : MonoBehaviour
{
    public AudioSource sonido;

    public void activarTodo()
    {
        Invoke("activarSonido", 12);
    }

    void activarSonido()
    {
        sonido.Play();
    }
}
