using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
        if (!InMiniGame) return;

        InMiniGame = false;
        GameManager.Instance.NextCase(this);

        _offset = GetMousePos() - (Vector2) transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var tempColor = ImagePerso.color;
        tempColor.a = .5f;
        ImagePerso.color = tempColor;

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
        var tempColor = ImagePerso.color;
        tempColor.a = 1f;
        ImagePerso.color = tempColor;

        _canvasGroup.blocksRaycasts = true;
    }

    Vector2 GetMousePos()
    {
        return Input.mousePosition;
    }

    //Data
    //public Queue<Service> ServicesToSee = new Queue<Service>();
    public Image ImagePerso;
    public Queue<Services> ServiceToSee = new Queue<Services>();
    public int[] PathIn = {0,0};
    public bool InMiniGame = false;
}
