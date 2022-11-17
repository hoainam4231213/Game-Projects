using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ConfigManager : BYSingleton<ConfigManager>
{
    public ConfigWeapon configWeapon;
    public ConfigEnemy configEnemy;
    public ConfigMission configMission;
    public ConfigWave configWave;
    // Start is called before the first frame update

    public void InitConfig(Action callback)
    {
        StartCoroutine(Init(callback));
    }

    IEnumerator Init(Action callback)
    {

        configWeapon = Resources.Load("DataTable/ConfigWeapon", typeof(ScriptableObject)) as ConfigWeapon;
        yield return new WaitUntil(() => configWeapon != null);

        configEnemy = Resources.Load("DataTable/ConfigEnemy", typeof(ScriptableObject)) as ConfigEnemy;
        yield return new WaitUntil(() => configEnemy != null);

        configWave = Resources.Load("DataTable/ConfigWave", typeof(ScriptableObject)) as ConfigWave;
        yield return new WaitUntil(() => configWave != null);

        configMission = Resources.Load("DataTable/ConfigMission", typeof(ScriptableObject)) as ConfigMission;
        yield return new WaitUntil(() => configMission != null);

        callback?.Invoke();
    }
}
