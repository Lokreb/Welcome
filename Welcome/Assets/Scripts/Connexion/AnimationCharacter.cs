using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class AnimationCharacter : MonoBehaviour
{
    public int PositionAttente;
    public Sprite[] Sprites;
    private Image _image;

    public GameObject[] CharactersGO;
    private float[] _originalsPositions;
    public Image[] Images;
    private void Start()
    {
        //_image = GetComponent<Image>();

        //v1
        //StartCoroutine(Jumps());
        //v2
        //ScrollStart();

        //v3
        //transform.localPosition = new Vector2(597f,1000f);
        //StartCoroutine(ScrollingOut());

        //v4
        int nb = CharactersGO.Length;
        for(int a=0;a<nb;a++)
        {
            CharactersGO[a].transform.localPosition = new Vector2(597f, 1000f);
        }
        StartCoroutine(ScrollingAll());
    }

    IEnumerator Jumps()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 10f));

            Sequence jumpSequence = DOTween.Sequence();
            float height = Random.Range(50f,100f);

            jumpSequence.Append(transform.DOMoveY(transform.position.y + height, .8f)).SetEase(Ease.OutQuad);
            jumpSequence.Append(transform.DOMoveY(transform.position.y, .6f).SetEase(Ease.InExpo));
            
        }
    }

    float[] values = { -90f, -60f, -120f, 30f, 90f, 60f, 120f, 30f };
    IEnumerator ScrollingOut()
    {
        yield return new WaitForSeconds(PositionAttente);

        while (true)
        {
            float value = values[Random.Range(0, 8)];
            Sequence scrolling = DOTween.Sequence();

            scrolling.Append(transform.DOLocalMove(new Vector2(597, 0f), 2f).SetEase(Ease.InExpo));
            scrolling.Append(transform.DOLocalMove(new Vector2(-815f, 0f), 5f).SetEase(Ease.Linear));
            scrolling.Append(transform.DOLocalMove(new Vector2(-875f, -330f), 1f).SetEase(Ease.InSine));
            scrolling.Join(transform.DOLocalRotate(new Vector3(0f, 0f,value), 1.5f));
            

            scrolling.OnComplete(()=>
            {
                transform.Rotate(0f,0f, -value);
                transform.localPosition = new Vector2(597f,1000f);
                _image.sprite = Sprites[Random.Range(0,10)];
            });

            if(PositionAttente<8f)
            {
                yield return new WaitForSeconds(9f);
            }else
            {
                yield return new WaitForSeconds(18f);
            }
            
        }
    }

    IEnumerator ScrollingAll()
    {
        int a = 0;
        int nb = CharactersGO.Length;
        bool cycle1 = true;
        while (a<nb)
        {
            if(cycle1 && a==nb-1)
            {
                cycle1 = false;
            }else
            {
                ScrollingV4(a);
                if (a == nb - 1) cycle1 = true;
            }
            

            a = a < nb-1 ? a + 1 : 0;
            yield return new WaitForSeconds(1);
        }
    }

    void ScrollingV4(int a)
    {
        float value = values[Random.Range(0, 8)];
        Sequence scrolling = DOTween.Sequence();

        scrolling.Append(CharactersGO[a].transform.DOLocalMove(new Vector2(597, 0f), 2f).SetEase(Ease.InExpo));
        scrolling.Append(CharactersGO[a].transform.DOLocalMove(new Vector2(-815f, 0f), 5f).SetEase(Ease.Linear));
        scrolling.Append(CharactersGO[a].transform.DOLocalMove(new Vector2(-875f, -330f), 1f).SetEase(Ease.InSine));
        scrolling.Join(CharactersGO[a].transform.DOLocalRotate(new Vector3(0f, 0f, value), 1.5f));


        scrolling.OnComplete(() =>
        {
            CharactersGO[a].transform.Rotate(0f, 0f, -value);
            CharactersGO[a].transform.localPosition = new Vector2(597f, 1000f);
            Images[a].sprite = Sprites[Random.Range(0, 10)];
        });
    }

    void ScrollStart()
    {
        float timeReachFall = Mathf.Abs(-815f - transform.localPosition.x)*5f/1412f;
        print(-815f - transform.localPosition.x);

        Sequence scrollingStart = DOTween.Sequence();
        scrollingStart.Append(transform.DOLocalMove(new Vector2(-815f, 0f), timeReachFall).SetEase(Ease.Linear));
        scrollingStart.Append(transform.DOLocalMove(new Vector2(-875f, -330f), 1f).SetEase(Ease.InSine));
        scrollingStart.Join(transform.DOLocalRotate(new Vector3(0f, 0f, 90f), 1.5f));
        scrollingStart.OnComplete(() =>
        {
            transform.Rotate(0f, 0f, -90f);
            transform.localPosition = new Vector2(597f, 1000f);
            StartCoroutine(ScrollingOut());
        });
    }
}
