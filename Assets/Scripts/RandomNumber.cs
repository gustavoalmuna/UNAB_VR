using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomNumber : MonoBehaviour
{
    #region Numero Aleatorio:
    int number = 0;
    #endregion

    #region tiempo:
    public float timeLapse;
    #endregion

    #region Mostrar Numero Aleatorio:
    public Text textNumber;
    #endregion

    #region Mostrar Conteo en Box:
    public Text textCount;
    #endregion

    #region
    public Text timeTxt;
    #endregion

    #region Conteo Script:
    public CountDownTime timer;
    #endregion

    [Space]

    #region Winner, Looser y Game Panel:
    public Canvas winnerPanel;
    public Canvas looserPanel;
    public Canvas Game;
    #endregion

    [Space]

    public GameObject Cube;
    public Transform padre;

    bool timeActive = false;

    // Start is called before the first frame update
    void Start()
    {
        timer.IniGame = true;
        #region Paneles Winner y Looser desactivados:
        winnerPanel.gameObject.SetActive(false);
        looserPanel.gameObject.SetActive(false);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //instancia la cantidad de elementos que se necesitan:
            CubesInst();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //instancia la cantidad de elementos que se necesitan:
            CubesInstMin();
        }

        #region El numero aleatorio se genera cuando el panel Game este activo:
        if (Game.gameObject.activeSelf && !timeActive)
        {
            Debug.Log("Game Canvas Enabled!");
            Invoke("GenerateNumber", 0f);
            timeActive = true;
        }
        #endregion

        #region Seteamos en la UI el conteo de elementos:
        //textCount.text = box.countNumber.ToString();
        #endregion
        /*
        #region Comprobamos si los elementos son iguales al numero random:
        if (number == box.countNumber && number != 0)
        {
            // muestra mensaje de victoria y desactivamos la UI del juego:
            if(timeLapse > 0)
            {
                Game.gameObject.SetActive(false);
                winnerPanel.gameObject.SetActive(true);
            }
        }
        else
        {
            // muestra mensaje de derrota:
            if(timeLapse == 0)
            {
                // canvas derrota (FUISTE WENO):
                Game.gameObject.SetActive(false);
                looserPanel.gameObject.SetActive(true);
            }
        }
        #endregion
        */
        #region Mostrar Tiempo en pantalla:

        if (Game.gameObject.activeSelf)
        {
            timeLapse -= Time.deltaTime;
        }

        if(timeLapse <= 0)
        {
            timeLapse = 0;
        }

        timeTxt.text = timeLapse.ToString("f0");
        #endregion
    }

    #region Funcion que Genera el numero aleatorio entre 0 y 20:
    void GenerateNumber()
    {
        number = Random.Range(1, 21);

        textNumber.text = number.ToString();
        Debug.Log("Numero Random: " + number);
    }
    #endregion

    void CubesInst()
    {
        GameObject Cubes;
        
        for(int i=0; i < 3; i++)
        {
            if(Cube != null && padre != null)
            {
                Cubes = Instantiate(Cube, padre.position, padre.rotation);
                Debug.Log(Cubes.name);
            }
        }
    }

    void CubesInstMin()
    {
        GameObject Cubes;

        for (int i = 0; i < 4; i++)
        {
            if(Cube != null && padre != null)
            {
                Cubes = Instantiate(Cube, padre.position, padre.rotation);
                Debug.Log(Cubes.name);
            }
        }
    }
}
