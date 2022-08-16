using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public GameObject human;
    public GameObject Carne;
    //buscamos una animacion para el humano
    public Animator animator;
    public GameObject boton;


    public void carneee()
    {
        Carne.SetActive(false);
        boton.SetActive(true);
    }

    public void Humano()
    {
        human.SetActive(false);
        animator.SetBool("Humano", true);
    }
}
