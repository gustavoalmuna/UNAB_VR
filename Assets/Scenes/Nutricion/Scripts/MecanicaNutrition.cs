using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanicaNutrition : MonoBehaviour
{
    int casos=1;
    public GameObject Pizza, Pastas,Pure, ArrozCCarne,nota1,nota2,nota3;
    Vector3 firstPosP, firstPosPas, firstPosPur, firstPosAcc;
    // Start is called before the first frame update
    void Start()
    {
        firstPosAcc = ArrozCCarne.transform.position;
        firstPosP = Pizza.transform.position;
        firstPosPas = Pastas.transform.position;
        firstPosPur = Pure.transform.position;
       
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Equals("grabPlateCarne") && casos == 1)
        {

            casos = 2;
            ArrozCCarne.SetActive(false);
            nota1.SetActive(false);

        }
        else if (other.gameObject.name.Equals("grabPlatePizza")&& casos == 1) {

            Pizza.transform.position = firstPosP;
        } 
        
        else if (other.gameObject.name.Equals("grabPlateSpageti")&& casos == 1) {

            Pastas.transform.position = firstPosPas;
        }  
        else if (other.gameObject.name.Equals("grabPlatePure")&& casos == 1) {

            Pure.transform.position = firstPosPur;
        }
        //Caso 2
        if (other.gameObject.name.Equals("grabPlatePure") && casos == 2)
        {
            nota2.SetActive(false);
            casos = 3;
            Pure.transform.position = firstPosPur;

        }
        else if (other.gameObject.name.Equals("grabPlatePizza")&& casos == 2) {
            
            Pizza.transform.position = firstPosP;
        } 
        
        else if (other.gameObject.name.Equals("grabPlateSpageti")&& casos == 2) {

            Pastas.transform.position = firstPosPas;
        }  
        else if (other.gameObject.name.Equals("grabPlateCarne")&& casos == 2) {

            ArrozCCarne.transform.position = firstPosAcc;
        } //Caso 3
        if (other.gameObject.name.Equals("grabPlatePizza") && casos == 2)
        {
            nota2.SetActive(false);
            casos = 3;
            Pure.transform.position = firstPosPur;

        }
        else if (other.gameObject.name.Equals("grabPlatePure")&& casos == 2) {
            
            Pizza.transform.position = firstPosP;
        } 
        
        else if (other.gameObject.name.Equals("grabPlateSpageti")&& casos == 2) {

            Pastas.transform.position = firstPosPas;
        }  
        else if (other.gameObject.name.Equals("grabPlateCarne")&& casos == 2) {

            ArrozCCarne.transform.position = firstPosAcc;
        }
    }
}
