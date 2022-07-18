using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AnimatorController : MonoBehaviour
{
     Animator animator;

    Animation animati;
    // Start is called before the first frame update
    void Start()
    {
        if (!animator) animator = GetComponent<Animator>();
        if (!animati) animati = GetComponent<Animation>();

        //animati["EarthAnimation"].time = 600;
        //animati["EearthAnimation"].speed = 0.003f;
        //animati["EarthAnimation"].weight = 1.0f;
        //animati.Sample();
    }

    // Update is called once per frame
    void Update()
    {
         Instruction();

    }


    void Instruction()
    {

        if (GlobSpace.isReady ==true)
        {

            StartCoroutine(WaitForAnimationToEnd(animator.GetCurrentAnimatorStateInfo(0).length));
            animator.Play("EarthAnimation");
        }

    }

    IEnumerator WaitForAnimationToEnd(float x = 0)
    {


      


        yield return new WaitForSeconds(x);
        //Aqui comienza las instrucciones

        StartCoroutine("Restart");
            

      

    }

    IEnumerator Restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        yield return new WaitForSeconds(15);
    }



}
