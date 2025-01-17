using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public static DragDrop itemBeingDragged;

    public Vector3 startPos {  get; private set; }
    public Transform startParent {  get; private set; }

    private bool canDrag;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetCanDrag(bool state)
    {
        canDrag = state;
        canvasGroup.blocksRaycasts = canDrag;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = this;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;
        startPos = transform.position;
        startParent = transform.parent;
        transform.SetParent(transform.root);
    }


    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / rectTransform.localScale;
    }

   
    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        if (transform.parent == startParent || transform.parent == transform.root)
        {
            transform.position = startPos;
            transform.SetParent(startParent);
            canDrag = true;
        }
        if (canDrag)
            canvasGroup.blocksRaycasts = true;
        else
            canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 1;

    }

    public void SetNewPos(Vector3 pos, Transform parent)
    {
        transform.position = pos;
        transform.SetParent(parent);
    }
}
