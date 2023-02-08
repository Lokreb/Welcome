using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum Services {A,C,D,E,MAX};

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event Action<WayPointsValue,Patient> OnPatientService;
    public event Action<int> OnHumorChange;
    public event Action OnPatientEnd;
    public event Action OnTimerChange;
    public event Action OnScoreChange;
    public event Action<bool> OnMiniGamePlaying;
    public event Action<bool> OnMoveBarriere;

    [Header("Game Balance")]
    public float Timer = 600;
    [HideInInspector]public float _timerStart;
    public AnimationCurve DifficultyInTime;
    [SerializeField] private int _HumorValue = 100;
    [SerializeField] private float _SpawnRate = 120f;
    [SerializeField] private float _SpawnRateVariance = 10f;
    [SerializeField] private float _ConveyorDelai = 60f;
    

    [Header("Game Running Settings")]
    private bool _inMinigame = false;
    [Range(1f,2f)]public float TimerSpeed = 1f;
    private int _timerTotal = 0;
    int[] order = { 6, 4, 2, 3, 1, 5, 0 };
    [SerializeField] private List<Patient> _listPatient;


    [HideInInspector]
    public int PatientFailed { get; private set; } = 0;
    public int PatientDone { get; private set; } = 0;
    public float Score { get; private set; } = 0;
    
    [Header("GameObject to link")]
    [SerializeField] private List<Paths> _ListChemins;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private Patient _prefab_Patient;
    [SerializeField] private GameObject[] _CharactersPrefabAnimations;
    [SerializeField] private GameObject _EndPopUp;
    [SerializeField] private GameDataScript _GameData;

    private int[] _LastWP = {0,0};


    void Awake() {

        if (Instance != null) return;
        Instance = this;
    }

    void OrderedPatient()
    {
        List<Patient> temp = new List<Patient>();
        //order
        
        //parcours WayPoints sortie -> entree
        foreach(int a in order)
        {
            int nb = _ListChemins[a].ListWaypoints.Count -1;
            for (int b = nb; b >= 0; b--)
            {
                Patient p = _ListChemins[a].ListWaypoints[b].CurrentPatient;

                if (p == null) continue;

                temp.Add(p);
            }
        }

        if (_listPatient.Count != temp.Count)
        {
            temp.Add(_listPatient[_listPatient.Count-1]);
        }
        _listPatient = new List<Patient>(temp);

    }

    float _spawnRate;
    void Start()
    {
        OnTimerChange?.Invoke();
        //initialisation ID waypoints
        int nbPath = _ListChemins.Count;
        for(int a=0;a<nbPath;a++)
        {
            int nbWayPoint = _ListChemins[a].ListWaypoints.Count;
            for(int b=0;b<nbWayPoint;b++)
            {
                _ListChemins[a].ListWaypoints[b].ID[0] = a;
                _ListChemins[a].ListWaypoints[b].ID[1] = b;
                _LastWP[1] = b;
            }
            _LastWP[0] = a;
        }

        _spawnRate = _SpawnRate;
        _timerStart = Timer;
        GameStateManager.Instance.SetState(GameState.Paused);
        GeneratePatient();
    }

    float _TimerSeconds = 0;
    float _conveyorCounter,_spawnCounter;
    private void FixedUpdate()
    {
        if (GameStateManager.Instance.CurrentGameState != GameState.Gameplay) return;

        _TimerSeconds += 1*TimerSpeed;
        _conveyorCounter += 1 * TimerSpeed;
        _spawnCounter += 1 * TimerSpeed;

        

        //Chaque in game seconde Timer - 1
        if (_TimerSeconds / 60 >= _timerTotal)
        {
            TimerSpeed = DifficultyInTime.Evaluate(1 - Timer/_timerStart);
            _timerTotal++;

            Timer--;

            if (Timer <= 0)
            {
                _GameData.score = (int)Score;
                GameStateManager.Instance.SetState(GameState.Paused);
                Timer = 0;
                _EndPopUp.SetActive(true);
            }

        }
        
        //Spawn patient
        if(_spawnCounter >= _SpawnRate)
        {
            _spawnCounter = 0;
            _SpawnRate = UnityEngine.Random.Range(_spawnRate-_SpawnRateVariance,_spawnRate+_SpawnRateVariance);
            GeneratePatient();
        }

        //Avance tapis every Speed
        if (_conveyorCounter >= _ConveyorDelai)
        {
            _conveyorCounter = 0;
            AvanceTapis();
        }
        
        OnTimerChange?.Invoke();

    }
    int choice = 0;
    void GeneratePatient()
    {
        Patient p = Instantiate(_prefab_Patient, _spawnPoint.transform.position, Quaternion.identity);
        p.gameObject.transform.SetParent(_spawnPoint.transform);
        _listPatient.Add(p);

        //int choice = UnityEngine.Random.Range(0,_CharactersPrefabAnimations.Length);
        GameObject go = Instantiate(_CharactersPrefabAnimations[choice], p.transform.position, Quaternion.identity);
        go.transform.localPosition = new Vector2(go.transform.localPosition.x, go.transform.localPosition.y-40f);
        go.transform.SetParent(p.transform);
        p.AnimatorUIScript = go.GetComponent<AnimatorUI>();

        p.SetServiceToSee();
        choice = choice == _CharactersPrefabAnimations.Length-1 ? 0 : choice+1 ;
    }

    bool _onePatientFinish;
    public void AvanceTapis()
    {
        _onePatientFinish = false;
        OrderedPatient();
        int nbPatient = _listPatient.Count;
        for (int a = 0; a < nbPatient; a++)
        {
            NextCase(_listPatient[a]);
        }
        _listPatient.Remove(_patientRemove);
        _patientRemove = null;
        OnMoveBarriere?.Invoke(_onePatientFinish);
    }

    Patient _patientRemove = null;
    int IdTweenSet = 0;
    public void NextCase(Patient p)
    {
        int[] nextWP = { p.PathIn[0], p.PathIn[1]+1};
        
        //spawn
        if (p.PathIn[1] == -1)
        {
            p.PathIn[1] = 0;

            if (!_ListChemins[0].ListWaypoints[0].Dispo)
            {
                _GameData.score = (int)Score;
                GameStateManager.Instance.SetState(GameState.Paused);
                _EndPopUp.SetActive(true);
                return;
            }
        }

        WayPointsValue wp = _ListChemins[p.PathIn[0]].ListWaypoints[p.PathIn[1]];
        wp.CurrentPatient = p;

        if (p.PathIn[0] == _LastWP[0] && p.PathIn[1] == _LastWP[1])//Delete fin de chemin
        {
            if (p.ServiceToSee.Count > 0)
            {
                PatientFailed++;
                ChangeHumor(p.ServiceToSee.Count * -5);
            }
            else
            {
                ChangeScore(200);
                PatientDone++;
            }
            OnPatientEnd?.Invoke();

            wp.Dispo = true;
            wp.CurrentPatient = null;
            _patientRemove = p;
            Destroy(p.gameObject);
            return;
        }

        if (wp.RoadSplit)
        {
            nextWP[0] = SplitPath(p);
            nextWP[1] = 0;
        }

        if (wp.RoadMerge)
        {
            nextWP[0] = MergePath(p);
            nextWP[1] = 0;
        }

        if (wp.Service && p.InMiniGame)
        {
            nextWP[0] = p.PathIn[0];
            nextWP[1] = p.PathIn[1];
        }

        WayPointsValue wpNext = _ListChemins[nextWP[0]].ListWaypoints[nextWP[1]];

        if (wpNext.Dispo)
        {
            if(wpNext.Service)
            {
                if(p.ServiceToSee.Count > 0)
                {
                    if(wpNext.ServiceAffiliated == p.ServiceToSee.Peek())
                    {
                        
                        wp.Dispo = true;
                        wp.CurrentPatient = null;
                        wpNext.Dispo = false;
                        wpNext.CurrentPatient = p;
                        p.PathIn = nextWP;
                        p.AttenteInGame();
                        OnPatientService?.Invoke(wpNext, p);
                    }
                }
            }else
            {
                wp.Dispo = true;
                wp.CurrentPatient = null;
                wpNext.Dispo = false;
                wpNext.CurrentPatient = p;
                p.PathIn = nextWP;
            }

        }

        Vector2 position = new Vector2(_ListChemins[p.PathIn[0]].ListWaypoints[p.PathIn[1]].transform.position.x, _ListChemins[p.PathIn[0]].ListWaypoints[p.PathIn[1]].transform.position.y+33f);
        p.transform.DOMove(position, (_ConveyorDelai) / 60 / TimerSpeed).SetEase(Ease.Linear).SetId(IdTweenSet);
        p.TweenID = IdTweenSet;
        IdTweenSet++;
        if (IdTweenSet == 1000) IdTweenSet = 0;

        if(p.ServiceToSee.Count == 0)_onePatientFinish = true;
    }

    int SplitPath(Patient p)
    {
        //Split 2/3
        if (p.PathIn[0] == 1)
        {
            if (p.ServiceToSee.Count == 0) return 3;

            switch (p.ServiceToSee.Peek())
            {
                case Services.C:
                    return 2;
                default:
                    return 3;
            }
        }

        //Split 4/6
        if (p.PathIn[0] == 4)
        {
            if (p.ServiceToSee.Count == 0) return 6;
        }

        //failsafe
        return p.PathIn[0]+1;
    }

    int MergePath(Patient p)
    {
        //Merge 2-3
        if (p.PathIn[0] == 2) return 4;

        //Merge 6
        if (p.PathIn[0] == 5) return 1;

        return p.PathIn[0]+1;
    }

    public void SetWayPointDispo(int[] id)
    {
        WayPointsValue wp = _ListChemins[id[0]].ListWaypoints[id[1]];
        wp.Dispo = true;
        wp.CurrentPatient = null;
    }

    public void ChangeHumor(int value)
    {
        ChangeScore(value*10);

        _HumorValue += value;
        if (_HumorValue <= 0)
        {
            _HumorValue = 0;
            GameStateManager.Instance.SetState(GameState.Paused);
            _EndPopUp.SetActive(true);
        }
        OnHumorChange?.Invoke(_HumorValue);
    }

    public void ChangeScore(int value)
    {

        Score += value;
        OnScoreChange?.Invoke();
    }

    public void InMinigame(bool playing)
    {
        _inMinigame = playing;

        OnMiniGamePlaying(playing);
    }

    public void TutoCheck()
    {
        GameStateManager.Instance.SetState(GameState.Gameplay);
    }
}