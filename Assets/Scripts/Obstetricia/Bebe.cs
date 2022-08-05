using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bebe : MonoBehaviour
{
  //si el objeto con el nombre bebe entra en el collider del jugador imprimimos por pantalla que el jugador ha ganado
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Baby")
        {
            Debug.Log("Ganaste");
        }
    }

}
