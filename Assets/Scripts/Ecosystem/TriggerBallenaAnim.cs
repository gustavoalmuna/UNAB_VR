using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TriggerBallenaAnim : MonoBehaviour
{
    public GameObject canvas;
    public Animator ballena;
    public Animator fish1;
    public Animator fish2;
    public Light SpotlightRed;
    public Text countTxt;
    float count = 11f;

    private void Start()
    {
        canvas.SetActive(false);
    }

    private void Update()
    {
        
        if (canvas.activeSelf == true)
        {
            count -= Time.deltaTime;
            Timer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && gameObject.name == "TriggerBallenaMov")
        {
            ballena.SetBool("StartBallena", true);
            fish1.SetBool("Fish1", true);

            Debug.Log("Anim Ballena 01 Playing...");
            SpotlightRed.color = Color.red;

            canvas.SetActive(true);
            
        }

        if (other.gameObject.CompareTag("Player") && gameObject.name == "Trigger2")
        {
            ballena.SetBool("MidBallena", true);
            fish2.SetBool("Fish2", true);
            Debug.Log("Anim Ballena 02 Playing...");
            SpotlightRed.color = Color.red;

            canvas.SetActive(true);
        }

        if (other.gameObject.CompareTag("Player") && gameObject.name == "Trigger3")
        {
            
            Debug.Log("Salir escenario...");
            SceneManager.LoadScene(0);
        }
    }

    public void LightDeactivated()
    {
        SpotlightRed.color = Color.HSVToRGB(0.40f,0.23f,1f);
        canvas.SetActive(false);
    }

    void Timer()
    {
        int conteo = 0;
        string mostrarConteo = "";

        if (count <= 0)
        {
            count = 0;
            SceneManager.LoadScene(0);
        }

        conteo = (int)count;
        mostrarConteo = conteo.ToString("00");
        countTxt.text = mostrarConteo;

    }
}
