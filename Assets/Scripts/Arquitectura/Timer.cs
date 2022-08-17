using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject pared;
    

    void Start()
    {
        Invoke("Destuir", 10);
        
    }

    public void Destuir()
    {
        pared.SetActive(false);        
    }

   

    
    
}
