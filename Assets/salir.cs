using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class salir : MonoBehaviour
{
    public GameObject Salir;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Salir", 120);        
    }

    public void SalirJuego()
    {
        Salir.SetActive(true);
    }


}
