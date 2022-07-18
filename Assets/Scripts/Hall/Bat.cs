using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    //hacemos una animacion
    public Animator animator;
    public Animator Robot;
    //buscamos el texto3d que esta en el escenario
    public GameObject texto3d;
    public GameObject stage;
    //buscamos el audiosource
    public AudioSource audioSource;

    private void Start()
    {
        animator.SetBool("Bateria", true);
    }


    //al agarrarlo se activa la animacion
    public void activarAnimacion()
    {
        Robot.enabled = false;
        animator.enabled = true;
        Destroy(texto3d);
        Invoke("Destruir", 2.5f);
        Destroy(audioSource);
        
    }


}
