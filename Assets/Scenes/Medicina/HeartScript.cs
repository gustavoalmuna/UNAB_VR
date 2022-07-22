using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{ 
  public  void palpito() {


        OVRInput.SetControllerVibration(2, 9);

    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
