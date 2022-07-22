using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public int counter;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(counter);

    }

    public void contador() {

       counter++;


        if (counter>5){

            GlobSpace.isReady = true;
        
        
        }



    
    }
}
