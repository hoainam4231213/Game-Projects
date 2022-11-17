using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ZombieIdleState : FSM_State
{
    public ZombieControl parent;
    private float countAttack;
    public override void OnEnter()
    {
        parent.databinding.Speed = 1;
        parent.meshAgent.speed = 2.5f;
        parent.meshAgent.stoppingDistance = parent.attackRange;
        countAttack = 0;
    }

    public override void Update()
    {
        parent.meshAgent.SetDestination(parent.player_target.position);
        float speed = parent.meshAgent.velocity.magnitude / parent.meshAgent.desiredVelocity.magnitude;
        parent.databinding.Speed = speed;
        Vector3 dir = parent.meshAgent.steeringTarget - parent.trans.position;
        if (dir.magnitude > 0.2f)
        {
            dir.Normalize();
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            parent.trans.rotation = Quaternion.Slerp(parent.trans.rotation, q, Time.deltaTime * 360);
        }

        countAttack += Time.deltaTime;
        float dis = Vector3.Distance(parent.player_target.position, parent.trans.position);
        if (dis <= parent.attackRange)
        {
            if (parent.CurrentState != parent.attackState)
            {
                if (countAttack >= parent.attackTime)
                {
                    parent.GotoState(parent.attackState);              
                }
            }
        }
    }

    public override void Exits()
    {
        parent.databinding.Speed = 0;
    }
}
