using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloverReceiver : MonoBehaviour, IDropHandler
{
    CloverDoorUI cloverDoorUI;
    void Awake()
    {
        cloverDoorUI = FindAnyObjectByType<CloverDoorUI>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        DragableClover dragableClover = dropped.GetComponent<DragableClover>();
        if (dragableClover == null) return;

        // ตั้ง parent → หมุนตาม parent ด้วย local transform
        dropped.transform.SetParent(transform, false);

        // รีเซ็ตตำแหน่งใน parent
        RectTransform droppedRT = dropped.GetComponent<RectTransform>();
        droppedRT.anchoredPosition = Vector2.zero;

        // อัพเดต parentAfterDrag เพื่อให้ DragableClover รู้
        dragableClover.parentAfterDrag = transform;

        cloverDoorUI.UpdateClover();
    }
    }
}
