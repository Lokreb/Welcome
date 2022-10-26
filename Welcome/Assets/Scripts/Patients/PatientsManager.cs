using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum PatientState {
    Walking,
    WaitingIn,
    WaitingOut
}

public class PatientsManager : MonoBehaviour
{
    private float _TIMEMoving = 2;

    public GameObject PatientPrefab;
    [SerializeField]private GameObject _spawnPoint;

    public List<Patient> PatientsList = new List<Patient>();
    [HideInInspector]public List<GameObject> PatientsGOList = new List<GameObject>();
    [SerializeField]private ServiceState[] _ServiceList;

    private int valueID;
    

    void Start()
    {
        PatientsList.Clear();
        valueID = 0;

        foreach(ServiceState ss in _ServiceList)
        {
            ss.OnPatientDone += MovingNext;
        }
    }

    public void Spawn()
    {
        GameObject pGO = Instantiate(PatientPrefab,new Vector3(_spawnPoint.transform.position.x,_spawnPoint.transform.position.y,0f),Quaternion.identity);
        pGO.transform.SetParent(transform);
        Patient p = new Patient();
        p.ID = valueID;
        

        PatientsList.Add(p);
        PatientsGOList.Add(pGO);

        //Verification Deplacement
        MovingTowards(0, valueID);
        valueID++;
    }

    private void MovingTowards(int service, int ID)
    {
        print("avance vers :"+service+" patient n°"+ID);
        if(_ServiceList[service].WaitingID.Count >= 5)
        {
            //Afficher un problème ou qlq chose
            Debug.Log("Service surchargé");
            return;
        }

        _ServiceList[service].WaitingID.Add(ID);
        PatientsGOList[ID].transform.DOMove(_ServiceList[service].transform.position,_TIMEMoving).SetEase(Ease.Linear);

        if(_ServiceList[service].WaitingID.Count == 1)return;

        
        StartCoroutine(killTween(_TIMEMoving - (_ServiceList[service].WaitingID.Count / 10f)));
        IEnumerator killTween(float sec)
        {
            yield return new WaitForSeconds(sec);
            DOTween.Kill(PatientsGOList[ID].transform);
        }
    }

    private void MovingNext(int id,int serviceID)
    {
        PatientsList[id].ServiceCompleted[serviceID] = true;

        for(int a=0;a<5;a++)
        {
            if(PatientsList[id].ServiceCompleted[a])continue;

            MovingTowards(a,id);
            return;
        }
    }
}
