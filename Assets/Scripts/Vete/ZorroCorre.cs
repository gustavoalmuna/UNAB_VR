using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZorroCorre : MonoBehaviour
{
    public GameObject Zorro;
    public Transform Spawn;


    private void OnTriggerEnter(Collider other)
    {
        Zorro.SetActive(true);
        Debug.Log("Zorro corre"); 
    }

}
