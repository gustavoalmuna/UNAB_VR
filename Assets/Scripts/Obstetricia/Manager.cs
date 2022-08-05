using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    //buscamos un texto 3d
    public Text texto;
    public Text texto2;
    
    public void Win()
    {
        //activamos el texto
        texto.gameObject.SetActive(true);
    }

    public void Lose()
    {
        //activamos el texto
        texto2.gameObject.SetActive(true);
    }


}
