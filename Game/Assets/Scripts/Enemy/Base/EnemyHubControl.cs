using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHubControl : MonoBehaviour
{
    [SerializeField]
    private RectTransform rect_trans;
    private Transform anchor;
    private RectTransform parentHub;
    private Camera cam;
    public Image healthUI;
    // Start is called before the first frame update

    private void Awake()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        Vector2 scenePos = RectTransformUtility.WorldToScreenPoint(cam, anchor.position);
        Vector2 localPoint = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentHub, scenePos, null, out localPoint);
        rect_trans.anchoredPosition = localPoint;

    }

    public void Init(Transform anchor, RectTransform parentHub)
    {
        this.anchor = anchor;
        this.parentHub = parentHub;
        rect_trans.SetParent(this.parentHub, false);
    }

    public void UpdateHealth(int hp, int maxhp)
    {
        healthUI.fillAmount = (float) hp / (float) maxhp;
        
    }
}
