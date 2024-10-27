using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class IdleBehavior : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private FSMAIController controller;
    private Animator anim;
    private float transitionDelay = 3.0f;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.GetComponentInParent<FSMAIController>();
        agent = controller.agent;
        anim = controller.anim;
        agent.isStopped = true;
        anim.SetTrigger("isIdle");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("CanSeePlayer", controller.CanSeePlayer());
        if(transitionDelay > 0f)
        {
            transitionDelay -= Time.deltaTime;
            return; 
        }

        else
        {
            animator.SetBool("IdletoPatrol", true); 
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim.SetTrigger("isIdle");
        transitionDelay = 3.0f; 
    }
}
