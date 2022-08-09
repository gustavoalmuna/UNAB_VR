using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STScript : MonoBehaviour
{
    TriggersVideo t;
    // Start is called before the first frame update



    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Auto Hand Player"))
        {

            t.playVideo(1);
        }
    }
} 