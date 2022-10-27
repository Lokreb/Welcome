using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Patient2 : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private Vector2 _offset,_originalPosition;
    [SerializeField]private CanvasGroup _canvasGroup;

    void Awake()
    {
        _originalPosition = transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _offset = GetMousePos() - (Vector2) transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = GetMousePos() - _offset;
    }

    public void OnDrop(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
    }

    

    Vector2 GetMousePos()
    {
        return Input.mousePosition;
    }
}
