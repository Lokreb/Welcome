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

    [SerializeField]private int _numberOfPatient = 30;
    [SerializeField]private float _timePatientSpawn_sec = 3f;
    [SerializeField]private Patient _prefab_Patient;
    [SerializeField]private ServicesManager _servicesManager;

    [SerializeField] private List<Paths> _ListChemins;

    [SerializeField]private GameObject _spawnPoint;

    [SerializeField] private List<Patient> _ListPatient;

    void Awake() {

        if (Instance != null) return;
        Instance = this;
    }

    public void Test()
    {
        List<Tween> t = DOTween.PlayingTweens();
        int nb = t.Count;
        for(int a=0;a<nb;a++)
        {
            print(t[a].intId);
        }
    }
    void Start()
    {
        //initialisation ID waypoints
        int nbPath = _ListChemins.Count;
        for(int a=0;a<nbPath;a++)
        {
            int nbWayPoint = _ListChemins[a].ListWaypoints.Count;
            for(int b=0;b<nbWayPoint;b++)
            {
                _ListChemins[a].ListWaypoints[b].ID[0] = a;
                _ListChemins[a].ListWaypoints[b].ID[1] = b;
            }
        }

        StartCoroutine(SpawnPatient());
    }

    bool EndGame = true;
    IEnumerator SpawnPatient()
    {
        while(EndGame)
        {
            if (_numberOfPatient > 0)
            {
                GeneratePatient();
                _numberOfPatient--;
            }

            yield return new WaitForSeconds(_timePatientSpawn_sec);
            if(EndGame)AvanceTapis();
        }
        
    }

    
    void GeneratePatient()
    {
        Patient p = Instantiate(_prefab_Patient, _spawnPoint.transform.position, Quaternion.identity);
        p.gameObject.transform.SetParent(_spawnPoint.transform);
        _ListPatient.Add(p);

        //Defini les services � voir
        SetServiceToSee(p);

        //verification dispo 1er waypoint + d�placement
        WayPointsValue wp = _ListChemins[0].ListWaypoints[0];
        if (wp.Dispo)
        {
            p.transform.DOMove(wp.transform.position, .5f).SetEase(Ease.Linear);
            wp.Dispo = false;
            
        }
        else
        {
            EndGame = false;
            print("perdu");
        }
    }
    void SetServiceToSee(Patient p)
    {
        int nbServices = UnityEngine.Random.Range(1, 5);
        for (int a = 0; a < nbServices; a++)
        {
            Services service = (Services)UnityEngine.Random.Range(0, (int)Services.MAX);
            p.ServiceToSee.Enqueue(service);
 
        }
    }

    public void AvanceTapis()
    {
        
        int nbPatient = _ListPatient.Count;
        for (int a = 0; a < nbPatient; a++)
        {
            NextCase(_ListPatient[a]);
        }
        _ListPatient.Remove(_patientRemove);
        _patientRemove = null;
    }

    Patient _patientRemove = null;
    int IdTweenSet = 0;
    public void NextCase(Patient p)
    {
        int[] nextWP = { p.PathIn[0], p.PathIn[1]+1};
        WayPointsValue wp = _ListChemins[p.PathIn[0]].ListWaypoints[p.PathIn[1]];

        if (p.PathIn[0] == 6 && p.PathIn[1] == 2)//Delete fin de chemin
        {
            wp.Dispo = true;
            //_ListPatient.Remove(p);
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
                p.InMiniGame = true;
                OnPatientService?.Invoke(wpNext,p);
            }
            wp.Dispo = true;
            wpNext.Dispo = false;
            p.PathIn = nextWP;
        }

        p.transform.DOMove(_ListChemins[p.PathIn[0]].ListWaypoints[p.PathIn[1]].transform.position, .4f).SetEase(Ease.Linear).SetId(IdTweenSet);
        p.TweenID = IdTweenSet;
        IdTweenSet++;
    }

    int SplitPath(Patient p)
    {
        //Split 1/5
        if (p.PathIn[0] == 0)
        {
            if (p.ServiceToSee.Count == 1) return 5;

            return 1;
        }

        //Split 2/3
        if (p.PathIn[0] == 1)
        {
            if (p.ServiceToSee.Peek() == Services.C) return 2;

            return 3;
        }

        //FailSafe
        return p.PathIn[0]++;
    }

    int MergePath(Patient p)
    {
        //Merge 4
        if (p.PathIn[0] == 2) return 4;

        //Merge 6
        if (p.PathIn[0] == 4) return 6;

        return p.PathIn[0]+1;
    }

    public void SetWayPointDispo(int[] id)
    {
        _ListChemins[id[0]].ListWaypoints[id[1]].Dispo = true;
    }
}