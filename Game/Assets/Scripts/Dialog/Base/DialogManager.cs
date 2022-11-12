using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class DialogManager : BYSingleton<DialogManager>
{
    public RectTransform anchor;
    private Dictionary<DialogIndex, BaseDialog> dic_dialog = new Dictionary<DialogIndex, BaseDialog>();
    public List<BaseDialog> baseDialogs = new List<BaseDialog>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(DialogIndex d_index in DialogConfig.dialogIndices)
        {
            string name_index = d_index.ToString();
            GameObject go = Instantiate(Resources.Load("Dialog/" + name_index, typeof(GameObject))) as GameObject;
            go.transform.SetParent(anchor, false);
            BaseDialog baseDialog = go.GetComponent<BaseDialog>();
            dic_dialog.Add(d_index, baseDialog);
            go.SetActive(false);
        }
    }

    public void ShowDialog(DialogIndex dialogIndex, DialogParam dialogParam = null, Action callback = null)
    {
        BaseDialog baseDialog = dic_dialog[dialogIndex];
        baseDialog.gameObject.SetActive(true);
        baseDialog.Setup(dialogParam);
        baseDialogs.Add(baseDialog);
        DialogCallback dialogCallback = new DialogCallback();
        dialogCallback.callback = callback;
        baseDialog.BroadcastMessage("ShowDialog", dialogCallback, SendMessageOptions.RequireReceiver);
    }

    public void HideDialog(DialogIndex dialogIndex)
    {
        BaseDialog baseDialog = baseDialogs.Where(x => x.dialogIndex == dialogIndex).FirstOrDefault();
        DialogCallback dialogCallback = new DialogCallback();
        dialogCallback.callback = () =>
        {
            baseDialog.gameObject.SetActive(false);
            baseDialogs.Remove(baseDialog);
        };
        baseDialog.BroadcastMessage("HideDialog", dialogCallback, SendMessageOptions.RequireReceiver);
    }

    public void HideAllDialog()
    {
        foreach(BaseDialog e in baseDialogs)
        {
            e.gameObject.SetActive(false);
        }
        baseDialogs.Clear();
    }
}
