using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlinkingEffect : MonoBehaviour
{
    public Transform Exterieur;

    Renderer[] _renderer = {null,null};
    private void Awake()
    {
        _renderer[0] = GetComponent<Renderer>();
        _renderer[1] = Exterieur.GetComponent<Renderer>();


        //alpha
        _renderer[0].material.DOFade(.75f, .5f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutQuad);
        _renderer[1].material.DOFade(.75f, .5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        //size
        transform.DOScale(new Vector2(26f,26f), .5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        Exterieur.DOScale(new Vector2(1.1f,1.1f), .5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

    }
}
