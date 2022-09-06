using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basuraBotada : MonoBehaviour
{
    public GameObject[] plantas;
    public BarraVida barraVida;

    //al entrar la basura en el collider 
    private void OnTriggerEnter(Collider other)
    {
        barraVida.vidaActual += 9;
        //activamos una planta aleatoria
        plantas[Random.Range(0, plantas.Length)].SetActive(true);
        Destroy(other.gameObject);
    }
    


}
