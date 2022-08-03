using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HotelBad : MonoBehaviour
{

    public GameObject hotel;
    //si el agente entra en el trigger, se ejecuta el metodo
    void OnTriggerEnter(Collider other)
    {
        //si el objeto que entra es un npc
        if (other.gameObject.tag == "NPC")
        {
            //lo movemos hacia el hotel
            other.gameObject.transform.position = Vector3.MoveTowards(other.gameObject.transform.position, hotel.transform.position, 0.1f);
            
        }
    }

}
