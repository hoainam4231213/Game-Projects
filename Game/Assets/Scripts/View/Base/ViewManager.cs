using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ViewManager : BYSingleton<ViewManager>
{
    public RectTransform anchor;
    public Transform menuView;
    private BaseView currentView;
    private Dictionary<ViewIndex, BaseView> dicView = new Dictionary<ViewIndex, BaseView>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(ViewIndex v_index in ViewConfig.viewIndices)
        {
            string name_view = v_index.ToString();
            GameObject go = Instantiate(Resources.Load("View/" + name_view, typeof(GameObject))) as GameObject;
            go.transform.SetParent(anchor,false);
            BaseView baseView = go.GetComponent<BaseView>();
            dicView.Add(v_index, baseView);
            go.SetActive(false);
        }
        instance.SwitchView(ViewIndex.EmptyView);
    }

    public void SwitchView(ViewIndex viewIndex, ViewParam viewParam = null, Action callback = null)
    {
        if (currentView != null)
        {
            ViewCallback viewCallback = new ViewCallback();
            viewCallback.callback = () =>
            {
                currentView.gameObject.SetActive(false);
                ShowNextView(viewIndex, viewParam, callback);
            };
            currentView.BroadcastMessage("HideView", viewCallback, SendMessageOptions.RequireReceiver);
        }
        else
            ShowNextView(viewIndex, viewParam, callback);
    }

    private void ShowNextView(ViewIndex viewIndex, ViewParam viewParam = null, Action callback = null)
    {
        currentView = dicView[viewIndex];
        currentView.gameObject.SetActive(true);
        currentView.Setup(viewParam);
        ViewCallback viewCallback = new ViewCallback();
        viewCallback.callback = callback;
        currentView.BroadcastMessage("ShowView", viewCallback, SendMessageOptions.RequireReceiver);
    }
}
