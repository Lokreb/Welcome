using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlinkingEffect : MonoBehaviour
{
    public Color[] ColorNew;
    Renderer _renderer;
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        Sequence sequence = DOTween.Sequence();

        sequence.Append(_renderer.material.DOColor(ColorNew[1], .5f).SetLoops(10,LoopType.Yoyo).SetEase(Ease.InOutQuad));
        sequence.Append(_renderer.material.DOColor(ColorNew[0], 0f));
        sequence.Append(_renderer.material.DOColor(ColorNew[2], .5f).SetLoops(10, LoopType.Yoyo).SetEase(Ease.InOutQuad));
        sequence.Append(_renderer.material.DOColor(ColorNew[0], 0f));
        sequence.Append(_renderer.material.DOColor(ColorNew[3], .5f).SetLoops(10, LoopType.Yoyo).SetEase(Ease.InOutQuad));
        sequence.Append(_renderer.material.DOColor(ColorNew[0], 0f));
    }

}
