using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Animations;
public class DecisionsScript : MonoBehaviour
{

    public Animator animator;
    private void Awake()
    {
        GlobalPsico.buena = 0;
         GlobalPsico.mala = 0;

    }
    private void Start()
    {

    
    }



    public void agregarBuena() { GlobalPsico.buena++;animator.SetTrigger("ButtonPressed");Invoke("blackToChoose", 5); }
    public void agregarMala() { GlobalPsico.mala++; animator.SetTrigger("ButtonPressed"); Invoke("blackToChoose", 5); }


    public void blackToChoose() {

        if (GlobalPsico.buena == 1) {

            animator.SetTrigger("isGood");
            GlobalPsico.buena = 0;
        }

    
        else if (GlobalPsico.mala == 1){

            animator.SetTrigger("isBad");
            GlobalPsico.mala = 0;
        }

    }

}
