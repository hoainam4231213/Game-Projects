using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : FSM_System
{
    public Transform trans;


    public float detected_range = 5f;
    [Range(0,1f)]
    public float dot_range;
    private float hp;
    public float HP
    {
        set
        {
            hp = value;
        }
        get
        {
            return hp;
        }
    }
    // Start is called before the first frame update

    public override void Awake()
    {
        trans = transform;
    }

    public virtual void SetUp()
    {

    }

    public override void Start()
    {
        base.Start();
        SetUp();
    }

    public virtual void OnDamge()
    {

    }

    public virtual void OnDetectPlayer(Transform player_trans)
    {

    }

    public void OnDead()
    {
        Destroy(gameObject);
    }


}
