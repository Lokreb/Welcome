using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationPuzzle : MonoBehaviour
{
    float _vitesse = .25f;
    public void Apparition(float delai)
    {
        

        Sequence sequence = DOTween.Sequence();

        sequence.SetDelay(delai);
        sequence.Append(transform.DOMove(new Vector2(transform.position.x, transform.position.y + 150f),_vitesse).SetEase(Ease.Linear));
        sequence.Join(transform.DORotate(new Vector3(0f,0f,180f), _vitesse).SetEase(Ease.Linear));
        sequence.Append(transform.DOMove(new Vector2(transform.position.x, transform.position.y + 300f), _vitesse).SetEase(Ease.Linear));
        sequence.Join(transform.DORotate(new Vector3(0f, 0f, 360f),_vitesse).SetEase(Ease.Linear));
    }
}
