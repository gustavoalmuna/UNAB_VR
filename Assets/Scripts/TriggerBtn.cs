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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        #region si el puntero de la camara toca los botones de las UI, la proxima se activa y anterior se desactiva:
        if (other.CompareTag("PlayerPoint"))
        {
            if(nextUI != null || prevUI != null)
            {
                nextUI.SetActive(true);
                prevUI.SetActive(false);
            }
        }
        #endregion
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
