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
    private float _LimiteService = 5;

    public GameObject PatientPrefab;
    [SerializeField]private GameObject _SpawnPoint;
    [SerializeField]private GameObject _LeavePoint;

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
            ss.OnPatientDone += TaskComplete;
            ss.OnPatientDoneLine += MovingCloser;
        }
    }

    void OnDestroy()
    {
        foreach(ServiceState ss in _ServiceList)
        {
            ss.OnPatientDone -= TaskComplete;
            ss.OnPatientDoneLine -= MovingCloser;
        }
    }

    public void Spawn()
    {
        GameObject pGO = Instantiate(PatientPrefab,new Vector3(_SpawnPoint.transform.position.x,_SpawnPoint.transform.position.y,0f),Quaternion.identity);
        pGO.transform.SetParent(transform);
        Patient p = new Patient();
        p.ID = valueID;
        

        PatientsList.Add(p);
        PatientsGOList.Add(pGO);

        //Verification Deplacement
        MovingTowards(0, valueID);
        valueID++;
    }

    private void TaskComplete(int id, int service, bool win)
    {
        PatientsList[id].ServiceCompleted[service] = win;

        MovingNext(id, service);
    }

    private void MovingTowards(int service, int id)
    {
        print("avance vers :"+service+" patient n°"+id);
        if(_ServiceList[service].WaitingID.Count >= _LimiteService)
        {
            //Afficher un problème ou qlq chose
            PatientsGOList[id].transform.DOMove(-_LeavePoint.transform.position,_TIMEMoving).SetEase(Ease.Linear).OnComplete(() => {
            Destroy(PatientsGOList[id]);
        
            });
            Debug.Log("Service surchargé");
            return;
        }

        _ServiceList[service].WaitingID.Add(id);

        Vector3 pos = ReducDistance(service, id);
        PatientsGOList[id].transform.DOMove(pos,_TIMEMoving).SetEase(Ease.Linear);

        /*if(_ServiceList[service].WaitingID.Count == 1)return;

        
        StartCoroutine(killTween(_TIMEMoving - (_ServiceList[service].WaitingID.Count / 10f)));
        IEnumerator killTween(float sec)
        {
            yield return new WaitForSeconds(sec);
            DOTween.Kill(PatientsGOList[id].transform);
        }*/
    }

    private void MovingToExit(int id)
    {
        PatientsGOList[id].transform.DOMove(_LeavePoint.transform.position,_TIMEMoving).SetEase(Ease.Linear).OnComplete(() => {
            Destroy(PatientsGOList[id]);
        
        });
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

        MovingToExit(id);
    }

    private void MovingCloser(int service, int id)
    {
        PatientsGOList[id].transform.DOMove(_ServiceList[service].transform.position,_TIMEMoving).SetEase(Ease.Linear);
        
        StartCoroutine(killTween(_TIMEMoving - (_ServiceList[service].WaitingID.Count / 10f)));
        IEnumerator killTween(float sec)
        {
            yield return new WaitForSeconds(sec);
            DOTween.Kill(PatientsGOList[id].transform);
        }
    }

    private Vector3 ReducDistance(int service, int id)
    {
        Vector3 arrive = _ServiceList[service].transform.position;

        float departx = PatientsGOList[id].transform.position.x;
        float departy = PatientsGOList[id].transform.position.y;

        float difx = (arrive.x - departx)/2f;
        float dify = (arrive.y - departy)/2f;

        float percentage = (_LimiteService - (_ServiceList[service].WaitingID.Count-1)) / _LimiteService;

        float x = departx + difx + (difx * percentage);
        float y = departy + dify + (dify * percentage);

        return new Vector3(x,y,0);
    }
}
