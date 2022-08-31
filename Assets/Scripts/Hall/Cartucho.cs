using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartucho : MonoBehaviour
{
    public GameObject cartucho;
    
    //instanciar cartucho
    public void InstanciarCartucho()
    {
        Instantiate(cartucho, transform.position, Quaternion.identity);
    }

}
