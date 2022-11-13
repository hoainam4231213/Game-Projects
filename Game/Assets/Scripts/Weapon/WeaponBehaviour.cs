using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WeaponData
{
    public ConfigWeaponRecord cf;
    public Transform trans_aim;
}
public class WeaponBehaviour : MonoBehaviour
{
    public WeaponData weaponData;
    public Transform trans_aim;
    public IWeaponHandle IWPHandle;
    public AnimatorOverrideController animatorOverride;
    public PlayerDatabinding databinding;
    public Transform prefab_projecties;
    public Transform prefab_impact;
    public MuzzleFlash muzzleFlash;

    [NonSerialized]
    public float damge;
    [NonSerialized]
    public int clip_size;
    public float rof;
    public float reloadTime;
    private float ChangeGunTime = 1;
    private float timeFire;
    public float force;
    [NonSerialized]
    public int projecties;

    private bool Reloading;
    private bool isFire;
    private bool isChangeGun;

    
    // Start is called before the first frame update
    
    public void Init(WeaponData weaponData)
    {
        this.weaponData = weaponData;
        this.trans_aim = weaponData.trans_aim;
        databinding = gameObject.GetComponentInParent<PlayerDatabinding>();
        muzzleFlash = gameObject.GetComponentInChildren<MuzzleFlash>();
        damge = weaponData.cf.Damge;
        clip_size = weaponData.cf.Clip_size;
        projecties = clip_size / 2;

        InitWeapon();
    }

    public virtual void InitWeapon()
    {

    }

    public void OnFire(bool is_fire)
    {
        this.isFire = is_fire;
    }

    public void Update()
    {
        timeFire += Time.deltaTime;
        if(isFire)
        {
            if(timeFire >= rof && projecties > 0 && !Reloading && !isChangeGun)
            {
                timeFire = 0;

                databinding.Fire = true;
                projecties--;
                muzzleFlash.FireHandle();
                IWPHandle.FireHandle();
                if (projecties <= 0)
                {
                    Invoke("OnReload", rof);
                }
            }    
        }
    }

    public void GetNextGun(bool isActive)
    {
        if(isActive)
        {
            gameObject.SetActive(true);
            databinding.Empty = true;
            databinding.ChangeAnimatorController(animatorOverride);
            StartCoroutine("ChangeGunning");
            if (projecties <= 0)
            {
                Invoke("OnReload", ChangeGunTime);
            }
        }
        else
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
    }
    private void OnReload()
    {
        Reloading = true;
        if(gameObject.activeSelf)
            StartCoroutine("ReLoading");
    }

    IEnumerator ReLoading()
    {
        databinding.Reload = true;
        yield return new WaitForSeconds(reloadTime);
        EndReload();
    }
    IEnumerator ChangeGunning()
    {
        isChangeGun = true;
        databinding.Draw = true;
        yield return new WaitForSeconds(ChangeGunTime);
        isChangeGun = false;
    }

    public void EndReload()
    {
        Reloading = false;
        projecties = clip_size / 2;
        
    }
}
