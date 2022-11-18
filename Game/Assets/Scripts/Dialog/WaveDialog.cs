using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveDialog : BaseDialog
{
    public Text waveIndex;
    public override void OnShowDialog()
    {
        base.OnShowDialog();
        
    }

    public override void OnHideDialog()
    {
        base.OnHideDialog();
    }

    public void Update()
    {
        waveIndex.text = MissionControl.instance.GetWaveIndex;
    }


}
