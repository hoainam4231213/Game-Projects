using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConfigWeaponRecord
{
    [SerializeField]
    private int id;
    [SerializeField]
    private string weapon_id;
    public string Weapon_id
    {
        get
        {
            return weapon_id;
        }
    }
    [SerializeField]
    private string weapon_name;
    public string Weapon_name
    {
        get
        {
            return weapon_name;
        }
    }
    [SerializeField]
    private string description;
    public string Description
    {
        get
        {
            return description;
        }
    }
    [SerializeField]
    private int damge;
    public int Damge
    {
        get
        {
            return damge;
        }
    }
    [SerializeField]
    private int clip_size;
    public int Clip_size
    {
        get
        {
            return clip_size;
        }
    }
    [SerializeField]
    private float rof;
    public float Rof
    {
        get
        {
            return rof;
        }
    }
}


public class ConfigWeapon : BYDataTable<ConfigWeaponRecord>
{
    public override ConfigCompare<ConfigWeaponRecord> DefineCompare()
    {
        ConfigCompare<ConfigWeaponRecord> configCompare = new ConfigCompare<ConfigWeaponRecord>("weapon_id");
        return configCompare;
    }
}
