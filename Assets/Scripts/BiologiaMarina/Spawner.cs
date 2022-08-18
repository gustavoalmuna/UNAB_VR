using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] peces;
    public Transform Spawns;
    

    public void Spawn()
    {
        //spawnear un pez random en una posicion random de la escena
        int randomPeces = Random.Range(0, peces.Length);
        Instantiate(peces[randomPeces], Spawns.position, Spawns.rotation);
        
    }



}
