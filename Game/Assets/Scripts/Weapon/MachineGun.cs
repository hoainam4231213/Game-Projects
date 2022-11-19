using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : WeaponBehaviour
{
    public override void InitWeapon()
    {
        base.InitWeapon();
        IWPHandle = new IMachineGun();
        IWPHandle.InitHandle(this);

        BYPool pool_bullet = new BYPool { namePool = prefab_projecties.name, total = clip_size / 10, prefab_ = prefab_projecties };
        BYPoolManager.instance.AddNewPool(pool_bullet);
        BYPool pool_impact = new BYPool { namePool = prefab_impact.name, total = clip_size / 10, prefab_ = prefab_impact };
        BYPoolManager.instance.AddNewPool(pool_impact);
        BYPool pool_Bloodimpact = new BYPool { namePool = prefab_blood_impact.name, total = clip_size / 10, prefab_ = prefab_blood_impact };
        BYPoolManager.instance.AddNewPool(pool_Bloodimpact);
    }
}

public class IMachineGun : IWeaponHandle
{
    public MachineGun wp_behaviour;
    public void FireHandle()
    {
        Transform bullet_trans = BYPoolManager.instance.Spawn(wp_behaviour.prefab_projecties.name);
        bullet_trans.position = wp_behaviour.muzzleFlash.transform.position;
        Vector3 pos = wp_behaviour.trans_aim.position;
        float x = (100 - wp_behaviour.accuracy) * 0.01f;
        pos.x -= UnityEngine.Random.RandomRange(-x, x);
        Vector3 dir = pos - bullet_trans.position;
        dir.Normalize();
        BulletData bulletData = new BulletData();
        bulletData.dir = dir;
        bulletData.damge = wp_behaviour.damge;
        bulletData.force = wp_behaviour.force;
        bullet_trans.GetComponent<BulletWeapon>().Setup(bulletData);
    }

    public void InitHandle(WeaponBehaviour weaponBehaviour)
    {
        wp_behaviour = (MachineGun)weaponBehaviour;
    }
}
