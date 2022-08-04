using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoint;
    public Animator anim;
   


    void Start()
    {
        
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
    
        if(agent.remainingDistance < 0.1f)
        {
            camina();
        }
    }

    void camina()
    {
        //el agent camine hacia un waypoint aleatorio de la lista
        agent.destination = waypoint[Random.Range(0, waypoint.Length)].position;
        anim.SetBool("caminar", true);
        
        
    }

    
}
