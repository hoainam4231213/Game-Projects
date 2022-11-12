using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class JoyStickControl : MonoBehaviour, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    private Vector3 moveDir;
    public RectTransform knod_js;
    public RectTransform bound_js;
    public RectTransform anchor_js;
    public RectTransform trans_js;

    public float limit_radious = 150;
    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = eventData.position;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(trans_js, pos, null, out localPoint);
        knod_js.anchoredPosition = localPoint;
        if(Vector2.Distance(knod_js.anchoredPosition,bound_js.anchoredPosition) >= limit_radious)
        {
            Vector2 dir = knod_js.anchoredPosition - bound_js.anchoredPosition;
            knod_js.anchoredPosition = bound_js.anchoredPosition + dir.normalized * limit_radious;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        anchor_js.position = eventData.position;
        knod_js.anchoredPosition = anchor_js.anchoredPosition;
        bound_js.anchoredPosition = anchor_js.anchoredPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        anchor_js.anchoredPosition = new Vector2(0, -350);
        knod_js.anchoredPosition = anchor_js.anchoredPosition;
        bound_js.anchoredPosition = anchor_js.anchoredPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        knod_js.anchoredPosition = anchor_js.anchoredPosition;
        bound_js.anchoredPosition = anchor_js.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        moveDir.x = (knod_js.anchoredPosition.x - anchor_js.anchoredPosition.x) / limit_radious;
        moveDir.y = (knod_js.anchoredPosition.y - anchor_js.anchoredPosition.y) / limit_radious;
        moveDir.x = Mathf.Clamp(moveDir.x, -1, 1);
        moveDir.y = Mathf.Clamp(moveDir.y, -1, 1);


        InputControlPlayer.instance.OnMoveInput(moveDir);
    }
}
