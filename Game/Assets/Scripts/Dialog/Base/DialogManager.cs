using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class DialogManager : BYSingleton<DialogManager>
{
    public RectTransform anchor;
    private Dictionary<DialogIndex, BaseDialog> dicDialog = new Dictionary<DialogIndex, BaseDialog>();
    private List<BaseDialog> dialogs = new List<BaseDialog>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (DialogIndex d_index in DialogConfig.dialogIndices)
        {
            // create view 
            string nameDialog = d_index.ToString();
            GameObject d_Object = Instantiate(Resources.Load("Dialog/" + nameDialog, typeof(GameObject))) as GameObject;
            d_Object.transform.SetParent(anchor, false);
            BaseDialog dialog = d_Object.GetComponent<BaseDialog>();
            dicDialog.Add(d_index, dialog);
            d_Object.SetActive(false);
        }
        // instance.SwitchView(ViewIndex.EmptyView);
    }

    // Update is called once per frame
    public void ShowDialog(DialogIndex dialogIndex, DialogParam dialogparam = null, Action callback = null)
    {
        BaseDialog dialog = dicDialog[dialogIndex];
        dialogs.Add(dialog);
        dialog.gameObject.SetActive(true);
        dialog.Setup(dialogparam);
        DialogCallBack dialogCallBack = new DialogCallBack();

        dialogCallBack.callback = callback;
        dialog.BroadcastMessage("ShowDialog", dialogCallBack, SendMessageOptions.RequireReceiver);


    }
    public void HideDialog(DialogIndex dialogIndex)
    {
        BaseDialog dialog = dialogs.Where(x => x.dialogIndex == dialogIndex).FirstOrDefault();
        DialogCallBack dialogCallBack = new DialogCallBack();

        dialogCallBack.callback = () =>
        {
            dialog.gameObject.SetActive(false);
            dialogs.Remove(dialog);
        };
        dialog.BroadcastMessage("HideDialog", dialogCallBack, SendMessageOptions.RequireReceiver);
    }
    public void HideAllDialog()
    {
        foreach (BaseDialog dl in dialogs)
        {
            dl.gameObject.SetActive(false);
        }
        dialogs.Clear();
    }
}
