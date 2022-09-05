using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basuraBotada : MonoBehaviour
{
    public BarraVida barraVida;

    //al entrar la basura en el collider 
    private void OnTriggerEnter(Collider other)
    {
        barraVida.vidaActual += 9;
        
    }
}
