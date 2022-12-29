using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhialItems : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    RectTransform rectTransform;
    Canvas canvas;
    CanvasGroup canvasGroup;
    public static string nameDrag;
    public GameDataScript _gameData;
    private Vector2 initial_position;
    private Vector2 initial_size;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        initial_position = rectTransform.anchoredPosition;
        initial_size = rectTransform.sizeDelta;
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        nameDrag = name;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("La fiole est-elle accessible ?" + PhialManager._isDraggable);
        if ((name != "Phial" && !PhialManager._isCompleted) || (name == "Phial" && PhialManager._isDraggable))
            ResizeOnDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if ((name != "Phial" && !PhialManager._isCompleted) || (name == "Phial" && PhialManager._isDraggable))
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f;
        if (PhialSlots.nameSelectedSlot == null)
        {
            BackInit();
        }
        else
        {

            if (PhialSlots.nameSelectedSlot == "Phial" && name != "Phial" && !PhialManager._isCompleted)
            {

                Debug.Log("Selection : " + name );

                Debug.Log("Réponse : " + _gameData.idCiblePhial);

                if(name == "Item" + _gameData.idCiblePhial)
                    PhialManager._winner = true;

                PhialManager._isDraggable = true;
                

                Destroy(eventData.pointerDrag);
            }

            if(PhialManager._isDraggable && name == "Phial" && PhialSlots.nameSelectedSlot == "FinalSlot")
            {
                if (PhialManager._winner)
                    Debug.Log("Le joueur a gagné");
                else
                    Debug.Log("Le joueur est un looser, noooooooooooooooooooooob");
                PhialManager._isCompleted = true;
                Destroy(eventData.pointerDrag);
            }

            if (PhialManager._isDraggable && name == "Phial" && PhialSlots.nameSelectedSlot != "FinalSlot")
                BackInit();

            if(PhialSlots.nameSelectedSlot == "FinalSlot" && name != "Phial" && !PhialManager._isCompleted)
                BackInit();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
    }

    public void BackInit()
    {
        rectTransform.anchoredPosition = initial_position;
        canvasGroup.blocksRaycasts = true;
        rectTransform.sizeDelta = initial_size;
    }

    public void ResizeOnDrag()
    {
        if(name != "Phial")
        {
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x*.8f, rectTransform.sizeDelta.y * .8f);
        }
        else
        {
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x * .8f, rectTransform.sizeDelta.y * .8f);
        }
    }
}
