using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Derecho : MonoBehaviour
{

    public bool malo = false;
    public bool bueno = false;
    public GameObject waypoint;
    public GameObject waypoint2;
    public GameObject Npc;
    public Animator animator;

    void Update()
    {
        if (malo == true)
        {
            Npc.transform.rotation = Quaternion.LookRotation(waypoint.transform.position - Npc.transform.position);
            animator.SetBool("caminar", true);
            //movemos al npc a la posicion del waypoint despues de 3 segundos
            Npc.transform.position = Vector3.MoveTowards(Npc.transform.position, waypoint.transform.position, 1 * Time.deltaTime);

            if (Npc.transform.position == waypoint.transform.position)
            {
                animator.SetBool("caminar", false);
            }


                    
        }
        if (bueno == true)
        {
            //rotamos al npc a la posicion del waypoint 
            Npc.transform.rotation = Quaternion.LookRotation(waypoint2.transform.position - Npc.transform.position);
            animator.SetBool("caminar", true);            
            //movemos al npc a la posicion del waypoint
            Npc.transform.position = Vector3.MoveTowards(Npc.transform.position, waypoint2.transform.position, Time.deltaTime * 1);
            
            if (Npc.transform.position == waypoint2.transform.position)
            {
                animator.SetBool("caminar", false);
            }
            
        }

    }

    public void cambiar_malo()
    {
        malo = true;
    }
    public void cambiar_bueno()
    {
        bueno = true;
    }
}
