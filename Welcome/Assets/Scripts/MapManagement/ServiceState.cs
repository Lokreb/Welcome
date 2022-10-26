using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceState : MonoBehaviour
{
    public int ID;
    public List<int> WaitingID = new List<int>();
    public event Action<int, int> OnPatientDone;

    public void Complete()
    {
        OnPatientDone?.Invoke(WaitingID[0],ID);
        WaitingID.RemoveAt(0);
    }
}
