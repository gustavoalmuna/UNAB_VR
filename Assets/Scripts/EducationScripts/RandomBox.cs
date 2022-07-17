using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomBox : MonoBehaviour
{
    #region Prefabs cajas a Instanciar:
    public GameObject boxRed;
    public GameObject boxBlue;
    public GameObject boxYellow;
    public GameObject boxEmpty;
    #endregion

    #region Prefabs bolas a Instanciar:
    public GameObject ballRed;
    public GameObject ballBlue;
    public GameObject ballYellow;
    public GameObject ballEmpty;
    #endregion

    public GameObject instanceBox;
    public GameObject instanceBall;
    public Transform pointBall;

    int rn = 0;
    int rn2 = 0;
    public bool once = false;

    public int scorePoints;
    public TextMeshPro scoreTxt;

    public int idBall;
    public int idBox;

    public CountDownTime cdt;

    

    // Start is called before the first frame update
    void Start()
    {
        if (instanceBox == null && cdt.timeLapse > 0)
        {
            rn = (int)Random.Range(0f, 4f);
            BoxRandom(rn);
            Debug.Log("RandomNumber: " + rn);
        }

        if (instanceBall == null && cdt.timeLapse > 0)
        {
            rn2 = (int)Random.Range(0f, 4f);
            BallRandom(rn2);
            Debug.Log("RandomNumber: " + rn2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreTxt != null)
        {
            scoreTxt.text = scorePoints.ToString();
        }

        
        if (!once)
        {
            if (instanceBox == null)
            {
                rn = (int)Random.Range(0f, 4f);
                BoxRandom(rn);
                Debug.Log("RandomNumber: " + rn);
            }

            if (instanceBall == null)
            {
                rn2 = (int)Random.Range(0f, 4f);
                BallRandom(rn2);
                Debug.Log("RandomNumber: " + rn2);
            }

            #region Solo testing PC:

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if(instanceBox == null)
                {
                    rn = (int)Random.Range(0f, 4f);
                    BoxRandom(rn);
                    Debug.Log("RandomNumber: " + rn);
                }

                if (instanceBall == null)
                {
                    rn2 = (int)Random.Range(0f, 4f);
                    BallRandom(rn2);
                    Debug.Log("RandomNumber: " + rn2);
                }

            }
            #endregion

        }

    }

    void BoxRandom(int random)
    {
        switch (random)
        {
            case 1:
                BoxInstance(boxRed);
                break;
            case 2:
                BoxInstance(boxBlue);
                break;
            case 3:
                BoxInstance(boxYellow);
                break;
            default:
                BoxInstance(boxEmpty);
                break;
        }
    }

    void BallRandom(int random)
    {
        switch (random)
        {
            case 1:
                BallInstance(ballRed);
                break;
            case 2:
                BallInstance(ballBlue);
                break;
            case 3:
                BallInstance(ballYellow);
                break;
            default:
                BallInstance(ballEmpty);
                break;
        }
    }

    public void BoxInstance(GameObject box)
    {
        instanceBox = Instantiate(box, transform.position, transform.rotation);
        once = true;
    }

    public void BallInstance(GameObject ball)
    {
        instanceBall = Instantiate(ball, pointBall.transform.position, pointBall.transform.rotation);
        once = true;
    }

}
