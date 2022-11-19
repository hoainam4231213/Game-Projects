using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControl : EnemyControl
{
    public ZombieDatabinding databinding;
    public ZombieIdleState idleState;
    public ZombieAttackState attackState;
    public ZombieDeadState deadState;
    public ZombieStartState startState;

    public override void Setup(EnemyDataInit enemyDataInit)
    {
        base.Setup(enemyDataInit);
        idleState.parent = this;
        attackState.parent = this;
        deadState.parent = this;
        startState.parent = this;
        GotoState(startState);
    }

    //  IEnumerator LoopDetect()
    // {
    //WaitForSeconds wait = new WaitForSeconds(0.25f);
    //  WaitForSeconds waitUntilAttack = new WaitForSeconds(1f);
    //    while (hp > 0)
    //    {
    //yield return wait;
    //float dis = Vector3.Distance(player_target.position, trans.position);
    //      if (dis <= attackRange)
    //    {  
    //      if (CurrentState != attackState)
    //    {
    //      if (countAttack >= attackTime)
    //    {
    //GotoState(attackState);
    //countAttack = 0;
    //}
    //}
    //}
    //          else
    //        {
    //          if (CurrentState != idleState)
    //yield return waitUntilAttack;
    //GotoState(idleState);
    //}
    //}
    //}

    public override void OnDamge(BulletData bulletData)
    {
        base.OnDamge(bulletData);
        hp -= bulletData.damge;
        if (hp <= 0)
        {
            if (CurrentState != deadState)
                GotoState(deadState);
        }
    }
}
