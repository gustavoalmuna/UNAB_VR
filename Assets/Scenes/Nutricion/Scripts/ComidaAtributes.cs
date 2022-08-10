using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ComidaAtributes : MonoBehaviour
{enum platos {
    
        pez,pizza,tallarines,papas,carne,pure
    
    }
    public int tipPla;
    float kcal,prot,carb,gra;
    Text t;
    platos p;
    public float getKcal() {

        return kcal;

    } public float getProt() {

        return prot;

    } public float getCarb()
    {

        return kcal;

    }
     public float getGra() {

        return gra;

    }
    // Start is called before the first frame update
    void Start()
    {
        t = GameObject.Find("txtPropiedades").GetComponent<Text>();
        switch (tipPla) {
         
            case 1:

                p = platos.carne;
                break;
            case 2:
                p = platos.papas;
                break;
            case 3:
                p = platos.pez;
                break;
            case 4:
                p = platos.pizza;
                break; 
            case 5:
                p = platos.pure;
                break; 
            case 6:
                p = platos.tallarines;
                break;
        }
        switch (p) {

            case platos.papas:
                kcal = 137;
                prot = 1.64f;
                carb = 12.44f;
                gra =  9.37f;
                break;
            case platos.carne:
                kcal = 135;
                prot = 21.91f;
                carb = 0;
                gra = 4.62f;
                break;
            case platos.pez:
                kcal = 146;
                prot = 21.62f;
                carb = 0;
                gra = 5.39f;
                break;
            case platos.tallarines:
                kcal = 329;
                prot = 13.06f;
                carb = 33.33f;
                gra = 13.06f;
                break;
            case platos.pure:
                kcal = 237;
                prot = 3.93f;
                carb = 35.22f;
                gra = 8.86f;
                break;          
               
            
            case platos.pizza:
                kcal = 298;
                prot = 13.32f;
                carb = 33.98f;
                gra = 12.13f;
            break;
        
        
        }
    

    }
    
}
