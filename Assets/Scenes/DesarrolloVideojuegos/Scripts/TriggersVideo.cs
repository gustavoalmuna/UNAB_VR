using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class TriggersVideo : MonoBehaviour
{

    VideoPlayer v1, v2, v3,v4;


    private void Start()
    {
        v1 = GameObject.Find("VideoCuphead").GetComponent<VideoPlayer>();
         v2 = GameObject.Find("VideoShooter").GetComponent<VideoPlayer>();
         v3 = GameObject.Find("BossesVideo").GetComponent<VideoPlayer>();
        v4 = GameObject.Find("MinecraftVideo").GetComponent<VideoPlayer>();
    }

    public void playVideo(int u) {

        switch (u) {

            case 0:
                v1.Play();
                v2.Pause();
                v3.Pause();
                v4.Pause();
                break;


            case 1:
                v1.Pause();
                v2.Play();
                v3.Pause();
                v4.Pause();
                break;


            case 2:
                v1.Pause();
                v2.Pause();
                v3.Play();
                v4.Pause();
                break;

            case 3:
                v1.Pause();
                v2.Pause();
                v3.Pause();
                v4.Play();

                break;
        
        
        }
    
    
    }
}
