using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FakeCursorAnimation : MonoBehaviour
{
    public Transform Cursor;
    public Transform Char;
    public Sprite[] Sprites;
    public Image ImageChar;
    void Start()
    {
        GenereAleatoire();

        CursorStartMouvement();
        StartCoroutine(CharMouvement());
    }

    void CursorStartMouvement()
    {
        Sequence randomMouvements = DOTween.Sequence();
        //attente
        for (int a = 0; a < 6; a++)
        {
            randomMouvements.Append(Cursor.DOLocalMove(new Vector2(Random.Range(-750f, 750f), Random.Range(75f, 700f)), 2f).SetEase(Ease.InOutSine));
        }

        randomMouvements.OnComplete(() =>
        {
            StartCoroutine(CursorMouvement());
        });
        
    }
    IEnumerator CursorMouvement()
    {
        while (true)
        {
            Sequence randomMouvements = DOTween.Sequence();

            //attrape
            randomMouvements.Append(Cursor.DOLocalMove(new Vector2(-391.4f, -27f), 1.5f).SetEase(Ease.InOutSine));
            //monte
            randomMouvements.Append(Cursor.DOLocalMove(new Vector2(_shareRandomValueX[5], _shareRandomValueY[5]), 1f).SetEase(Ease.InOutSine));

            //attente
            for (int a = 0; a < 5; a++)
            {
                randomMouvements.Append(Cursor.DOLocalMove(new Vector2(_shareRandomValueX[a], _shareRandomValueY[a]), 2.1f).SetEase(Ease.InOutSine));
            }
            //relache
            randomMouvements.Append(Cursor.DOLocalMove(new Vector2(929f, 858f), 1f).SetEase(Ease.InOutSine));

            //attente
            for (int a = 0; a < 2; a++)
            {
                randomMouvements.Append(Cursor.DOLocalMove(new Vector2(Random.Range(-750f, 750f), Random.Range(75f, 700f)), 2.5f).SetEase(Ease.InOutSine));
            }

            GenereAleatoire();
            yield return new WaitForSeconds(18f);
        }
    }

    IEnumerator CharMouvement()
    {
        yield return new WaitForSeconds(8f);

        while (true)
        {
            Sequence charMouvements = DOTween.Sequence();

            //chute
            charMouvements.Append(Char.DOLocalMove(new Vector2(597, -14f), 2f).SetEase(Ease.InExpo));
            //tapis
            charMouvements.Append(Char.DOLocalMove(new Vector2(-391.4f, -14f), 3.5f).SetEase(Ease.Linear));
            //monte
            charMouvements.Append(Char.DOLocalMove(new Vector2(_shareRandomValueX[5], _shareRandomValueY[5]), 1f).SetEase(Ease.InOutSine));

            //attente
            for (int a = 0; a < 5; a++)
            {
                charMouvements.Append(Char.DOLocalMove(new Vector2(_shareRandomValueX[a], _shareRandomValueY[a]), 2.1f).SetEase(Ease.InOutSine));
            }
            //relache
            charMouvements.Append(Char.DOLocalMove(new Vector2(929f, 858f), 1f).SetEase(Ease.InOutSine));

            charMouvements.OnComplete(()=>
            {
                Char.localPosition = new Vector2(597f, 1000f);
                ImageChar.sprite = Sprites[Random.Range(0,10)];
            });
            yield return new WaitForSeconds(18f);
        }
    }

    float[] _shareRandomValueX = {0f,0f,0f,0f,0f,0f};
    float[] _shareRandomValueY = { 0f, 0f, 0f, 0f, 0f,0f};
    void GenereAleatoire()
    {
        for(int a=0;a<5;a++)
        {
            _shareRandomValueX[a] = Random.Range(-750f, 750f);
            _shareRandomValueY[a] = Random.Range(75f, 700f);
        }

        _shareRandomValueX[5] = Random.Range(-355f, 405f);
        _shareRandomValueY[5] = Random.Range(500f, 600f);
    }
}
