using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServicesManager : MonoBehaviour
{
    public ServiceState[] Services;
    public GameObject[] ParentsUI;
    public MiniGamePop Background;

    [SerializeField]private PatientsManager _PatientsManager;

    public event Action<ServiceState, Patient> OnMiniGameStart;

    private ServiceState _sc;

    public void ShowOnClick(ServiceState sc)
    {
        _sc = sc;

        GameObject bg = Instantiate(Background.gameObject,new Vector3(transform.position.x-260f,transform.position.y,0f),Quaternion.identity);

        bg.GetComponent<MiniGamePop>().Result += ResultMiniGame;

        OnMiniGameStart?.Invoke(sc, _PatientsManager.PatientsList[sc.WaitingID[0]]);

        bg.transform.SetParent(ParentsUI[0].transform);
        
    }

    public void ResultMiniGame(bool win, MiniGamePop pop)
    {
        pop.Result -= ResultMiniGame;
        _sc.Complete(win);
    }


}
