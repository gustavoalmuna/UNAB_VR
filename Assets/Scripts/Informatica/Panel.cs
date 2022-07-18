using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
   // public GameObject piezas;
    public Text texto_panel;

    public List<GameObject> armado = new List<GameObject>();
    public List<GameObject> piezas = new List<GameObject>();

    static int conteo;
    public GameObject final;

    public GameObject detectores;

    public TextosUNABOT textos;

    void Start()
    {
        for (int n = 0; n < 7; n++)
        {
            armado[n].SetActive(true);
            piezas[n].SetActive(false);
        }
        final.SetActive(false);
        detectores.SetActive(false);
    }



    private void OnTriggerEnter(Collider other)
    {
        //piezas.SetActive(true);
        //for (int n = 0; n < 7; n++)
        //{
        //    armado[n].SetActive(false);
        //    piezas[n].SetActive(true);
        //}
        //detectores.SetActive(true);
        //texto_panel.text = "...Armado en proceso...";
        //conteo = 0;
        //textos.var_siguiente = 1;
        //textos.Burbujas();
    }

    public void Final()
    {
        conteo = conteo + 1;
        Debug.Log(conteo);
        if (conteo == 7)
        {
            final.SetActive(true);
            texto_panel.text = "Presional para Salir";
        }
        
    }

    public void BotonInicio()
    {
        //piezas.SetActive(true);
        for (int n = 0; n < 7; n++)
        {
            armado[n].SetActive(false);
            piezas[n].SetActive(true);
        }
        detectores.SetActive(true);
        texto_panel.text = "...Armado en proceso...";
        conteo = 0;
        textos.var_siguiente = 1;
        textos.Burbujas();
    }
}
