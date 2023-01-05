using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationFioles : MonoBehaviour
{
    float _vitesse = 1f;
    public void Pop(float delai)
    {
        transform.localScale = Vector3.zero;

        Sequence sequence = DOTween.Sequence();

        sequence.SetDelay(delai);
        sequence.Append(transform.DOScale(1f,_vitesse).SetEase(Ease.OutElastic));
        sequence.Join(transform.DOShakeRotation(_vitesse, new Vector3(0f,0f,20f), 3 , 90f, true, ShakeRandomnessMode.Full).SetEase(Ease.Linear));

    }
}
