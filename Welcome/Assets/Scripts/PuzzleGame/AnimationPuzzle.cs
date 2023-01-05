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

    float _vitesse2 = 1.5f;  
    public void CutPieces()
    {
        float distance = transform.localPosition.x;
        float hauteur = transform.localPosition.y *.1f;

        Sequence sequence = DOTween.Sequence();

        sequence.SetDelay(.1f);

        sequence.Append(transform.DOLocalJump(new Vector2(distance*.5f,0f),transform.localPosition.y+hauteur,1,_vitesse2*.5f).SetEase(Ease.Linear));
        sequence.Append(transform.DOLocalJump(new Vector2(distance*.3f,0f),hauteur*2f,1,_vitesse2*.2f).SetEase(Ease.Linear));
        sequence.Append(transform.DOLocalJump(new Vector2(distance*.1f,0f),hauteur*.5f,1,_vitesse2*.2f).SetEase(Ease.Linear));

        sequence.Append(transform.DOLocalMoveX(0f,_vitesse2*.1f).SetEase(Ease.Linear));

    }
}
