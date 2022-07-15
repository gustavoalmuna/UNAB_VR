using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CountDownTime : MonoBehaviour
{
    #region tiempo:
    public float timeLapse;
    #endregion

    #region Mostrar Conteo en Box:
    public TextMeshPro timeTxt;
    #endregion

    public Image black;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region Mostrar Tiempo en pantalla:

        timeLapse -= Time.deltaTime;
        TimerUpdate();

        if (timeLapse <= 0)
        {
            timeLapse = 0;
            black.GetComponent<Animator>().SetBool("black", true);
            StartCoroutine(SceneIndex());
        }

        //timeTxt.text = timeLapse.ToString("f0");
        #endregion
    }

    void TimerUpdate()
    {
        int minutes = 0;
        int seconds = 0;
        string timerText = "";

        // no permitimos valores negativos:
        if (timeLapse < 0) timeLapse = 0;

        // armamos los minutos y segundos:
        minutes = (int)timeLapse / 60;
        seconds = (int)timeLapse % 60;

        // armamos visualmente el timer:
        timerText = minutes.ToString("00") + ":" + seconds.ToString("00");

        // mostramos el timer en pantalla:
        timeTxt.text = timerText;
    }

    IEnumerator SceneIndex()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync(0);
    }
}
