using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControl : EnemyControl
{
  
    public ZombieIdleState idleState;
    public ZombieAttackState attackState;
    public ZombieDeadState deadState;
    public override void Setup(EnemyDataInit enemyDataInit)
    {
        base.Setup(enemyDataInit);
        idleState.parent = this;
        attackState.parent = this;
        deadState.parent = this;
        GotoState(idleState);
        StartCoroutine("LoopDetect");
    }

    IEnumerator LoopDetect()
    {
        WaitForSeconds wait = new WaitForSeconds(0.25f);
        while (hp > 0)
        {
            yield return wait;
            float dis = Vector3.Distance(player_target.position, trans.position);
            if (dis < attackRange)
            {
                if (countAttack >= attackTime)
                {
                    if (CurrentState != attackState)
                    {
                        GotoState(attackState);
                        countAttack = 0;
                    }
                }
            }
            else
            {
                if (CurrentState != idleState)
                    GotoState(idleState);
            }
        }
    }

    public override void Update()
    {
        base.Update();
        countAttack += Time.deltaTime;
    }


    public override void OnDamge(BulletData bulletData)
    {
        hp -= bulletData.damge;

    }
}
