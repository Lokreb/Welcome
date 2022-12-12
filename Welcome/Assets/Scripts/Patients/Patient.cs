using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Patient : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private Vector2 _offset,_originalPosition;
    [SerializeField]private CanvasGroup _canvasGroup;
    [SerializeField]private Sprite[] _ServiceVisuel;

    private Patient _clone;
    [SerializeField]private Image _service;



    void Awake()
    {
        _originalPosition = transform.position;
        SetServiceToSee();
        SetSpriteBulle();
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

    void SetServiceToSee()
    {
        int nbServices = UnityEngine.Random.Range(1, 5);
        for (int a = 0; a < nbServices; a++)
        {
            Services service = (Services)UnityEngine.Random.Range(0, (int)Services.MAX);
            this.ServiceToSee.Enqueue(service);

        }

        SetSpriteBulle();
    }

    void SetSpriteBulle()
    {
        switch(ServiceToSee.Peek())
        {
            case Services.A :
                _service.sprite = _ServiceVisuel[0];
            break;
            case Services.C :
                _service.sprite = _ServiceVisuel[1];
            break;
            case Services.D :
                _service.sprite = _ServiceVisuel[2];
            break;
            case Services.E :
                _service.sprite = _ServiceVisuel[3];
            break;
        }
    }

    public void EndMiniGame(bool win)
    {
        if (win)
        {
            print(ServiceToSee.Peek());
            ServiceToSee.Dequeue();
            print(ServiceToSee.Peek());

            SetSpriteBulle();
        }
        

        InMiniGame = false;
        GameManager.Instance.NextCase(this);
    }
    //Data
    public Queue<Services> ServiceToSee = new Queue<Services>();
    public int[] PathIn = {0,0};
    public bool InMiniGame = false;
    public int TweenID;
}
