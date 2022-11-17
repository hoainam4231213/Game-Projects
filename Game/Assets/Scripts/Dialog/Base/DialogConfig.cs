using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum DialogIndex
{
    WaveDialog = 0,
    VictoryDialog = 1,
    PauseDialog = 2,
    SettingDialog = 3,
    DefeatDialog = 4
}

public class DialogConfig
{
    public static DialogIndex[] dialogIndices =
    {
        DialogIndex.WaveDialog,
        DialogIndex.VictoryDialog,
        DialogIndex.DefeatDialog,
    };
}

public class DialogCallBack
{
    public Action callback;
}

public class DialogParam
{

}


