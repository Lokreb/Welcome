using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationPopUpMinigames : MonoBehaviour
{
    public static float DELAIGLOBALEANIM = .5f;

    public void StartPop()
    {
        transform.localScale = Vector3.zero;

        transform.DOScale(new Vector2(1f,1f),.5f).SetEase(Ease.Linear);
    }
}
