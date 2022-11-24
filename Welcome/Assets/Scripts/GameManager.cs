using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum Services {A,C,D,E,MAX};
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]private int _numberOfPatient = 30;
    [SerializeField]private float _timePatientSpawn_sec = 3;
    [SerializeField]private Patient _prefab_Patient;
    [SerializeField]private ServicesManager _servicesManager;

    [SerializeField]private List<DropZonePatient> _dropZone;
    [SerializeField] private List<Paths> _ListChemins;

    [SerializeField]private GameObject _spawnPoint,_dropZoneParent;

    [SerializeField] private List<Patient> _ListPatient;

    void Awake() {

        if (Instance != null) return;
        Instance = this;

        //changement d'état -> patient change d'état Spawn à S1, S1 à Sx, Sx à Sy... pour définir le chemin
        foreach(DropZonePatient dzp in _dropZone)
        {
            dzp.OnDropPatient += PatientDropped;
        }
    }

    void OnDestroy()
    {
        foreach(DropZonePatient dzp in _dropZone)
        {
            dzp.OnDropPatient -= PatientDropped;
        }
    }

    void Start()
    {
        StartCoroutine(SpawnPatient());  
    }

    IEnumerator SpawnPatient()
    {
        while(true)
        {
            Patient p = Instantiate(_prefab_Patient,_spawnPoint.transform.position,Quaternion.identity);
            p.gameObject.transform.SetParent(_spawnPoint.transform);
            _ListPatient.Add(p);

            //_servicesManager.AddServicesToSee(p);
            //_servicesManager.CanGoTo(p);
            SetServiceToSee(p);
            p.transform.DOMove(_ListChemins[0].ListWaypoints[0].transform.position, 1f).SetEase(Ease.Linear);

            yield return new WaitForSeconds(_timePatientSpawn_sec);
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
        int nbPatient = _ListPatient.Count-1;
        for (int a = nbPatient; a >= 0; a--)
        {
            NextCase(_ListPatient[a]);
        }
    }

    public void NextCase(Patient p)
    {
        int[] nextWP = { p.PathIn[0], p.PathIn[1]+1};
        WayPointsValue wp = _ListChemins[p.PathIn[0]].ListWaypoints[p.PathIn[1]];

        if (p.PathIn[0] == 6 && p.PathIn[1] == 2)
        {
            wp.Dispo = true;
            _ListPatient.Remove(p);
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
        
        WayPointsValue wpNext = _ListChemins[nextWP[0]].ListWaypoints[nextWP[1]];
        
        if (wpNext.Dispo)
        {
            wp.Dispo = true;
            wpNext.Dispo = false;
            p.PathIn = nextWP;
        }

        p.transform.DOMove(_ListChemins[p.PathIn[0]].ListWaypoints[p.PathIn[1]].transform.position, .5f).SetEase(Ease.Linear);
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

    void PatientDropped(DropZonePatient dzp)
    {
        print(dzp.Name);
    }
}