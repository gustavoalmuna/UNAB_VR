using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Videos : MonoBehaviour
{

    //buscamos video player
    public VideoPlayer videoPlayer;

    //al apretar el boton activamos el video
    public void activarVideo()
    {
        videoPlayer.Play();
    }

}
