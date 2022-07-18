using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambiarNombre : MonoBehaviour
{
    public SceneChangue sceneChangue;
    public string nombre;
    
    public void Cambiar()
    {
        sceneChangue.nombreEscena = nombre;
    }
    
}
