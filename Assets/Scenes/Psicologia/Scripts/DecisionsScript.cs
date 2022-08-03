using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionsScript : MonoBehaviour
{
    private void Awake()
    {
        GlobalPsico.buena = 0;
         GlobalPsico.mala = 0;

    }
    public void agregarBuena() { GlobalPsico.buena++; }
    public void agregarMala() { GlobalPsico.mala++; }
}
