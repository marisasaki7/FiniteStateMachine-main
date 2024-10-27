using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    //estados e eventos

    public enum STATE {IDLE, PATROL, PURSUE, ATTACK, DEAD, RUNAWAY};

    public enum EVENT { ENTER, UPDATE, EXIT};

    //variaveis para maquina de estados

    public STATE state;
    public EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected State nextState; 

    //variavies para condicionais

    protected NavMeshAgent agent;
    float visionDistance = 10.0f;
    float visionAngle = 30.0f;
    float attackDistance = 7.0f;

    //constructor 

    public State(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        npc = _npc; 
        agent = _agent; 
        anim = _anim;
        player = _player;
        stage = EVENT.ENTER; 
    }

    //metodos que serao implementados pelas classes filhas

    public virtual void Enter() { stage = EVENT.UPDATE; }

    public virtual void Update() { stage = EVENT.UPDATE; }

    public virtual void Exit() { stage = EVENT.EXIT; }

    //metodo para processar a maquina de estados

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }

        return this; 
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.position); 
        if (direction.magnitude < visionDistance && angle < visionAngle)
        {
            return true;    
        }

        return false;
    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        if (direction.magnitude < attackDistance)
        {
            return true;
        }

        return false;
    }

    public bool IsPlayerBehind()
    {
        Vector3 direction = npc.transform.position - player.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        if (direction.magnitude < 2f && angle < visionAngle)
        {
            return true;
        }

        return false;
    }



}
    
