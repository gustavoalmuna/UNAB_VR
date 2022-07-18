using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangue : MonoBehaviour
{
    //pedimos el nombre de escena
    public string nombreEscena;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other)
        {
            SceneManager.LoadScene(nombreEscena);
        }
    }
}
