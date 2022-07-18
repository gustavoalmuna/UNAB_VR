using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;




public class Videos : MonoBehaviour
{
    public ContenedorVid contenedorVid;
    public int index;
    //buscamos el objeto con el video player
    public VideoPlayer videoPlayer;

    
    public void ReproducirVideo()
    {
        //aplicamos el video que queremos al video player
        videoPlayer.clip = contenedorVid.videos[index];
        //reproducimos el video
        videoPlayer.Play();
        
        
    }

}
