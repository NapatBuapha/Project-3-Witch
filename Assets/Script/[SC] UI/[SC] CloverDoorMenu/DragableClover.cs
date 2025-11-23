using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableClover : MonoBehaviour , IBeginDragHandler , IEndDragHandler, IDragHandler
{
    public Image image;

    [HideInInspector] public Transform parentAfterDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {

        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
        transform.root as RectTransform, // Canvas root
        eventData.position,
        eventData.pressEventCamera,       // camera ที่ใช้กับ canvas
        out localPoint))
    {
        (transform as RectTransform).localPosition = localPoint;
    }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag,false);
        image.raycastTarget = true;
    }


}
