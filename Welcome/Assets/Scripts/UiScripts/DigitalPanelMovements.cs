using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalPanelMovements : MonoBehaviour
{
    [SerializeField] UIServices _UIServices;

    public Vector2[] PositionToBe;
    

    private void Start()
    {
        _UIServices.OnAnimationService += UpdatePosition;
    }

    private void OnDestroy()
    {
        _UIServices.OnAnimationService -= UpdatePosition;
    }

    public void UpdatePosition(int index)
    {
        transform.localPosition = PositionToBe[index];
    }
}
