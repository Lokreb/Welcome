using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]private int _numberOfPatient = 30;
    [SerializeField]private float _timePatientSpawn_sec = 2;
    [SerializeField]private Patient _prefab_Patient;
    [SerializeField]private ServicesManager _servicesManager;

    [SerializeField]private List<DropZonePatient> _dropZone;

    [SerializeField]private GameObject _spawnPoint,_dropZoneParent;
    public List<Patient> ListPatient;
    

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
            ListPatient.Add(p);

            _servicesManager.AddServicesToSee(p);
            _servicesManager.CanGoTo(p);
            //p.transform.DOMove(_dropZone[0].WayPoints[0].position,5f).SetEase(Ease.Linear);

            yield return new WaitForSeconds(_timePatientSpawn_sec);
        }
        
    }

    void PatientDropped(DropZonePatient dzp)
    {
        print(dzp.Name);
    }
}