using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caminarAtril : MonoBehaviour
{
    public GameObject waypoint;
    public GameObject Npc;
    public bool Activo = false;
    public Animator animator;


    //si activo es verdadero el npc se movera a la posicion del waypoint
    void Update()
    {
        if (Activo == true)
        {
            animator.SetBool("caminar", true);
            Npc.transform.position = Vector3.MoveTowards(Npc.transform.position, waypoint.transform.position, Time.deltaTime * 1);
        }
        if (Npc.transform.position == waypoint.transform.position)
        {
            animator.SetBool("caminar", false);
            Activo = false;
        }
    }


    
}
