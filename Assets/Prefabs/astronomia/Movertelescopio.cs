using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movertelescopio : MonoBehaviour
{
    public GameObject telescopio;

    //si entra en el collider con el tag "Mover" vamos rotando el telescopio hasta que salga del collider
    private void OnTriggerEnter(Collider other)
    {
        telescopio.transform.Rotate(0, 0, 1);
    }

    //si sale del collider con el tag "Mover" dejamos de rotar el telescopio
    private void OnTriggerExit(Collider other)
    {
        telescopio.transform.Rotate(0, 0, 0);
    }
    
}
