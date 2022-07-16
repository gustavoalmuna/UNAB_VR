using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Interactable : MonoBehaviour
{
    //buscamos la luz
    public Light light;
    //buscamos bateria
    public GameObject battery;
    //buscamos una animation
    public Animator anim;
    public PlayableDirector director;
    //buscamos un audio source
    public AudioSource audioSource;
    public GameObject fuego;
    public GameObject triptico;
    
    



    // Start is called before the first frame update
    void Start()
    {
        //pausamos la animacion
        anim.enabled = false;
        //stop al audio source
        audioSource.Stop();
        
        
    }

    //si battery entra en el trigger del objeto con el tag "Entra" activamos una animacion y bateria se desactiva
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Entra")
        {
            //desactivamos la luz
            light.enabled = false;
            Destroy(battery);
            director.Play();
            anim.enabled = true;
            //activamos el fuego
            fuego.SetActive(true);
            triptico.SetActive(true);
            

        }
    }
}
