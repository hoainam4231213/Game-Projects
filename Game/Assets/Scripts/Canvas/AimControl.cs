using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class AimControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform trans_aim;
    public RectTransform bound_aim;
    public RectTransform knod_aim;
    public RectTransform anchor_aim;

    public float limit_radious = 110f;
    private Vector3 lookDir;

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = eventData.position;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(trans_aim, pos, null, out localPoint);
        knod_aim.anchoredPosition = localPoint;
        if(Vector2.Distance(knod_aim.anchoredPosition, bound_aim.anchoredPosition) >= limit_radious)
        {
            Vector2 dir = knod_aim.anchoredPosition - bound_aim.anchoredPosition;
            knod_aim.anchoredPosition = bound_aim.anchoredPosition + dir.normalized * limit_radious;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InputControlPlayer.instance.OnAimInput(true);
        anchor_aim.position = eventData.position;
        knod_aim.anchoredPosition = anchor_aim.anchoredPosition;
        bound_aim.anchoredPosition = anchor_aim.anchoredPosition;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputControlPlayer.instance.OnAimInput(false);
        anchor_aim.anchoredPosition = new Vector2(226f, -128f);
        knod_aim.anchoredPosition = anchor_aim.anchoredPosition;
        bound_aim.anchoredPosition = anchor_aim.anchoredPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        knod_aim.anchoredPosition = anchor_aim.anchoredPosition;
        bound_aim.anchoredPosition = anchor_aim.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        lookDir.x = (knod_aim.anchoredPosition.x - anchor_aim.anchoredPosition.x) / limit_radious;
        lookDir.y = (knod_aim.anchoredPosition.y - anchor_aim.anchoredPosition.y) / limit_radious;
        lookDir.x = Mathf.Clamp(lookDir.x, -1 ,1);
        lookDir.y = Mathf.Clamp(lookDir.y, -1, 1);
        InputControlPlayer.instance.OnLookInput(lookDir);
    }
}
