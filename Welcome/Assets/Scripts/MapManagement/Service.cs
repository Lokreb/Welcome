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

    [SerializeField] private AudioClip[] _AudioClips;
    [SerializeField] private AudioSource _AudioSource;
    


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
            _AudioSource.clip = _AudioClips[0];
            _AudioSource.Play();
        }
    }

    public void OnClick()
    {
        if (_currentPatient == null) return;

        GameManager.Instance.InMinigame(true);
        _Jeu.SetActive(true);
        GameStateManager.Instance.SetState(GameState.Paused);
        _AudioSource.clip = _AudioClips[1];
        _AudioSource.Play();

    }

    public void ResultMiniGame(bool win)
    {
        if(win) GameManager.Instance.ChangeScore(50);
        print("Play win ?"+win);
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
