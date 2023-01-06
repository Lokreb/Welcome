using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PhialItems : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    RectTransform rectTransform;
    Canvas canvas;
    CanvasGroup canvasGroup;
    public static string nameDrag;
    public GameDataScript _gameData;
    private Vector2 initial_position;
    private Vector2 initial_size;

    [Header("Phial Only")]
    [SerializeField] private Image _ContenantFiole;
    public int FioleMelange = 0;


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
        if ((name != "Phial" && !PhialManager._isCompleted && !PhialManager._isDraggable) || (name == "Phial" && PhialManager._isDraggable))
            ResizeOnDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if ((name != "Phial" && !PhialManager._isCompleted && !PhialManager._isDraggable) || (name == "Phial" && PhialManager._isDraggable))
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f;

        if (PhialSlots.nameSelectedSlot == null)
        {   
            if(PhialManager._Step1Finish || name == "Phial")return;
            BackInit();
        }
        else
        {
            if (PhialSlots.nameSelectedSlot == "Phial" && name != "Phial" && !PhialManager._isCompleted)
            {
                if(eventData.pointerEnter.name != "Fiole")
                {
                    if (PhialManager._Step1Finish) return;
                    BackInit();
                    return;
                }

                if(PhialManager._isDraggable)return;

                if(name == "Item" + _gameData.idCiblePhial)
                    PhialManager._winner = true;

                PhialManager._isDraggable = true;

                Color tubeCouleur = new Color();
                
                PhialItems fiolescript = eventData.pointerEnter.GetComponentInParent<PhialItems>();
                switch (fiolescript.FioleMelange)
                {
                    case 0:
                        tubeCouleur = PhialManager.CouleursPossible[(int)Char.GetNumericValue(name[name.Length - 1])];
                        break;
                    case 1 :
                        tubeCouleur = fiolescript._ContenantFiole.color + PhialManager.CouleursPossible[(int)Char.GetNumericValue(name[name.Length - 1])];
                        tubeCouleur *= .5f;
                        break;
                    case 2:
                        tubeCouleur = new Color(0.527451f, 0.33529415f, 0.22058825f,1f);
                        break;
                }
                fiolescript._ContenantFiole.color = tubeCouleur;
                fiolescript.FioleMelange += 1;

                GameObject fioleGO = eventData.pointerCurrentRaycast.gameObject;
   
                fioleGO.transform.localScale = new Vector3(.8f,.8f,.8f);
                fioleGO.transform.DOScale(1f,.5f).SetEase(Ease.OutElastic);
                PhialManager._Step1Finish = true;
                print("Fill up sound");
                Destroy(eventData.pointerDrag);
            }

            if(PhialManager._isDraggable && name == "Phial" && PhialSlots.nameSelectedSlot == "FinalSlot")
            {
                PhialManager._isCompleted = true;

                _ContenantFiole.color = Color.white;
                FioleMelange = 0;
                BackInit();
            }

            if (PhialManager._isDraggable && name == "Phial" && PhialSlots.nameSelectedSlot != "FinalSlot")
            {
                BackInit();
            }
            if(PhialSlots.nameSelectedSlot == "FinalSlot" && name != "Phial")
            {
                BackInit();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void BackInit()
    {
        rectTransform.anchoredPosition = initial_position;
        canvasGroup.blocksRaycasts = true;
        rectTransform.sizeDelta = initial_size;
        print("Fail drag sound");
        if(name != "Phial")transform.Rotate(0f,0f,-30f);
    }

    public void ResizeOnDrag()
    {
        print("Pick up glass sound");
        if(name != "Phial")
        {
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x*.8f, rectTransform.sizeDelta.y * .8f);
            transform.Rotate(0f,0f,30f);
        }
        else
        {
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x * .8f, rectTransform.sizeDelta.y * .8f);
        }
    }
}
