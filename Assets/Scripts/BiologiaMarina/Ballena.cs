using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballena : MonoBehaviour
{
    public GameObject ballena;
    public Transform Lugar1;

    void Update()
    {
        //buscamos el transform de Lugar1
        Transform Lugar1 = GameObject.Find("Lugar1").transform;
        ballena.GetComponent<Rigidbody>().AddForce(Lugar1.position * 5);
    }

    //buscamos el transform de la posicion de spawn de la ballena

    
}
