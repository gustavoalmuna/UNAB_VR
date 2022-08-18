using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picar : MonoBehaviour
{
    public GameObject Roca;
    public int vidas = 30;
    public GameObject amatista;
    public AudioSource sonido;

    private void OnTriggerEnter(Collider other)
    {
        vidas--;
        sonido.Play();
        if (vidas == 0)
        {
            Destroy(Roca);
            //activamos la gravedad a la amatista
            amatista.GetComponent<Rigidbody>().useGravity = true;
            

        }
    }
        
    
}
