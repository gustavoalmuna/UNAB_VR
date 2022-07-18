using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepVibing : MonoBehaviour
{
   public static KeepVibing KVInstance;

    private void Awake()
    {
        KVInstance = this;
    }
  public  void TriggerVibration(AudioClip a,OVRInput.Controller c) {



        OVRHapticsClip vibing = new OVRHapticsClip(a);


        if (c == OVRInput.Controller.LTouch) {

            OVRHaptics.LeftChannel.Preempt(vibing); 
        
        }
        if (c == OVRInput.Controller.RTouch) {

            OVRHaptics.RightChannel.Preempt(vibing); 
        
        }

    }
       public void TriggerVibration(int iteration, int frequency, int fuerza, OVRInput.Controller c) {



        OVRHapticsClip vibing = new OVRHapticsClip();

        for (int i = 0;i<iteration; i++) {

            vibing.WriteSample(i % frequency == 0 ? (byte)fuerza : (byte)0);
        
        }


        if (c == OVRInput.Controller.LTouch) {

            OVRHaptics.LeftChannel.Preempt(vibing); 
        
        }
        if (c == OVRInput.Controller.RTouch) {

            OVRHaptics.RightChannel.Preempt(vibing); 
        
        }

    }

}
