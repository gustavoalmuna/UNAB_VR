using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPeces : MonoBehaviour
{
    private void OnTriggerEnter(Collider c){
        Destroy(c.gameObject);
    } 
}
