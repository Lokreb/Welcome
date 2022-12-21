using UnityEngine;
using DG.Tweening;
using System.Collections;

public class AnimationCharacter : MonoBehaviour
{
    public int PositionAttente;
    private float _originalPosition;
    private void Start()
    {
        _originalPosition = transform.localPosition.y;

        //v1
        //StartCoroutine(Jumps());
        //v2
        //ScrollStart();

        //v3
        transform.localPosition = new Vector2(597f,1000f);
        StartCoroutine(ScrollingOut());
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

    IEnumerator ScrollingOut()
    {
        yield return new WaitForSeconds(PositionAttente);

        while (true)
        {
            Sequence scrolling = DOTween.Sequence();

            scrolling.Append(transform.DOLocalMove(new Vector2(597, _originalPosition), 2f).SetEase(Ease.InExpo));
            scrolling.Append(transform.DOLocalMove(new Vector2(-815f, _originalPosition), 5f).SetEase(Ease.Linear));
            scrolling.Append(transform.DOLocalMove(new Vector2(-875f, -330f), 1f).SetEase(Ease.InSine));
            scrolling.Join(transform.DOLocalRotate(new Vector3(0f, 0f, 90f), 1.5f));

            scrolling.OnComplete(()=>
            {
                transform.Rotate(0f,0f,-90f);
                transform.localPosition = new Vector2(597f,1000f);
            });

            yield return new WaitForSeconds(10f);
        }
    }

    void ScrollStart()
    {
        float timeReachFall = Mathf.Abs(-815f - transform.localPosition.x)*5f/1412f;
        print(-815f - transform.localPosition.x);

        Sequence scrollingStart = DOTween.Sequence();
        scrollingStart.Append(transform.DOLocalMove(new Vector2(-815f, _originalPosition), timeReachFall).SetEase(Ease.Linear));
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
