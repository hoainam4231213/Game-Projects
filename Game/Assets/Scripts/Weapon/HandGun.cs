using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : WeaponBehaviour
{
    public override void InitWeapon()
    {
        base.InitWeapon();
        IWPHandle = new IHandGun();
        IWPHandle.InitHandle(this);

        BYPool pool_bullet = new BYPool {namePool = prefab_projecties.name, total = clip_size, prefab_ = prefab_projecties };
        BYPoolManager.instance.AddNewPool(pool_bullet);
        BYPool pool_impact = new BYPool { namePool = prefab_impact.name, total = clip_size, prefab_ = prefab_impact };
        BYPoolManager.instance.AddNewPool(pool_impact);
    }
}

public class IHandGun : IWeaponHandle
{
    public HandGun wp_behaviour;
    public void FireHandle()
    {
        
        
    }

    public void InitHandle(WeaponBehaviour weaponBehaviour)
    {
        wp_behaviour = (HandGun)weaponBehaviour;
    }
}
