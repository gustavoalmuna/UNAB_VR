using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Transporter1 : MonoBehaviour
{
    public string name;



    public void ComebackToMap()
    {
        //cambiamos la scena actual a la escena que queremos
        SceneManager.LoadScene(name);
    }

}
