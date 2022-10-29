using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]private int NumberOfPatient = 30;
    [SerializeField]private float TimePatientSpawn_sec = 2;
    [SerializeField]private Patient Prefab_Patient;
    [SerializeField]private List<DropZonePatient> _dropZone;

    [SerializeField]private GameObject _spawnPoint,_dropZoneParent;
    public List<Patient> ListPatient;
    

    void Awake() {

        if (Instance != null) return;
        Instance = this;

        //changement d'�tat -> patient change d'�tat Spawn � S1, S1 � Sx, Sx � Sy... pour d�finir le chemin
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
            Patient p = Instantiate(Prefab_Patient,_spawnPoint.transform.position,Quaternion.identity);
            p.gameObject.transform.SetParent(_spawnPoint.transform);
            ListPatient.Add(p);

            p.transform.DOMove(_dropZone[0].WayPoints[0].position,5f).SetEase(Ease.Linear);

            yield return new WaitForSeconds(TimePatientSpawn_sec);
        }
        
    }

    void PatientDropped(DropZonePatient dzp)
    {
        print(dzp.Name);
    }
}