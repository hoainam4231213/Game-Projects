using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class ConfigEnemyRecord
{
    [SerializeField]
    private int id;
    [SerializeField]
    private string enemy_id;
    public string Enemy_id
    {
        get
        {
            return enemy_id;
        }
    }
    [SerializeField]
    private string enemy_name;
    public string Enemy_name
    {
        get
        {
            return enemy_name;
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
    private int hp;
    public int HP
    {
        get
        {
            return hp;
        }
    }
    [SerializeField]
    private int damage;
    public int Damage
    {
        get
        {
            return damage;
        }
    }
}

public class ConfigEnemy : BYDataTable<ConfigEnemyRecord>
{
    public override ConfigCompare<ConfigEnemyRecord> DefineCompare()
    {
        ConfigCompare<ConfigEnemyRecord> configCompare = new ConfigCompare<ConfigEnemyRecord>("enemy_id");
        return configCompare;
    }
}
