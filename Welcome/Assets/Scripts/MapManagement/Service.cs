using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Service : MonoBehaviour
{
    [SerializeField]private WayPointsValue _wpService;
    [SerializeField]private Jeu_GeneralFonction _JeuResponse;

    [SerializeField]private Patient _currentPatient;


    void Start()
    {
        GameManager.Instance.OnPatientService += PatientArrive;
        _JeuResponse.OnGameResponse += ResultMiniGame;
        _JeuResponse.gameObject.SetActive(false);
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

        _JeuResponse.gameObject.SetActive(true);

    }

    void ResultMiniGame(bool win)
    {
        _currentPatient.EndMiniGame(win);
        _JeuResponse.gameObject.SetActive(false);
        _currentPatient=null;
    }
}
