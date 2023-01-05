using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Patient : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private Vector2 _offset;
    [SerializeField]private CanvasGroup _canvasGroup;
    [SerializeField]private Sprite[] _ServiceVisuel;

    private Patient _clone;
    private Color alpha = new Color(1f,1f,1f,.5f);
    private Color complet = new Color(1f, 1f, 1f, 1f);
    [SerializeField] private SpriteRenderer _bulle;
    [SerializeField]private SpriteRenderer _service;
    [HideInInspector] public AnimatorUI AnimatorUIScript;
    

    public float Patience = 5f;

    //Data
    public Queue<Services> ServiceToSee = new Queue<Services>();
    public int[] PathIn = { 0, 0 };
    public bool InMiniGame = false;
    public int TweenID;

    void Awake()
    {
        PathIn[0] = 0;
        PathIn[1] = -1;
        GameManager.Instance.OnMiniGamePlaying += Playing;
    }

    void OnDestroy()
    {
        GameManager.Instance.OnMiniGamePlaying -= Playing;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        /*if (!InMiniGame) return;

        InMiniGame = false;
        GameManager.Instance.NextCase(this);*/

        _offset = GetMousePos() - (Vector2) transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (InMiniGame) return;

        AnimatorUIScript.Transparent(true);
        _bulle.color = alpha;
        _service.color = alpha;
        _canvasGroup.blocksRaycasts = false;
        

        _clone = Instantiate(this);
        _clone.gameObject.transform.SetParent(transform.parent);
        _clone.AnimatorUIScript.AnimatorComponent.SetBool("isGrab", true);
        

        if (ServiceToSee.Count == 0) return;
        _clone.ServiceToSee.Enqueue(ServiceToSee.Peek());
        _clone.SetSpriteBulle();
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(_clone==null)return;

        _clone.transform.position = GetMousePos() - _offset;        
    }

    public void OnDrop(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(_clone==null)return;

        Destroy(_clone.gameObject);
        _canvasGroup.blocksRaycasts = true;
        AnimatorUIScript.Transparent(false);
        _bulle.color = complet;
        _service.color = complet;
    }

    Vector2 GetMousePos()
    {
        return Input.mousePosition;
    }

    public void SetServiceToSee()
    {
        float tempsRestant = GameManager.Instance.Timer/GameManager.Instance._timerStart*100f;
        float rng = UnityEngine.Random.Range(0f,1f);
        int nbServices = 1;
        if(tempsRestant>95)
        {
            nbServices = 1;//100%
        }else if(tempsRestant>65)
        {
            nbServices = rng >= .4f ? 1 : 2;//60%
        }
        else if(tempsRestant>40)
        {
            if(rng <= .2f)
            {
                nbServices = 1;//20%
            }else if(rng <= .8f)
            {
                nbServices = 2;//60%
            }else
            {
                nbServices = 3;//20%
            }
        }
        else if(tempsRestant>25)
        {
            if(rng <= .15f)
            {
                nbServices = 1;//15%
            }else if(rng <= .45f)
            {
                nbServices = 2;//30%
            }else if(rng <= .9f)
            {
                nbServices = 3;//45%
            }else
            {
                nbServices = 4;//10%
            }
        }else if(tempsRestant>10)
        {
            if(rng <= .1f)
            {
                nbServices = 1;//10%
            }else if(rng <= .3f)
            {
                nbServices = 2;//20%
            }else if(rng <= .8f)
            {
                nbServices = 3;//50%
            }else if(rng <= .95f)
            {
                nbServices = 4;//15%
            }else
            {
                nbServices = 5;//5%
            }
        }else
        {
            if(rng <= .1f)
            {
                nbServices = 2;//10%
            }else if(rng <= .6f)
            {
                nbServices = 3;//50%
            }else if(rng <= .9f)
            {
                nbServices = 4;//30%
            }else
            {
                nbServices = 5;//10%
            }
        }
        
        for (int a = 0; a < nbServices; a++)
        {

            Services service = (Services)UnityEngine.Random.Range(0, (int)Services.MAX);

            if(a>0)
            {
                while(ServiceToSee.Peek()==service)
                {
                    service = (Services)UnityEngine.Random.Range(0, (int)Services.MAX);
                }
            }
            //force service
            service = Services.C;


            this.ServiceToSee.Enqueue(service);
        }

        SetSpriteBulle();
    }

    public void SetSpriteBulle()
    {
        if (ServiceToSee.Count == 0)
        {
            _service.transform.parent.gameObject.SetActive(false);
            return;
        }

        _service.transform.parent.gameObject.SetActive(true);

        switch (ServiceToSee.Peek())
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

    public void EndMiniGame(bool win,Services service)
    {
        StopCoroutine(Attente());

        if (win && ServiceToSee.Count > 0)
        {
            if(service == ServiceToSee.Peek())
            {
                ServiceToSee.Dequeue();
            }
        }

        SetSpriteBulle();
        InMiniGame = false;
        GameManager.Instance.NextCase(this);
    }

    public void Playing(bool playing)
    {
        if(!playing)
        {
            if(InMiniGame)
            {
                AttenteInGame();
                return;
            }

            return;
        }

        if(InMiniGame)
        {
            StopCoroutine(_coroutine);
        }
        
    }
    
    public void AttenteInGame()
    {
        if(_clone!=null)
        {
            OnEndDrag(null);
        }
        _service.transform.parent.gameObject.SetActive(false);
        InMiniGame = true;
        _coroutine = Attente();
        StartCoroutine(_coroutine);
    }

    private IEnumerator _coroutine;
    IEnumerator Attente()
    {
        while (InMiniGame)
        {
            yield return new WaitForSeconds(Patience);
            if (InMiniGame && GameStateManager.Instance.CurrentGameState == GameState.Gameplay)
            {
                GameManager.Instance.ChangeHumor(-1);
            }
        }
    }
}
