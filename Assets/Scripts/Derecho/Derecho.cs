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
    public caminarAtril caminar;

    void Start()
    {
        Npc = GameObject.Find("Npc");
        animator = GetComponent<Animator>();
    } 



    void Update()
    {
        Npc = GameObject.Find("Npc");
        animator = Npc.GetComponent<Animator>();
        if (malo == true)
        {
            
            Npc.transform.rotation = Quaternion.LookRotation(waypoint.transform.position - Npc.transform.position);
            animator.SetBool("caminar", true);
            Npc.transform.position = Vector3.MoveTowards(Npc.transform.position, waypoint.transform.position, 1 * Time.deltaTime);
            caminar.Activo = true;

            if (Npc.transform.position == waypoint.transform.position)
            {
                animator.SetBool("caminar", false);
                Destroy(Npc);
                malo = false;
            }


                    
        }
        if (bueno == true)
        {
            caminar.Activo = true;
            //rotamos al npc a la posicion del waypoint 
            Npc.transform.rotation = Quaternion.LookRotation(waypoint2.transform.position - Npc.transform.position);
            animator.SetBool("caminar", true);            
            //movemos al npc a la posicion del waypoint
            Npc.transform.position = Vector3.MoveTowards(Npc.transform.position, waypoint2.transform.position, Time.deltaTime * 1);
            
            if (Npc.transform.position == waypoint2.transform.position)
            {
                animator.SetBool("caminar", false);
                Destroy(Npc);
                bueno = false;
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
