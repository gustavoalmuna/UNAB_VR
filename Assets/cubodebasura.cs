using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubodebasura : MonoBehaviour
{
    //hacer una lista con 4 materiales 
    public Material[] materiales;

    //hacemos que al instanciar el cubo se le asigne un material aleatorio
    void Start()
    {
        GetComponent<Renderer>().material = materiales[Random.Range(0, materiales.Length)];
    }

}
