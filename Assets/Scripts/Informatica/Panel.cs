using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
   //hacemos una lista de objetos
    public GameObject[] Armado;
    public GameObject Final;

    //si todos los objetos estan armados, se activa el objeto final
    public void Activar()
    {
        foreach (GameObject obj in Armado)
        {
            if (obj.activeSelf == false)
            {
                return;
            }
        }
        Final.SetActive(true);
    }


    
}
