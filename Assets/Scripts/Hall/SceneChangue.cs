using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangue : MonoBehaviour
{
    //creamos un string con el nombre de la escena que queremos cambiar
    public string nombreEscena;
    //creamos unas objeto
    public GameObject particula;
    

    //metodo para cambiar de escena
    public void particulas()
    {
        //activamos la particula
        particula.SetActive(true);
        //cambiamos la escena despues de 3 segundos
        Invoke("Cambiar", 2);
    }

    //metodo para cambiar de escena
    public void Cambiar()
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
