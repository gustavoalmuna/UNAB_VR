using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject pared;
    public bool activ;
    public GameObject[] luces;

    void Start()
    {
        Invoke("Destuir", 10);
        activ = false;
    }

    public void Destuir()
    {
        pared.SetActive(false);        
    }

    public void Activar()
    {
        activ = true;
    }

    void Update()
    {
        //si activ es true, activamos las luces progresivamente
        if (activ)
        {
            for (int i = 0; i < luces.Length; i++)
            {
                luces[i].SetActive(true);
            }
        }
    }
    
}
