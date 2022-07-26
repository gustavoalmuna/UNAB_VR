using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dientes : MonoBehaviour
{
     void Start()
    {
        
    }

    public GameObject diente;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Diente")
        {
            diente.SetActive(true);
            //destruimos el objeto que colisiona con el tag Diente
            Destroy(gameObject);
        }
    }
}
