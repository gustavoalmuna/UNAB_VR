using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggSript : MonoBehaviour
{
    public GameObject easter,canv;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        easter.SetActive(true);
        canv.SetActive(true);
    }
}
