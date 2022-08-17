using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballena : MonoBehaviour
{
    public GameObject ballena;
    public Transform Lugar1;

    void Update()
    {
        ballena.GetComponent<Rigidbody>().AddForce(Lugar1.position * 2);
    }
    
}
