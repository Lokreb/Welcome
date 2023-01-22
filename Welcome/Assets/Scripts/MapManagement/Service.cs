using System.Collections;
using System.Collections.Generic;
using System;
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
    [SerializeField] private TrueValueManager _spritesManagerRythm;

    [SerializeField] private AudioClip[] _AudioClips;
    [SerializeField] private AudioSource _AudioSource;

    [SerializeField] SelectorMinigames _Selector;
    [SerializeField] AnimationPopUpMinigames _AnimationPopUp;

    [SerializeField] GameDataScript _gameData;

    [SerializeField] FinishBar _finishBar;
    public event Action<float> OnFillBar;

    bool _patientIn = false;
    public float DuréeTraitement = 300f;
    float _duréeTraitement;

    void Start()
    {
        GameManager.Instance.OnPatientService += PatientArrive;
        OnFillBar += _finishBar.SetFill;
        _Jeu.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPatientService -= PatientArrive;
        OnFillBar -= _finishBar.SetFill;
    }

    private void Update()
    {
        _AudioSource.volume = _gameData.volume;
    }

    
    private void PatientArrive(WayPointsValue wp, Patient p)
    {
        if (_wpService.ID == wp.ID)
        {
            _currentPatient = p;
            _PopupImage.SetActive(true);
            _finishBar.gameObject.SetActive(true);
            _AudioSource.clip = _AudioClips[0];
            _AudioSource.Play();
            _patientIn = true;
            _duréeTraitement = DuréeTraitement;
        }
        
        
    }

    void Traitement()
    {
        _duréeTraitement -= 1 * GameManager.Instance.TimerSpeed;

        OnFillBar?.Invoke(1 - _duréeTraitement / DuréeTraitement);

        if (_duréeTraitement<=0)
        {
            _patientIn = false;
            _currentPatient.EndMiniGame(true, _serviceSecteur);
            _currentPatient = null;
            _PopupImage.SetActive(false);
            _finishBar.gameObject.SetActive(false);
        }

        
    }
    private void FixedUpdate()
    {
        if (GameStateManager.Instance.CurrentGameState == GameState.Paused) return;

        if (_patientIn) Traitement();
    }

    public void OnClick()
    {
        if (_currentPatient == null) return;

        GameManager.Instance.InMinigame(true);

        if (_Jeu.name == "RythmGame")
        {
            //All important values of the mini rhythm game are reset.
            _Spawner._gameStarted = false;
            _Spawner.numberStillAlive = 1;
            _Spawner._scoreMax = 0;
            _spritesManagerRythm._trueSpritesList.Clear();
            _spritesManagerRythm._trueValueList.Clear();
            _spritesManagerRythm.AssignTrueValue();
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
        if (win) GameManager.Instance.ChangeScore(50);

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
