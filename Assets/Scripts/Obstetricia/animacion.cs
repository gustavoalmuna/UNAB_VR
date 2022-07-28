using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animacion : MonoBehaviour
{
    
    public Animator anim;
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public GameObject obj5;
    public GameObject obj6;
    public GameObject obj7;

    // Start is called before the first frame update
    void Start()
    {
        //activamos el bool de la animacion
        anim.SetBool("Activo", true);
        obj3.SetActive(true);
        obj4.SetActive(true);
        Invoke("activar", 2f);
        obj5.SetActive(true);
        Invoke("activar2", 3f);
    }

    void activar()
    {
        obj1.SetActive(true);
        obj2.SetActive(true);
        obj6.SetActive(true);
                
    }

    void activar2()
    {
        obj7.SetActive(true);
    }

    
}
