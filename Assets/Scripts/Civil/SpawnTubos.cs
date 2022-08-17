using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTubos : MonoBehaviour
{
    public GameObject tubo;
    public Transform tuboSpawn;

    public void SpawnTubo()
    {
        Instantiate(tubo, tuboSpawn.position, tuboSpawn.rotation);
    }
            

}
