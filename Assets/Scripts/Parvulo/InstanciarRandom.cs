using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciarRandom : MonoBehaviour
{
    public GameObject[] objetos;
    public GameObject[] spanws;

    void Start()
    {
        InvokeRepeating("Spawnear", 5, 5);
    }
    


    void Spawnear()
    {
        int random = Random.Range(0, objetos.Length);
        int randomSpawn = Random.Range(0, spanws.Length);
        Instantiate(objetos[random], spanws[randomSpawn].transform.position, Quaternion.identity);
    }
}
