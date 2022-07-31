using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class ButtonIngFuncionality : MonoBehaviour
{
    public Animator animator; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  public   void justBeTruePlease() {

        animator.SetTrigger("HaveDestroyedRocks");
        Debug.Log("FUNCIONAAAA");
    
    }
}
