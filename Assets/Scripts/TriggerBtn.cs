using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBtn : MonoBehaviour
{
    #region Variables UI, anterior y proxima:
    public GameObject nextUI;
    public GameObject prevUI;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region UI anterior activada, UI proxima desactivada:
        prevUI.SetActive(true);
        nextUI.SetActive(false);
        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {

        if (nextUI != null || prevUI != null)
        {
            nextUI.SetActive(true);
            prevUI.SetActive(false);
        }

    }

    public void NextGUI()
    {
        if (nextUI != null || prevUI != null)
        {
            nextUI.SetActive(true);
            prevUI.SetActive(false);
        }
    }
}
