using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class KeepVibing : MonoBehaviour
{
    public Animator a;


    private void Start()
    {
        a = GameObject.Find("EscenaMedicina").GetComponent<Animator>();


    }


    public void AnimacionTrue() {

        a.SetTrigger("DesencadenarAnimacion");

    }
}

