using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class MiniCameraSaveLocations : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject pl;
    GameObject rtex;
    float a;
    void Start()
    {
        pl = GameObject.Find("Nave");

        rtex = GameObject.Find("pantalla");
        
    }

    // Update is called once per frame
    void Update()
    {
        a = transform.position.y;


        if (a > 6) {


            Destroy(this.gameObject);
            rtex.SetActive(false);
        }

        
    }

  

}
