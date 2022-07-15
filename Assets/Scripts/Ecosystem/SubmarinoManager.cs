using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarinoManager : MonoBehaviour
{
    //public GameObject yoshi;
    public GameObject btnIni;
    public GameObject btnMid;
    public GameObject btnEnd;

    public int btnI = 0;

    private void Start()
    {
        if(btnI == 0)
        {
            btnIni.SetActive(true);
            btnMid.SetActive(false);
            btnEnd.SetActive(false);
        }
    }

    /*
    public void Pressbtn()
    {
        yoshi.SetActive(false);
    }
    */

    public void ShowBtn(int btnIndex)
    {
        switch (btnIndex)
        {
            case 1:
                btnIni.SetActive(false);
                btnMid.SetActive(true);
                btnEnd.SetActive(false);
                break;
            case 2:
                btnIni.SetActive(false);
                btnMid.SetActive(false);
                btnEnd.SetActive(true);
                break;
            default:
                btnIni.SetActive(true);
                btnMid.SetActive(false);
                btnEnd.SetActive(false);
                break;

        }
    }
}