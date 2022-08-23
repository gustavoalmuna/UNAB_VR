using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Animations;
public class DecisionsScript : MonoBehaviour
{

   public  enum goodORbad {
    
        good,
        bad
    };
    public goodORbad a ;


    public Animator animator;
    private void Awake()
    {
        GlobalPsico.buena = 0;
         GlobalPsico.mala = 0;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (a == goodORbad.good) { agregarBuena(); }

        else { agregarMala(); }
        
    }



    public void agregarBuena() { GlobalPsico.buena++;Debug.Log("A") ; animator.SetTrigger("ButtonPressed");Invoke("blackToChoose", 5); }
    public void agregarMala() { GlobalPsico.mala++; Debug.Log("b"); animator.SetTrigger("ButtonPressed"); Invoke("blackToChoose", 5); }


    public void blackToChoose() {

        if (GlobalPsico.buena >0) {

            animator.SetTrigger("isGood");
            GlobalPsico.buena = 0;
        }

    
        else if (GlobalPsico.mala >0){

            animator.SetTrigger("isBad");
            GlobalPsico.mala = 0;
        }

    }

}
