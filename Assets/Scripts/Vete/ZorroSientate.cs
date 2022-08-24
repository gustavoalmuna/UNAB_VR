using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZorroSientate : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fox")
        {
            animator.SetBool("Sientate", true);
        }
    }
}
