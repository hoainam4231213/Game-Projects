using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponHandle
{
    void InitHandle(WeaponBehaviour weaponBehaviour);
    void FireHandle();
}
