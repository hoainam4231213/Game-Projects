using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ZombieDeadState : FSM_State
{
    public ZombieControl parent;
    public override void OnEnter()
    {
        parent.meshAgent.enabled = false;
        parent.databinding.Dead = true;
        parent.databinding.DeadIndex = UnityEngine.Random.Range(1,5);
    }

    public override void OnAnimationExit()
    {
        parent.OnDead();
    }


}
