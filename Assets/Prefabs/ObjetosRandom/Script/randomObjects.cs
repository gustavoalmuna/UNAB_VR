using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomObjects : MonoBehaviour
{
    public Animator jinx,kaatana,swrd,gatito;
    
    void Start()
    {
       
    }

    // Update is called once per frame


    public void Jinxed() {

        jinx.Play("getJinxed");
    
    }
    
    public void katana() { kaatana.Play("katana"); }
    
    public void sword() { swrd.Play("SwordDoingMovement"); }
    
    public void michi() { gatito.Play("Take001"); }



}
