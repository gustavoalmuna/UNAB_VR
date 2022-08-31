using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinchazo1 : MonoBehaviour
{
    public GameObject texto;
    public GameObject texto2;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Jeringa2")
        {
            texto.SetActive(true);
            texto2.SetActive(false);

        }
    }
}
