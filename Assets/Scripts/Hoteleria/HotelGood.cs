using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotelGood : MonoBehaviour
{
    public GameObject emoji;

    //si entra un npc en el trigger, se ejecuta el metodo
    void OnTriggerEnter(Collider other)
    {
        //si el objeto que entra es un npc
        if (other.gameObject.tag == "NPC")
        {
            Destroy(other.gameObject);
            emoji.SetActive(true);
            Invoke("Destruir", 2);            
        }
    }


    public void Destruir()
    {
        emoji.SetActive(false);
    }
}
