using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Patient : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private Vector2 _offset,_originalPosition;
    [SerializeField]private CanvasGroup _canvasGroup;

    void Awake()
    {
        _originalPosition = transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("down");
        _offset = GetMousePos() - (Vector2) transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("drag");
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

    //Data
    public Queue<Service> ServicesToSee = new Queue<Service>();
    public Localisation ServiceLocalisation = Localisation.Start;
}
