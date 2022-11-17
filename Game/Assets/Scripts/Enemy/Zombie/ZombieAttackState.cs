using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ZombieAttackState : FSM_State
{
    public ZombieControl parent;

    public override void OnEnter()
    {
        parent.databinding.Attack = true;
        parent.databinding.AttackIndex = UnityEngine.Random.Range(1, 4);   
    }
    public override void OnAnimationMiddle()
    {
        base.OnAnimationMiddle();
        if (parent.player_target != null)
        {
            Vector3 dir = parent.player_target.position - parent.trans.position;
            dir.Normalize();
            float dis = Vector3.Distance(parent.player_target.position, parent.trans.position);
            float dot = Vector3.Dot(dir, parent.trans.forward);
            if (dot > 0.5f && dis <= parent.attackRange)
            {
                EnemyDamageData data = new EnemyDamageData();
                data.damage = parent.cf.Damage;
                parent.player_target.GetComponent<PlayerControl>().OnDamge(data);
            }
        }
    }

    public override void OnAnimationExit()
    {
        parent.GotoState(parent.idleState);
    }
}
