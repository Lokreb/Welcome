using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzleItems : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    RectTransform rectTransform;
    Canvas canvas;
    CanvasGroup canvasGroup;
    Vector2 initialPosition;
    public static string nameDrag;
    [HideInInspector] GameObject spawn;
    [HideInInspector] string actualTarget;
    public GameDataScript _gameData;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        spawn = GameObject.FindGameObjectWithTag("Respawn");
        initialPosition = spawn.GetComponent<RectTransform>().anchoredPosition;
    }


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

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
        if (PuzzleSlots.nameSelectedSlot == null)
        {
            StartingPosition();
        }
        else
        {
            switch (PuzzleSlots.nameSelectedSlot)
            {
                case "Slot_A":
                    if (name == "Piece1")
                    {
                        GoodPosition(PuzzleSlots.nameSelectedSlot);
                        Destroy(eventData.pointerDrag);
                    }
                    else
                        StartingPosition();
                    break;
                case "Slot_B":
                    if (name == "Piece2")
                    {
                        GoodPosition(PuzzleSlots.nameSelectedSlot);
                        Destroy(eventData.pointerDrag);
                    }
                    else
                        StartingPosition();
                    break;
                case "Slot_C":
                    if (name == "Piece3")
                    {
                        GoodPosition(PuzzleSlots.nameSelectedSlot);
                        Destroy(eventData.pointerDrag);
                    }
                    else
                        StartingPosition();
                    break;
                case "Slot_D":
                    if (name == "Piece4")
                    {
                        GoodPosition(PuzzleSlots.nameSelectedSlot);
                        Destroy(eventData.pointerDrag);
                    }
                    else
                        StartingPosition();
                    break;
                case "Slot_E":
                    if (name == "Piece5")
                    {
                        GoodPosition(PuzzleSlots.nameSelectedSlot);
                        Destroy(eventData.pointerDrag);
                    }
                    else
                        StartingPosition();
                    break;
                case "Slot_F":
                    if (name == "Piece6")
                    {
                        GoodPosition(PuzzleSlots.nameSelectedSlot);
                        Destroy(eventData.pointerDrag);
                    }
                    else
                        StartingPosition();
                    break;
                case "Slot_G":
                    if (name == "Piece7")
                    {
                        GoodPosition(PuzzleSlots.nameSelectedSlot);
                        Destroy(eventData.pointerDrag);
                    }
                    else
                        StartingPosition();
                    break;
                case "Slot_H":
                    if (name == "Piece8")
                    {
                        GoodPosition(PuzzleSlots.nameSelectedSlot);
                        Destroy(eventData.pointerDrag);
                    }
                    else
                        StartingPosition();
                    break;
                case "Slot_I":
                    if (name == "Piece9")
                    {
                        GoodPosition(PuzzleSlots.nameSelectedSlot);
                        Destroy(eventData.pointerDrag);
                    }
                    else
                        StartingPosition();
                    break;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
    }

    public void StartingPosition()
    {
        rectTransform.anchoredPosition = new Vector2(0, 0);
        canvasGroup.blocksRaycasts = true;
        rectTransform.sizeDelta = new Vector2(150, 150);
    }
    
    public void GoodPosition(string name)
    {
        _gameData.scorePuzzleGame += 10;
        _gameData.count++;
        rectTransform.sizeDelta = new Vector2(200, 200);
        GameObject go = GameObject.Find(name);
        go.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
    }
}
