using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientsManager : MonoBehaviour
{
    public GameObject PatientPrefab;
    [SerializeField]private GameObject _spawnPoint;

    public List<Patient> PatientsList = new List<Patient>();
    [HideInInspector]public List<GameObject> PatientsGOList = new List<GameObject>();
    [SerializeField]private GameObject[] _ServiceList;

    private int valueID;

    void Start()
    {
        PatientsList.Clear();
        valueID = 0;

    }

    public void Spawn()
    {
        GameObject pGO = Instantiate(PatientPrefab,new Vector3(_spawnPoint.transform.position.x,_spawnPoint.transform.position.y,0f),Quaternion.identity);
        pGO.transform.parent = transform;
        Patient p = PatientParameters();
        p.ID = valueID;
        valueID++;

        PatientsList.Add(p);
        PatientsGOList.Add(pGO);
    }

    private Patient PatientParameters()
    {
        List<int> serviceToSee = new List<int>();
        serviceToSee.Add(0);

        //random service to see
        for(int a=1;a<5;a++)
        {
            int b = Random.Range(1,5);

            if(serviceToSee.Contains(b))continue;

            serviceToSee.Add(b);
        }

        Patient p = new Patient(serviceToSee);
        return p;
    }
}
