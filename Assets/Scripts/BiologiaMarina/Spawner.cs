using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] peces;

    public void Spawn()
    {
        //spawnear un pez random en la posicion del spawner
        Instantiate(peces[Random.Range(0, peces.Length)], transform.position, Quaternion.identity);
        
    }



}
