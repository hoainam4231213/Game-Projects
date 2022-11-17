using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryDialog : BaseDialog
{
    public override void OnShowDialog()
    {
        base.OnShowDialog();
        Time.timeScale = 0;
    }

    public override void OnHideDialog()
    {
        base.OnHideDialog();
        Time.timeScale = 1;
    }

    public void OnGoBack()
    {
        DialogManager.instance.HideDialog(dialogIndex);
        LoadSceneManager.instance.LoadScene(1, () =>
        {
            MenuManager.instance.EnableMenu();
            ViewManager.instance.SwitchView(ViewIndex.HomeView);
        });
    }
}
