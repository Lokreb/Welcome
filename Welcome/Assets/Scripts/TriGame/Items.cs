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
        if (Slots.nameSelectedSlot == null)
        {
            rectTransform.anchoredPosition = new Vector2(50,50);
            canvasGroup.blocksRaycasts = true;
            rectTransform.sizeDelta = new Vector2(200, 200);
        }
        else
        {
            actualTarget = Slots.nameSelectedSlot;
            Slots.nameSelectedSlot = null;
            Destroy(eventData.pointerDrag);

            switch (actualTarget)
            {
                case "ServiceA":
                    if (_gameData.idCible[_gameData.count] == 1)
                        _gameData.scoreSortGame += 10;
                    else
                        _gameData.scoreSortGame -= 10;
                    break;
                case "ServiceB":
                    if (_gameData.idCible[_gameData.count] == 2)
                        _gameData.scoreSortGame += 10;
                    else
                        _gameData.scoreSortGame -= 10;
                    break;
                case "ServiceC":
                    if (_gameData.idCible[_gameData.count] == 3)
                        _gameData.scoreSortGame += 10;
                    else
                        _gameData.scoreSortGame -= 10;
                    break;
                case "ServiceD":
                    if (_gameData.idCible[_gameData.count] == 4)
                        _gameData.scoreSortGame += 10;
                    else
                        _gameData.scoreSortGame -= 10;
                    break;
            }
            if (_gameData.scoreSortGame < 0)
                _gameData.scoreSortGame = 0;
            _gameData.count++;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
    }
}
