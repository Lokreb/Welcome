using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Service : MonoBehaviour
{
    [SerializeField]private WayPointsValue _wpService;
    [SerializeField]private GameObject _Jeu;

    [SerializeField]private Patient _currentPatient;


    void Start()
    {
        GameManager.Instance.OnPatientService += PatientArrive;
        //_JeuResponse.OnGameResponse += ResultMiniGame;
        _Jeu.SetActive(false);
    }
    private void PatientArrive(WayPointsValue wp,Patient p)
    {
        if(_wpService.ID == wp.ID)
        {
            _currentPatient = p;
        }
    }

    public void OnClick()
    {
        if (_currentPatient == null) return;

        _Jeu.SetActive(true);

    }

    public void ResultMiniGame(bool win)
    {
        _currentPatient.EndMiniGame(win);
        _Jeu.SetActive(false);
        _currentPatient=null;
    }
}
