using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armado : MonoBehaviour
{

    void Start()
    {
        
    }

    //buscamos el nombre del tag
    public string nombreTag;
    public GameObject Piezas;
    public GameObject Armados;

    


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == nombreTag)
        {
            Armados.SetActive(true);
            Piezas.SetActive(false);
            Destroy(gameObject);
        }
    }



}






    