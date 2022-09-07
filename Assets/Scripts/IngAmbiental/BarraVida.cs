using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image BarraDeVida;

    public float vidaActual;

    public float vidaMaxima;

    public GameObject Final;

    // Update is called once per frame
    void Update()
    {
        BarraDeVida.fillAmount = vidaActual / vidaMaxima;

        if (vidaActual >= vidaMaxima)
        {
            Final.SetActive(true);
        }        
    }
}
