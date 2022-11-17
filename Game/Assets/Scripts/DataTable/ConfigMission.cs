using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConfigMissionRecord
{
    [SerializeField]
    private int id;
    [SerializeField]
    private string mission_id;
    public string Mission_id
    {
        get
        {
            return mission_id;
        }
    }
    [SerializeField]
    private string mission_name;
    public string Mission_name
    {
        get
        {
            return mission_name;
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
    private MissionType mission_type;
    public MissionType Mission_type
    {
        get
        {
            return mission_type;
        }
    }
    [SerializeField]
    private string waves;
    public List<WaveInits> Waves
    {
        get
        {
            List<WaveInits> ls = new List<WaveInits>();
            string[] s = waves.Split(';');
            foreach(string e_s in s)
            {
                string[] s_element = e_s.Split(':');
                WaveInits waveInits = new WaveInits();
                waveInits.wave_id = s_element[0];
                waveInits.delayTime = int.Parse(s_element[1]);
                ls.Add(waveInits);
            }
            return ls;
               
        }
    }
    [SerializeField]
    private string rewards;
    public string Rewards
    {
        get
        {
            return rewards;
        }
    }
    [SerializeField]
    private string map;
    public string Map
    {
        get
        {
            return map;
        }
    }

}
public class ConfigMission : BYDataTable<ConfigMissionRecord>
{
    public override ConfigCompare<ConfigMissionRecord> DefineCompare()
    {
        ConfigCompare<ConfigMissionRecord> configCompare = new ConfigCompare<ConfigMissionRecord>("mission_id");
        return configCompare;
    }
}

public class WaveInits
{
    public string wave_id;
    public int delayTime;
}

public enum MissionType
{
    MissionKillAll = 1,
    MissionAlive = 2
}
