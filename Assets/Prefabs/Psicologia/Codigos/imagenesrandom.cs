using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imagenesrandom : MonoBehaviour
{
    public GameObject[] imagenes;

    //hacemos que salga una imagen aleatoria
    public void ImagenRandom()
    {
        Invoke("ImagenRandom2", 3);
    }

    public void ImagenRandom2()
    {
        int random = Random.Range(0, imagenes.Length);
        imagenes[random].SetActive(true);
    }

}
