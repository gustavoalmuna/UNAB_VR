using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{ OVRGrabbable ovrGrababble;
    
  
    // Start is called before the first frame update
    void Start()
    {
        ovrGrababble = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        KeepVibing.KVInstance.TriggerVibration(4,2,5,ovrGrababble.grabbedBy.GetController); 
    }
}
