using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picar : MonoBehaviour
{
    public GameObject Roca;
    public int vidas = 30;
    public GameObject amatista;

    private void OnTriggerEnter(Collider other)
    {
        vidas--;
        if (vidas == 0)
        {
            Destroy(Roca);
            //activamos la gravedad a la amatista
            amatista.GetComponent<Rigidbody>().useGravity = true;
        }
    }
        
    
}
