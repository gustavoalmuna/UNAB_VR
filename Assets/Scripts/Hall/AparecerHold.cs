using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerHold : MonoBehaviour
{
    //hacemos una funcion que cuando pasen 10 segundos se activen los objetos
    public GameObject bateria;
    public GameObject texto3d;

    void Start()
    {
        Invoke("Activar", 5);
    }

    void Activar()
    {
        bateria.SetActive(true);
        texto3d.SetActive(true);
    }

    
}
