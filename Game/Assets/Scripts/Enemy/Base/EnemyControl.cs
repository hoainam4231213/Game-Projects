using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDataInit
{
    public ConfigEnemyRecord cf;
}
public class EnemyDamageData
{
    public int damage;
}
public class EnemyControl : FSM_System
{
    public NavMeshAgent meshAgent;
    public Transform trans;
    public Transform player_target;
    public int maxHP;
    public int hp;
    public float attackRange;
    public float attackTime;
    public float countAttack;
    public override void Awake()
    {
        base.Awake();
        trans = transform;
        meshAgent = gameObject.GetComponent<NavMeshAgent>();
        player_target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public virtual void Setup(EnemyDataInit enemyDataInit)
    {
        maxHP = enemyDataInit.cf.HP;
        hp = maxHP;
    }

    public virtual void OnDamge(BulletData bulletData)
    {

    }

    public void OnDead()
    {
        MissionControl.instance.OnEnemyDead(this);
        Destroy(gameObject);
    }
}
