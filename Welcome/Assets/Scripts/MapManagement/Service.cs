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

    [SerializeField] private Queue<Patient> _currentPatient = new Queue<Patient>();

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
    public event Action OnMultiplicatorChange;
    public event Action<int> OnPatientChange;

    bool _patientIn = false;
    public float Dur�eTraitement = 300f;
    float _dur�eTraitement;
    
    public float VitesseMultiplicator = 1f;
    public float VitesseDur�e = 300f;

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
            _currentPatient.Enqueue(p);
            OnPatientChange?.Invoke(_currentPatient.Count);

            if(_currentPatient.Count == 1)
            {
                _PopupImage.SetActive(true);
                _finishBar.gameObject.SetActive(true);
                _AudioSource.clip = _AudioClips[0];
                _AudioSource.Play();
                _patientIn = true;
                _dur�eTraitement = Dur�eTraitement;
            }
        }
    }

    void Traitement()
    {
        _dur�eTraitement -= 1 * GameManager.Instance.TimerSpeed * VitesseMultiplicator;

        OnFillBar?.Invoke(1 - _dur�eTraitement / Dur�eTraitement);

        if (_dur�eTraitement<=0)
        {
            _currentPatient.Peek().EndMiniGame(true, _serviceSecteur);
            _currentPatient.Dequeue();

            OnPatientChange?.Invoke(_currentPatient.Count);

            _patientIn = _currentPatient.Count != 0;

            _PopupImage.SetActive(_patientIn);
            _finishBar.gameObject.SetActive(_patientIn);

            _dur�eTraitement = Dur�eTraitement;
        }

        
    }
    private void FixedUpdate()
    {
        if (GameStateManager.Instance.CurrentGameState == GameState.Paused) return;

        if (_patientIn) Traitement();

        VitesseDur�e = VitesseDur�e >= 0 ? VitesseDur�e - 1 : 0;

        if (VitesseDur�e == 0)
        {
            VitesseMultiplicator = 1f;
            OnMultiplicatorChange?.Invoke();
        }
    }

    public void OnClick()
    {
        if (_currentPatient.Count == 0) return;

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
            VitesseMultiplicator = 2f;
            VitesseDur�e = 300f;

            OnMultiplicatorChange?.Invoke();
        }
        else
        {
            _AudioSource.clip = _AudioClips[3];
            _AudioSource.PlayDelayed(.1f);
        }
        
        
        //_currentPatient.Peek().EndMiniGame(win, _serviceSecteur);
        _ResultScreen.gameObject.SetActive(true);
        _ResultScreen.Result(win,this);
    }

    public void EndResult()
    {
        _ResultScreen.gameObject.SetActive(false);
        _Jeu.SetActive(false);
        _currentPatient.Dequeue();
        _PopupImage.SetActive(false);
        GameManager.Instance.InMinigame(false);
        GameStateManager.Instance.SetState(GameState.Gameplay);
    }

    private void OnMouseDown()
    {
        if (_currentPatient.Count == 0 || GameStateManager.Instance.CurrentGameState == GameState.Paused) return;

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
