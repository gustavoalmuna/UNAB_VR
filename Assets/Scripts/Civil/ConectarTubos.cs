using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConectarTubos : MonoBehaviour
{
    void OncollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Tubo")
        {
            Destroy(col.gameObject);
        }
        
    }

}
