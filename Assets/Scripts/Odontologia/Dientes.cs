using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dientes : MonoBehaviour
{
     void Start()
    {
        
    }

    public GameObject diente;
    public GameObject DientesRomper;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Diente")
        {
            diente.SetActive(true);
            Destroy(DientesRomper);
            
        }
    }
}
