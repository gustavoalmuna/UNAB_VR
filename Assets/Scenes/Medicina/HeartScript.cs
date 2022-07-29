using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    GameObject heart;
    public Animator a;
    private void Start()
    {
       heart= GameObject.Find("heart");
    }
    private void Update()
    {
    }
    public  void palpito() {


        OVRInput.SetControllerVibration(2, 9);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("heart")) {

            collision.gameObject.SetActive(false);
            a.SetTrigger("isBroken");

        }
    }
}
