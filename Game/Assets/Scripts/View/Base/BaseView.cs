using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseView : MonoBehaviour
{
    public ViewIndex viewIndex;
    private BaseViewAnimation viewAnimation;
    // Start is called before the first frame update
    private void Awake()
    {
        viewAnimation = gameObject.GetComponentInChildren<BaseViewAnimation>();
    }
    
    public virtual void Setup(ViewParam viewParam)
    {

    }
    private void ShowView(ViewCallback viewCallback)
    {
        viewAnimation.OnShowViewAnimation(() =>
        {
            viewCallback.callback?.Invoke();
            OnShowView();
        });
    }

    public virtual void OnShowView()
    {

    }

    private void HideView(ViewCallback viewCallback)
    {
       viewAnimation.OnHideViewAnimation(() =>
       {
           viewCallback.callback?.Invoke();
           OnHideView();
       });
    }

    public virtual void OnHideView()
    {

    }
}
