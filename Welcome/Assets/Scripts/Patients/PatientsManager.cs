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
    }

    public void Spawn()
    {
        GameObject pGO = Instantiate(PatientPrefab,new Vector3(_spawnPoint.transform.position.x,_spawnPoint.transform.position.y,0f),Quaternion.identity);
        pGO.transform.SetParent(transform);
        Patient p = PatientParameters();
        p.ID = valueID;
        

        PatientsList.Add(p);
        PatientsGOList.Add(pGO);

        //Verification Deplacement
        MovingTowards(0, pGO.transform);

        valueID++;
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

    /*private void Moving(Transform go)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(go.transform.DOMove(_ServiceList[0].transform.position,_TIMEMoving));

        for(int a=1;a<5;a++)
        {
            if(!PatientsList[valueID].ServiceCompleted[a])continue;

            sequence.Append(go.transform.DOMove(_ServiceList[a].transform.position,_TIMEMoving));
        }
    }*/

    private void MovingTowards(int service, Transform go)
    {
        if(_ServiceList[service].WaitingID.Count == 0)
        {
            _ServiceList[service].WaitingID.Add(valueID);
            go.DOMove(_ServiceList[service].transform.position,_TIMEMoving).SetEase(Ease.Linear);
            return;
        }

        if(_ServiceList[service].WaitingID.Count >= 5)
        {
            //Afficher un problème ou qlq chose
            Debug.Log("Service surchargé");
            return;
        }

        _ServiceList[service].WaitingID.Add(valueID);
        go.DOMove(_ServiceList[service].transform.position,_TIMEMoving).SetEase(Ease.Linear);
        StartCoroutine(killTween(_TIMEMoving - (_ServiceList[service].WaitingID.Count / 10f)));

        IEnumerator killTween(float sec)
        {
            print(sec);
            yield return new WaitForSeconds(sec);
            DOTween.Kill(go);
        }
    }
}
