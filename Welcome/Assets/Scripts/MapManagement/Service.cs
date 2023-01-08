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
    
    [SerializeField] private AudioClip[] _AudioClips;
    [SerializeField] private AudioSource _AudioSource;

    [SerializeField] SelectorMinigames _Selector;
    [SerializeField] AnimationPopUpMinigames _AnimationPopUp;



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

        if (_Jeu.name == "RythmGame")
        {
            _scoreManagerRythm.comboScore = 0;
            StartCoroutine(_Spawner.SpawnCubes());
            
        }

        _Jeu.SetActive(true);
        GameStateManager.Instance.SetState(GameState.Paused);
        _AudioSource.clip = _AudioClips[1];
        _AudioSource.Play();

    }

    public void ResultMiniGame(bool win)
    {
        if(win) GameManager.Instance.ChangeScore(50);

        if(win)
        {
            _AudioSource.clip =_AudioClips[2];
            _AudioSource.PlayDelayed(.3f);
        }
        else
        {
            _AudioSource.clip = _AudioClips[3];
            _AudioSource.PlayDelayed(.1f);
        }
        
        
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

    private void OnMouseDown()
    {
        if (_currentPatient == null || GameStateManager.Instance.CurrentGameState == GameState.Paused) return;

        OnClick();
        
        switch(_serviceSecteur)
        {
            case Services.A:
                _Selector.FolderManager.StartMinigame(_AnimationPopUp.gameObject);
                break;
            case Services.C:
                _Selector.PuzzleManager.StartMinigame(_AnimationPopUp.gameObject);
                break;
            case Services.D:
                break;
            case Services.E:
                _Selector.PhialManager.StartMinigame(_AnimationPopUp.gameObject);
                break;
        }

        _AnimationPopUp.StartPop();
    }
}
