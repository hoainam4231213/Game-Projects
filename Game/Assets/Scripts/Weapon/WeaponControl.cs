using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public Transform aim_point;
    public Transform trans_parent;
    public List<string> ls_weapon;
    private List<WeaponBehaviour> weaponBehaviours = new List<WeaponBehaviour>();
    private WeaponBehaviour currentGun;
    private int index = -1;

    public event Action<WeaponBehaviour> OnChangeGun;
    // Start is called before the first frame update
    void Start()
    {
        foreach(string s in ls_weapon)
        {
            ConfigWeaponRecord cf_wp = ConfigManager.instance.configWeapon.GetRecordByKeySearch(s);
            GameObject go_wpp = Instantiate(Resources.Load("Weapon/" + s, typeof(GameObject))) as GameObject;
            go_wpp.transform.SetParent(trans_parent, false);
            go_wpp.SetActive(false);
            WeaponBehaviour weaponBehaviour = go_wpp.GetComponent<WeaponBehaviour>();
            weaponBehaviour.Init(new WeaponData { cf = cf_wp, trans_aim = aim_point });
            weaponBehaviours.Add(weaponBehaviour);
        }
        ChangeGun();
    }

    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.K))
        {
            currentGun.OnFire(true);
        }
        else
        {
            currentGun.OnFire(false);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeGun();
        }

    }

    private void ChangeGun()
    {
        index++;
        if(index >= weaponBehaviours.Count)
        {
            index = 0;
        }
        currentGun?.GetNextGun(false);
        currentGun = weaponBehaviours[index];
        currentGun.GetNextGun(true);
        OnChangeGun?.Invoke(currentGun);
    }
}
