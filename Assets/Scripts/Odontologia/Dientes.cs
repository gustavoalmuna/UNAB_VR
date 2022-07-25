using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dientes : MonoBehaviour
{
    public GameObject[] dientesBuenos;
    public GameObject[] dientesMalos;


    void Start()
    {
        dientesMalos[0].SetActive(true);
        dientesBuenos[0].SetActive(false);

        //si objeto con tag "herramienta" entra en el trigger de diente malo se activa el diente bueno y se desactiva el diente malo
       

        
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Herramienta")
        {
            dientesMalos[0].SetActive(false);
            dientesBuenos[0].SetActive(true);         
        }
    }

    
    

    

}
