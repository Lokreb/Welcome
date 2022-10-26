using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceState : MonoBehaviour
{
    public List<int> WaitingID = new List<int>();

    public void Complete()
    {
        WaitingID.RemoveAt(0);
    }
}
