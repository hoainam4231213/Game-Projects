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
    public PlayerControl playerControl;
    public ConfigEnemyRecord cf;
    public Transform anchor;
    public GameUI gameUI;
    protected int maxHP;
    protected int hp;
    public float attackRange;
    public float attackTime;
    private EnemyHubControl enemyHub;
    public override void Awake()
    {
        base.Awake();
        trans = transform;
        meshAgent = gameObject.GetComponent<NavMeshAgent>();
        player_target = GameObject.FindGameObjectWithTag("Player").transform;
        playerControl = GameObject.FindObjectOfType<PlayerControl>();
        gameUI = GameObject.FindObjectOfType<GameUI>();
        meshAgent.enabled = false;
    }
    public virtual void Setup(EnemyDataInit enemyDataInit)
    {
        cf = enemyDataInit.cf;
        maxHP = enemyDataInit.cf.HP;
        hp = maxHP;
        Transform trans_hub = BYPoolManager.instance.Spawn("EnemyHub");
        enemyHub = trans_hub.GetComponent<EnemyHubControl>();
        enemyHub.Init(anchor, gameUI.parentHub);
        enemyHub.UpdateHealth(hp, maxHP);
    }

    public override void Update()
    {
        base.Update();
        if (Vector3.Distance(player_target.position, trans.position) <= playerControl.shootRange)
        {
            playerControl.AddEnemyToList(trans);
        }
        else
        {
            playerControl.RemoveEnemyFromList(trans);
        }

        if (hp <= 0)
            playerControl.RemoveEnemyFromList(trans);
    }

    public virtual void OnDamge(BulletData bulletData)
    {
        enemyHub.UpdateHealth(hp, maxHP);
    }

    public void OnDead()
    {
        BYPoolManager.instance.DeSpawn("EnemyHub", enemyHub.transform);
        MissionControl.instance.OnEnemyDead(this);
        playerControl.RemoveEnemyFromList(trans);
        Destroy(gameObject);
    }
}
