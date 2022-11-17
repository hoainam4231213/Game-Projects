using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : WeaponBehaviour
{
    public int numberBullet;
    public override void InitWeapon()
    {
        base.InitWeapon();
        IWPHandle = new IShotGun();
        IWPHandle.InitHandle(this);

        BYPool pool_bullet = new BYPool { namePool = prefab_projecties.name, total = clip_size / 4, prefab_ = prefab_projecties };
        BYPoolManager.instance.AddNewPool(pool_bullet);
        BYPool pool_impact = new BYPool { namePool = prefab_impact.name, total = clip_size / 4, prefab_ = prefab_impact };
        BYPoolManager.instance.AddNewPool(pool_impact);
    }
}

public class IShotGun : IWeaponHandle
{
    public ShotGun wp_behaviour;
    public void FireHandle()
    {
        for(int i = 0; i < wp_behaviour.numberBullet; i++)
        {
            Transform bullet_trans = BYPoolManager.instance.Spawn(wp_behaviour.prefab_projecties.name);
            bullet_trans.position = wp_behaviour.muzzleFlash.transform.position;
            Vector3 pos = wp_behaviour.trans_aim.position;
            pos.y = bullet_trans.position.y;
            pos.x -= UnityEngine.Random.RandomRange(-0.3f, 0.3f);
            pos.y -= UnityEngine.Random.RandomRange(-0.3f,0.3f);
            Vector3 dir = pos - bullet_trans.position;
            dir.Normalize();
            BulletData bulletData = new BulletData();
            bulletData.dir = dir;
            bulletData.damge = wp_behaviour.damge;
            bulletData.force = wp_behaviour.force;
            bullet_trans.GetComponent<BulletWeapon>().Setup(bulletData);
        }
    }

    public void InitHandle(WeaponBehaviour weaponBehaviour)
    {
        wp_behaviour = (ShotGun)weaponBehaviour;
    }
}
