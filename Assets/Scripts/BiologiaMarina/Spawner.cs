using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Ballena;
    public GameObject[] Spawnpoints;

    //hacemos que la ballena camine aleatoriamente entre los spawnpoints
    void Start()
    {
        int random = Random.Range(0, Spawnpoints.Length);
        Ballena.GetComponent<Rigidbody>().AddForce(Spawnpoints[random].transform.position * 5);
    }



}
