using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData
{
    public Vector3 dir;
    public float force;
    public int damge;
    
}

public class BulletWeapon : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Transform trans;
    public BulletData data;
    private Vector3 force;
    public LayerMask mask;
    public string pool_name;
    public string pool_nameImpact;
    public string pool_nameBloodImpact;
    private Transform trans_impact;
    public float timeFly;
    private bool isFly;

    // Start is called before the first frame update
    private void Awake()
    {
        trans = transform;
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    public void Setup(BulletData bulletData)
    {
        this.data = bulletData;
        trans.forward = bulletData.dir;
        force = bulletData.dir * bulletData.force;
        rigidbody.AddForce(force);
    }

    public void OnSpawned()
    {
        isFly = true;
        StartCoroutine("DelayTimeFly");
    }

    public void OnDeSpawned()
    {
        rigidbody.velocity = Vector3.zero;
    }

    IEnumerator DelayTimeFly()
    {
        yield return new WaitForSeconds(timeFly);
        BYPoolManager.instance.DeSpawn(pool_name, trans);
    }
    // Update is called once per frame
    void Update()
    {
        if(isFly)
        {
            RaycastHit hitInfo;
            if(Physics.Raycast(trans.position, trans.forward, out hitInfo, 0.5f, mask))
            {
                isFly = false;

                

                BYPoolManager.instance.DeSpawn(pool_name, trans);
                EnemyControl enemyControl = hitInfo.collider.gameObject.GetComponent<EnemyControl>();
                if (enemyControl != null)
                {
                    enemyControl.OnDamge(data);
                    trans_impact = BYPoolManager.instance.Spawn(pool_nameBloodImpact);

                }
                else
                {
                    trans_impact = BYPoolManager.instance.Spawn(pool_nameImpact);
                }

                if (trans_impact != null)
                {
                    trans_impact.position = hitInfo.point;
                    trans_impact.forward = hitInfo.normal;
                }


            }
        }
    }
}
