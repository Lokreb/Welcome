using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIServices : MonoBehaviour
{
    public event Action<int> OnAnimationService;

    public TextMeshProUGUI NB_Patient;
    public TextMeshProUGUI Multiplicator;
    public Sprite[] SpriteSheetsAnimation;
    [SerializeField] private SpriteRenderer _ImageAnim;
    int _indexAnimation = 0;

    public Service Service;

    int _nbPatient = 0;
    float _multiplicator = 1f;

    private void Start()
    {
        Service.OnPatientChange += PatientChange;
        Service.OnMultiplicatorChange += MultiplicatorChange;
    }

    private void FixedUpdate()
    {
        if (_nbPatient <= 0) return;

        if (GameStateManager.Instance.CurrentGameState == GameState.Paused) return;

        WorkingAnimation();
    }

    void PatientChange(int nb)
    {
        _nbPatient = nb;
        NB_Patient.text = nb.ToString();

        if (_nbPatient <= 0)
        {
            _ImageAnim.sprite = SpriteSheetsAnimation[0];
            _waitASecond = 0f;

            OnAnimationService?.Invoke(_indexAnimation);
        }
    }

    Color alpha0f = new Color(1f, 1f, 1f, 0f);
    Color alpha1f = new Color(1f, 1f, 1f, 1f);
    void MultiplicatorChange()
    {
        _multiplicator = Service.VitesseMultiplicator;
        Multiplicator.color = Service.VitesseMultiplicator == 1 ? alpha0f : alpha1f;
        Multiplicator.text = "x " + _multiplicator.ToString();
    }

    float _frameRateAnimation = 0f;
    float _waitASecond = 0f;
    void WorkingAnimation()
    {
        if(_waitASecond<=40f)
        {
            _waitASecond++;
            return;
        }

        _frameRateAnimation += _multiplicator;

        
        if(_frameRateAnimation >= 4f)
        {
            _frameRateAnimation = 0f;
            _indexAnimation = (_indexAnimation + 1) % SpriteSheetsAnimation.Length;
            _ImageAnim.sprite = SpriteSheetsAnimation[_indexAnimation % SpriteSheetsAnimation.Length];

            OnAnimationService?.Invoke(_indexAnimation);
        }

    }
}
