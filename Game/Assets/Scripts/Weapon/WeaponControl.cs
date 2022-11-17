using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public Transform aim_point;
    private Transform trans;
    public Transform trans_parent;
    public PlayerControl playerControl;
    public float shootRange = 9;
    public List<string> ls_weapon;
    private List<WeaponBehaviour> weaponBehaviours = new List<WeaponBehaviour>();
    public List<Transform> enemy_list = new List<Transform>();
    private WeaponBehaviour currentGun;
    private int index = -1;

    public event Action<WeaponBehaviour> OnChangeGun;
    // Start is called before the first frame update
    private void Awake()
    {
        trans = transform;
    }
    void Start()
    {
        playerControl = GameObject.FindObjectOfType<PlayerControl>();
        foreach (string s in ls_weapon)
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
        if (enemy_list.Count > 0)
        {
            Vector3 dir = enemy_list[0].position - trans.position;
            dir.Normalize();
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            trans.rotation = Quaternion.Slerp(trans.rotation, q, Time.deltaTime * 360);
            playerControl.isAim = true;
            currentGun.OnFire(true);
        }
        else
        {
            playerControl.isAim = false;
            currentGun.OnFire(false);
        }
    }

    public void ChangeGun()
    {
        index++;
        if (index >= weaponBehaviours.Count)
        {
            index = 0;
        }
        currentGun?.GetNextGun(false);
        currentGun = weaponBehaviours[index];
        currentGun.GetNextGun(true);
        OnChangeGun?.Invoke(currentGun);
    }

    public void AddToListEnemy(Transform trans)
    {
        if (!enemy_list.Contains(trans))
            enemy_list.Add(trans);
    }

    public void RemoveTolistEnemy(Transform trans)
    {
        enemy_list.Remove(trans);
    }
}
