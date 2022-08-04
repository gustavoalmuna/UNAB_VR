using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject Personas;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawner", 5, 4);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawner()
    {
        Instantiate(Personas, spawnPoint.position, spawnPoint.rotation);
    }
}
