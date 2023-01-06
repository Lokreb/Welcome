using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationBarriere : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.OnMoveBarriere += MoveBarriere;
    }

    public void MoveBarriere(bool opening)
    {
        float z = opening ? -65 : 35;

        transform.DORotate(new Vector3(0f,0f,z),1f);
    }
}
