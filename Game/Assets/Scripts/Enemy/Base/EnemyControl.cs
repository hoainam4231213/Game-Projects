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
    public WeaponControl weaponControl;
    public ConfigEnemyRecord cf;
    public int maxHP;
    public int hp;
    public float attackRange;
    public float attackTime;
    public override void Awake()
    {
        base.Awake();
        trans = transform;
        meshAgent = gameObject.GetComponent<NavMeshAgent>();
        player_target = GameObject.FindGameObjectWithTag("Player").transform;
        weaponControl = GameObject.FindObjectOfType<WeaponControl>();
    }
    public virtual void Setup(EnemyDataInit enemyDataInit)
    {
        cf = enemyDataInit.cf;
        maxHP = enemyDataInit.cf.HP;
        hp = maxHP;
    }

    public override void Update()
    {
        base.Update();
        if (Vector3.Distance(player_target.position, trans.position) <= weaponControl.shootRange)
        {
            weaponControl.AddToListEnemy(trans);
        }
        else
        {
            weaponControl.RemoveTolistEnemy(trans);
        }

        if (hp < 0)
            weaponControl.RemoveTolistEnemy(trans);
    }

    public virtual void OnDamge(BulletData bulletData)
    {

    }

    public void OnDead()
    {
        MissionControl.instance.OnEnemyDead(this);
        weaponControl.RemoveTolistEnemy(trans);
        Destroy(gameObject);
    }
}