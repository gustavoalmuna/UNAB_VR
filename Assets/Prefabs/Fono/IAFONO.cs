using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAFONO : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoint;
    
   


    void Start()
    {
        
      
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
        
        
        
    }

}
