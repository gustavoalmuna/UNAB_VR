using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverAtril : MonoBehaviour
{
    public GameObject Npc;
    public Animator animator;
    public GameObject atril;
    public Derecho derecho;
    public GameObject game;



    void Update()
    {
        Npc = GameObject.Find("Npc");
        animator = Npc.GetComponent<Animator>();
        
        if (derecho.malo==false && derecho.bueno==false)
        {
            Npc.transform.position = Vector3.MoveTowards(Npc.transform.position, atril.transform.position, 1 * Time.deltaTime);
            animator.SetBool("caminar", true);
            if (Npc.transform.position == atril.transform.position)
            { 
                animator.SetBool("caminar", false);
                game.SetActive(true);
            }
        }
        
    }
}
