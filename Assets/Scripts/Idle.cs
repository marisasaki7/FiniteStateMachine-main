using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) 
        : base(_npc, _agent, _anim, _player)
    {
        state = STATE.IDLE; 
    }

    public override void Enter()
    {
        Debug.Log("Entrando no estado IDLE");
        anim.SetTrigger("isIdle");
        base.Enter(); 
    }

    public override void Update()
    {
        if(CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, anim, player);
            stage = EVENT.EXIT; 
        }


      else if(Random.Range(0, 100) < 10)
        {
            nextState = new Patrol(npc, agent, anim, player);
            stage = EVENT.EXIT; 
        }
    }

    public override void Exit() 
    {

        Debug.Log("Saindo do Estado IDLE");
        anim.ResetTrigger("isIdle");
        base.Exit();    
    }
}
