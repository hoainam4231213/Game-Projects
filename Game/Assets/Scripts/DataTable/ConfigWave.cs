using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConfigWaveRecord
{
    [SerializeField]
    private int id;
    [SerializeField]
    private string wave_id;
    public string Wave_id
    {
        get
        {
            return wave_id;
        }
    }
    [SerializeField]
    private string enemies_id;
    public List<string> Enemies_id
    {
        get
        {
            List<string> ls_s = new List<string>();
            string[] s = enemies_id.Split(';');
            ls_s.AddRange(s);
            return ls_s;
        }
    }
    [SerializeField]
    private int number;
    public int Number
    {
        get
        {
            return number;
        }
    }

}


public class ConfigWave : BYDataTable<ConfigWaveRecord>
{
    public override ConfigCompare<ConfigWaveRecord> DefineCompare()
    {
        ConfigCompare<ConfigWaveRecord> configCompare = new ConfigCompare<ConfigWaveRecord>("wave_id");
        return configCompare;
    }
}
