using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suerte : MonoBehaviour
{
    public GameObject Ganaste;
    public GameObject Perdiste;
    public GameObject Tension;
    public GameObject Maletin;
    public Transform[] SpawnMaletin;

    public void TomarNumero()
    {
        //tomamos un numero aleatorio entre 0 y 100
        int numero = Random.Range(0, 100);
        Tension.SetActive(true);

        //si el numero es mayor a 50
        if (numero < 50)
        {
            Invoke("Ganar", 4);
            Instantiate(Maletin, SpawnMaletin[Random.Range(0, SpawnMaletin.Length)].position, SpawnMaletin[Random.Range(0, SpawnMaletin.Length)].rotation);            
        }
        else
        {
            Invoke("Perder", 4);
            
        }
    }

    public void Ganar()
    {
        Ganaste.SetActive(true);
        Tension.SetActive(false);
        Perdiste.SetActive(false);
    }

    public void Perder()
    {
        Perdiste.SetActive(true);
        Tension.SetActive(false);
        Ganaste.SetActive(false);
    }


    public void Safe()
    {
        Perdiste.SetActive(false);
        Ganaste.SetActive(false);
    }

    


}
