using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventListener : EventTrigger
{
    public Action<GameObject, PointerEventData> onClick;
    public Action<GameObject, PointerEventData> onDown;
    public Action<GameObject, PointerEventData> onEnter;
    public Action<GameObject, PointerEventData> onExit;
    public Action<GameObject, PointerEventData> onUp;
    public Action<GameObject, PointerEventData> onBeginDrag;
    public Action<GameObject, PointerEventData> onEndDrag;
    public Action<GameObject, PointerEventData> onDrag;
    public Action<GameObject, PointerEventData> onScroll;

    public static UIEventListener Get(GameObject go)
    {
        UIEventListener listener = go.GetComponent<UIEventListener>();
        if (listener == null) listener = go.AddComponent<UIEventListener>();
        return listener;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke(gameObject, eventData);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        onDown?.Invoke(gameObject, eventData);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        onEnter?.Invoke(gameObject, eventData);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        onExit?.Invoke(gameObject, eventData);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        onUp?.Invoke(gameObject, eventData);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        onBeginDrag?.Invoke(gameObject, eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        onDrag?.Invoke(gameObject, eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        onEndDrag?.Invoke(gameObject, eventData);
    }

    public override void OnScroll(PointerEventData eventData)
    {
        onScroll?.Invoke(gameObject, eventData);
    }
}