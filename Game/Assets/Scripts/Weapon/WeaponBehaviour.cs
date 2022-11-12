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
    [NonSerialized]
    public Transform trans_aim;
    public IWeaponHandle IWPHandle;
    public AnimatorOverrideController animatorOverride;
    [NonSerialized]
    public PlayerDatabinding databinding;
    public Transform prefab_projecties;
    public Transform prefab_impact;
    public MuzzleFlash muzzleFlash;

    public float damge;
    public int clip_size;
    public float rof;
    public float reloadTime;
    private float timeFire;
    public int projecties;

    private bool Reloading;
    private bool isFire;

    
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
            if(timeFire >= rof && projecties > 0 && !Reloading)
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
            if (projecties <= 0)
            {
                OnReload();
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
        StartCoroutine("ReLoading");
    }

    IEnumerator ReLoading()
    {
        databinding.Reload = true;
        yield return new WaitForSeconds(reloadTime);
        EndReload();
    }

    public void EndReload()
    {
        Reloading = false;
        projecties = clip_size / 2;
        
    }
}
