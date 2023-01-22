using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishBar : MonoBehaviour
{
    [SerializeField] Image _Fill;
    [SerializeField] Slider _Slider;
    public Gradient BarGradient;
    public void SetFill(float value)
    {
        Color color = BarGradient.Evaluate(value);

        _Fill.color = color;

        _Slider.value = value;
    }

}
