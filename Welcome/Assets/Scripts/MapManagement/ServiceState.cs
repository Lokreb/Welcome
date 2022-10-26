using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceState : MonoBehaviour
{
    public int ID;
    public List<int> WaitingID = new List<int>();
    public event Action<int, int> OnPatientDone;
    public event Action<int, int> OnPatientDoneLine;

    public void Complete()
    {
        OnPatientDone?.Invoke(WaitingID[0],ID);
        
        int cpt = WaitingID.Count;

        for(int a=1;a<cpt;a++)
        {
            OnPatientDoneLine?.Invoke(ID,WaitingID[a]);
        }

        WaitingID.RemoveAt(0);
    }
}
