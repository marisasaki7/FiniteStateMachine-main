using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class Attack : State
{
    float rotationSpeed = 2.0f;
    AudioSource shoot; 

    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
        : base(_npc, _agent, _anim, _player)
    {
        state = STATE.ATTACK;
        shoot = npc.GetComponent<AudioSource>();
        agent.isStopped = true;
    }

    public override void Enter()
    {
        anim.SetTrigger("isShooting");
        shoot.Play();
        base.Enter();   
    }

    public override void Update()
    {

        Vector3 direction = player.position - npc.transform.position;
        direction.y = 0f;
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        if(!CanAttackPlayer())
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT; 
        }
    }


    public override void Exit()
    {

        anim.ResetTrigger("isShooting");
        shoot.Stop();
        base.Exit();    
    }
}
