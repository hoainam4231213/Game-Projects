using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class BaseDialogAnimation : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }
    public virtual void OnShowDiaglogAnimation(Action callback)
    {
        canvasGroup.DOFade(1f, 0.5f).OnComplete(() =>
         {
             callback?.Invoke();
         }).SetUpdate(true);
    }

    public virtual void OnHideDialogAnimation(Action callback)
    {
        canvasGroup.DOFade(0f, 0.5f).OnComplete(() =>
        {
            callback?.Invoke();
        }).SetUpdate(true);
    }

    private void Reset()
    {
        gameObject.name = typeof(BaseDialogAnimation).Name;
    }

}
