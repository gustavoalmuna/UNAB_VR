using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinchazo : MonoBehaviour
{
    public GameObject texto;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Jeringa")
        {
            texto.SetActive(true);

        }
    }
}
