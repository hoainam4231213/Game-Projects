using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ZombieStartState : FSM_State
{
    public ZombieControl parent;
    public override void OnEnter()
    {
        base.OnEnter();
        parent.databinding.Emerge = true;

    }


    public override void OnAnimationExit()
    {
        parent.GotoState(parent.idleState);
    }
}
