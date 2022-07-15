using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSubmarine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entrando al Submarino...");
        SceneManager.LoadScene("EcoturismoUnderWater");
    }
}
