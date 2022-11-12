using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ConfigManager : BYSingleton<ConfigManager>
{
    public ConfigWeapon ConfigWeapon;
    // Start is called before the first frame update

    public void InitConfig(Action callback)
    {
        StartCoroutine(Init(callback));
    }

    IEnumerator Init(Action callback)
    {
        ConfigWeapon = Resources.Load("DataTable/ConfigWeapon", typeof(ScriptableObject)) as ConfigWeapon;
        yield return new WaitUntil(() => ConfigWeapon != null);

        callback?.Invoke();
    }
}
