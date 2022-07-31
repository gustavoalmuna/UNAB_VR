using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTScript : MonoBehaviour
{
    TriggersVideo t;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Auto Hand Player")) {

            t.playVideo(2);
        }
    }

}
