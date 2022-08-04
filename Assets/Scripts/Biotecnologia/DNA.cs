using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    public GameObject AdnAzul;
    public GameObject ADN;
    public GameObject act;

    private void OnTriggerEnter(Collider other)
    {
       AdnAzul.SetActive(true);
       Destroy(ADN);
       act.SetActive(true);
    }    
}
