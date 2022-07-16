using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triptico : MonoBehaviour
{
    //buscamos el objeto 
    public GameObject objeto;

    // el robot al entrar al collider se activa el objeto
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "activado")
        {
            objeto.SetActive(true);
            Debug.Log("activado");
        }
    }
}
