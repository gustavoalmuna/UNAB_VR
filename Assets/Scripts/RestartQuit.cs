using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartQuit : MonoBehaviour
{
    #region Booleanas para determinar que objeto es cual:
    public bool iAmRestart;
    public bool iAmQuit;
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        #region Si el puntero de la camara toca la esfera Jugar y la esfera Salir:
        if (other.CompareTag("PlayerPoint"))
        {
            if (iAmQuit)
            {
                Debug.Log("Quitado");
                Application.Quit();
            }

            if (iAmRestart)
            {
                SceneManager.LoadScene("Educativo_Scene");
            }
        }
        #endregion
    }
}
