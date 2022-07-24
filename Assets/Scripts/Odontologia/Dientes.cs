using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dientes : MonoBehaviour
{
    public GameObject[] dientesBuenos;
    public GameObject[] dientesMalos;

    //hacemos una funcion para que active dientes malos random cada determinado tiempo
    public void activarDientesMalos()
    {
        int random = Random.Range(0, dientesMalos.Length);
        dientesMalos[random].SetActive(true);
    }
}
