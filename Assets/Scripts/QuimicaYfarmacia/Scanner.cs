using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public GameObject Good;
    public GameObject Bad;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Good")
        {
            Good.SetActive(true);
            Bad.SetActive(false);
        }
        if (other.gameObject.tag == "Bad")
        {
            Good.SetActive(false);
            Bad.SetActive(true);            
        }
    }
}
