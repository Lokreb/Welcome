using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Items : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    RectTransform rectTransform;
    Canvas canvas;
    CanvasGroup canvasGroup;
    Vector2 initialPosition;
    public static string nameDrag;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;
        nameDrag = name;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        rectTransform.sizeDelta = new Vector2(100, 100);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f;
        //FolderScript actualFolder = GameObject.Find(eventData.pointerDrag.transform.parent.name).GetComponent<FolderScript>();
        //Debug.Log("ID de l'élément selectionné : " + actualFolder.service_ID);
        //Debug.Log(Slots.nameSelectedSlot);
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
    }
}
