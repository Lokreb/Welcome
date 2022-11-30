using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Patient : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private Vector2 _offset,_originalPosition;
    [SerializeField]private CanvasGroup _canvasGroup;
    private Patient _clone;

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
        _canvasGroup.alpha = .5f;
        _canvasGroup.blocksRaycasts = false;

        _clone = Instantiate(this);
        _clone.gameObject.transform.SetParent(this.transform.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _clone.transform.position = GetMousePos() - _offset;

        
        
    }

    public void OnDrop(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        Destroy(_clone.gameObject);
        _canvasGroup.blocksRaycasts = true;
    }

    Vector2 GetMousePos()
    {
        return Input.mousePosition;
    }

    //Data
    //public Queue<Service> ServicesToSee = new Queue<Service>();
    public Queue<Services> ServiceToSee = new Queue<Services>();
    public int[] PathIn = {0,0};
    public bool InMiniGame = false;
    public int TweenID;
}
