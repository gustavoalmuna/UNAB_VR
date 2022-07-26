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
        
    }

    
    //si diente malo sale del trigger, se desactiva y se activa el otro
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "boca")
        {
            dientesMalos[0].SetActive(false);
            dientesBuenos[0].SetActive(true);
        }
    }


      
    

    
    

    

}
