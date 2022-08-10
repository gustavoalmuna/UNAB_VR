using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardController : MonoBehaviour
{
    public Text t;
    float otherkcal, otherprot, othercarb, othergra;
    // Start is called before the first frame update
    void Start()
    {
        t = GameObject.Find("txtPropiedades").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        otherkcal = other.GetComponent<ComidaAtributes>().getKcal();
        otherprot = other.GetComponent<ComidaAtributes>().getProt();
        othercarb = other.GetComponent<ComidaAtributes>().getCarb();
        othergra = other.GetComponent<ComidaAtributes>().getGra();
        Debug.Log("Yo soy eiren y esto  es FUNCIONA POR FAVOR TE LO PIDO A");
        Invoke("Imprimir", 4);
    }
    public void Imprimir()
    {

        t.text = "Kcal : " + otherkcal + "\n" + "Proteína : " + "\n" + otherprot + "\n" + "Carbohidratos : " + othercarb + "\n" + "Grasas : " + othergra + "\n";

    }
    private void OnTriggerExit(Collider other)
    {
        t.text = "Introduzca Tarjeta";

    }
}
