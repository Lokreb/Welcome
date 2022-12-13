using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Service : MonoBehaviour
{
    [SerializeField] private WayPointsValue _wpService;
    [SerializeField] private GameObject _Jeu;
    [SerializeField] private Services _serviceSecteur;
    [SerializeField] private Image _image;

    [SerializeField] private Patient _currentPatient;


    void Start()
    {
        GameManager.Instance.OnPatientService += PatientArrive;
        //_JeuResponse.OnGameResponse += ResultMiniGame;
        _Jeu.SetActive(false);

        SetServiceSprite();
    }
    private void PatientArrive(WayPointsValue wp, Patient p)
    {
        if (_wpService.ID == wp.ID)
        {
            _currentPatient = p;
        }
    }

    public void OnClick()
    {
        if (_currentPatient == null) return;

        _Jeu.SetActive(true);
        _currentPatient.Playing();

    }

    public void ResultMiniGame(bool win)
    {
        _currentPatient.EndMiniGame(win, _serviceSecteur);
        _Jeu.SetActive(false);
        _currentPatient = null;
    }

    void SetServiceSprite()
    {
        switch (_serviceSecteur)
        {
            case Services.A:
                _image.sprite = GameManager.Instance.ServiceVisuel[0];
                break;
            case Services.C:
                _image.sprite = GameManager.Instance.ServiceVisuel[1];
                break;
            case Services.D:
                _image.sprite = GameManager.Instance.ServiceVisuel[2];
                break;
            case Services.E:
                _image.sprite = GameManager.Instance.ServiceVisuel[3];
                break;
        }
    }
}
