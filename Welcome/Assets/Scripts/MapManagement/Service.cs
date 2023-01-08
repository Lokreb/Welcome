using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Service : MonoBehaviour
{
    [SerializeField] private WayPointsValue _wpService;
    [SerializeField] private GameObject _Jeu;
    [SerializeField] private Services _serviceSecteur;
    [SerializeField] private GameObject _PopupImage;
    [SerializeField] private AnimationsClips _ResultScreen;

    [SerializeField] private Patient _currentPatient;

    [SerializeField] private Spawner _Spawner;
    [SerializeField] private ScoreManager _scoreManagerRythm;
    


    void Start()
    {
        GameManager.Instance.OnPatientService += PatientArrive;
        _Jeu.SetActive(false);
    }

    private void PatientArrive(WayPointsValue wp, Patient p)
    {
        if (_wpService.ID == wp.ID)
        {
            _currentPatient = p;
            _PopupImage.SetActive(true);
        }
    }

    public void OnClick()
    {
        if (_currentPatient == null) return;

        GameManager.Instance.InMinigame(true);

        if (_Jeu.name == "RythmGame")
        {
            _scoreManagerRythm.comboScore = 0;
            StartCoroutine(_Spawner.SpawnCubes());
            
        }

        _Jeu.SetActive(true);
        GameStateManager.Instance.SetState(GameState.Paused);

    }

    public void ResultMiniGame(bool win)
    {
        if(win) GameManager.Instance.ChangeScore(50);

        _currentPatient.EndMiniGame(win, _serviceSecteur);
        _ResultScreen.gameObject.SetActive(true);
        _ResultScreen.Result(win,this);
    }

    public void EndResult()
    {
        _ResultScreen.gameObject.SetActive(false);
        _Jeu.SetActive(false);
        _currentPatient = null;
        _PopupImage.SetActive(false);
        GameManager.Instance.InMinigame(false);
        GameStateManager.Instance.SetState(GameState.Gameplay);
    }
}
