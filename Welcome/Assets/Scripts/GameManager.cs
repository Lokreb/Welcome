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

    public GameObject SpawnPoint;
    public List<Patient2> ListPatient;

    void Awake() {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnPatient());
        
    }

    IEnumerator SpawnPatient()
    {
        while(true)
        {
            Patient2 p = Instantiate(Prefab_Patient,SpawnPoint.transform.position,Quaternion.identity);
            p.gameObject.transform.SetParent(SpawnPoint.transform);
            ListPatient.Add(p);


            yield return new WaitForSeconds(TimePatientSpawn_sec);
        }
        
    }
}