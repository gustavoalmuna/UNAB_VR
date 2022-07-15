using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCount : MonoBehaviour
{
    #region Conteo de elementos:
    public int countNumber = 0;
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        #region Si los cubos entran a la caja de madera, suma 1 por cada 1 de ellos:
        if (other.CompareTag("Ball"))
        {
            countNumber++;
            //onceTime = true;
        }
        #endregion
    }
}
