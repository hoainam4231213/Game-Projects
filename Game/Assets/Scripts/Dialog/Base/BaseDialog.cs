using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDialog : MonoBehaviour
{
    public DialogIndex dialogIndex;
    private BaseDialogAnimation dialogAnimation;
    private void Awake()
    {
        dialogAnimation = gameObject.GetComponentInChildren<BaseDialogAnimation>();
    }
    public virtual void Setup(DialogParam dialogParam)
    {

    }

    private void ShowDialog(DialogCallBack dialogCallback)
    {
        dialogAnimation.OnShowDiaglogAnimation(() =>
        {
            dialogCallback.callback?.Invoke();
            OnShowDialog();
        });
    }
    public virtual void OnShowDialog()
    {

    }

    private void HideDialog(DialogCallBack dialogCallback)
    {
        dialogAnimation.OnHideDialogAnimation(() =>
        {
            dialogCallback.callback?.Invoke();
            OnHideDialog();
        });
    }

    public virtual void OnHideDialog()
    {

    }
}
