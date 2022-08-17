using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBallena : MonoBehaviour
{

    public GameObject ballena;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Spawn", 4);        
    }

    public void Spawn()
    {
        ballena.SetActive(true);
    }

    
}
