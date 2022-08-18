using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotarBasura : MonoBehaviour
{ 
    public Material Verde;
    public Material Rojo;
    public GameObject Hoyo;
    public AudioSource sonido;
    public GameObject Fuego;
    public AudioSource sonidoListo;
    public GameObject CuboBasura;
    public Transform SpawnBasura;

    //si un objeto entra en el collider del hoyo, se cambia el color del material del objeto a rojo
    private void OnTriggerEnter(Collider other)
    {
        //si el objeto que entra tiene el tag "Basura"
        if (other.gameObject.tag == "Basura")
        {
            Destroy(other.gameObject);
            Hoyo.gameObject.GetComponent<Renderer>().material = Rojo;
            sonido.Play();
            Fuego.SetActive(true);
            Invoke("basuraLista",4);
        }
             
    }

    public void basuraLista()
    {
        sonido.Stop();
        Hoyo.gameObject.GetComponent<Renderer>().material = Verde;
        Fuego.SetActive(false);
        sonidoListo.Play();
        //instanciar el cubo de basura en SpawnBasura
        Instantiate(CuboBasura, SpawnBasura.position, SpawnBasura.rotation);                       

    }



}
