using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armado : MonoBehaviour
{
    public Panel panel;
    public TextosUNABOT textos;

    void Start()
    {
        
    }

    public GameObject Piezas;
    public GameObject Armados;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == gameObject.tag)
        {
            Armados.SetActive(true);
            Piezas.SetActive(false);
            panel.Final();
            textos.var_siguiente = textos.var_siguiente +1;
            textos.Burbujas();
            Destroy(gameObject);
        }
    }



}






    