using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]private int NumberOfPatient = 30;
    [SerializeField]private float TimePatientSpawn_sec = 2;
    [SerializeField]private Patient2 Prefab_Patient;
    [SerializeField]private List<DropZonePatient> _dropZone;

    [SerializeField]private GameObject _spawnPoint,_dropZoneParent;
    public List<Patient2> ListPatient;
    

    void Awake() {
        Instance = this;

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
            Patient2 p = Instantiate(Prefab_Patient,_spawnPoint.transform.position,Quaternion.identity);
            p.gameObject.transform.SetParent(_spawnPoint.transform);
            ListPatient.Add(p);


            yield return new WaitForSeconds(TimePatientSpawn_sec);
        }
        
    }

    void PatientDropped(DropZonePatient dzp)
    {
        print(dzp.Name);
    }
}