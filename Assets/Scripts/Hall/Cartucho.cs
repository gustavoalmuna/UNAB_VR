using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartucho : MonoBehaviour
{
    public GameObject cartucho;
    public GameObject Portal;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cartucho")
        {
            Destroy(other.gameObject);            
            cartucho.SetActive(true);
            Portal.SetActive(true);            
        }
    }

}
