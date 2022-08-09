using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DecisionsScript : MonoBehaviour
{
     public Text casos;
    GameObject cnv;

    private void Awake()
    {
        GlobalPsico.buena = 0;
         GlobalPsico.mala = 0;

    }
    private void Start()
    {
        cnv = GameObject.Find("CanvasWithDebug");
    
    }

    public void nuevoEvento() {
        cnv.SetActive(true);
        
    
    }

    public void agregarBuena() { GlobalPsico.buena++;cnv.SetActive(false); }
    public void agregarMala() { GlobalPsico.mala++; cnv.SetActive(false); }
}
