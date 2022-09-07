using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picar : MonoBehaviour
{
    public GameObject Roca;
    public int vidas = 30;
    public AudioSource sonido;
    //hacemos una lista de minerales
    public GameObject[] minerales;

    private void OnTriggerEnter(Collider other)
    {
        vidas--;
        sonido.Play();
        if (vidas == 0)
        {
            Instantiate(minerales[Random.Range(0, minerales.Length)], Roca.transform.position, Quaternion.identity);
            Destroy(Roca);
            //instanciar un mineral aleatorio en la posicion de la roca
            
            
            

        }
    }
        
    
}
