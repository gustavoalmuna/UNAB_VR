using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public bool iAmBall;
    public bool iAmBox;
    public int id;

    public GameObject explodeParticles;
    GameObject instExp;

    public float countDestroy;
    public RandomBox rambox;

    private void Start()
    {
        iAmBox = true;
        rambox = GameObject.Find("RandomBox").GetComponent<RandomBox>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        #region Si soy caja, me destruyo cuando la bola me colisione, despues de X seg:
        if (iAmBox && collision.gameObject.CompareTag("Ball"))
        {
            rambox.idBox = id;
            if(rambox.idBox == rambox.idBall)
            {
                rambox.scorePoints++;
            }

            iAmBox = false;
            if(countDestroy > 0)
            {
                StartCoroutine(CountDown(countDestroy));
            }
            
        }
        #endregion

        #region Si soy bola, me destruyo cuando colisione con la caja o con la caja DestroyBalls:
        if (iAmBall)
        {
            if (collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Trash"))
            {
                rambox.idBall = id;
                iAmBall = false;
                if (countDestroy > 0)
                {
                    StartCoroutine(CountDown(countDestroy));
                }
            }
        }
        #endregion
    }

    IEnumerator CountDown(float d)
    {
        rambox.once = false;
        yield return new WaitForSeconds(d);
        instExp = Instantiate(explodeParticles, transform.position, transform.rotation);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(instExp);
        Destroy(gameObject);
    }

    #region Desactivamos el kinematic del rigidbody de la pelota cuando la tomemos con la mano derecha:
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "RobotHand (R)")
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    #endregion
}
