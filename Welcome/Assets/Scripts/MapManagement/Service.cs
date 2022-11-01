using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Service : MonoBehaviour
{
    //Intérieur = transform.position;
    public Transform[] WaitingPoints;
    //Queue.Count = waiting pos

    public Dictionary<Patient,int> PatientQueue = new Dictionary<Patient, int>();
    private int _IDQueue = 0;


    public bool AddPatient(Patient patient)
    {
        if(PatientQueue.Count <= WaitingPoints.Length)
        {
            PatientQueue.Add(patient, _IDQueue);
            _IDQueue++;
            return true;
        }

        return false;
    }

    public void RemovePatient(Patient patient)
    {
        int removedPatient = PatientQueue[patient];

        foreach(Patient p in PatientQueue.Keys)
        {
            if (PatientQueue[p] <= removedPatient) continue;

            PatientQueue[p]--;
        }

        PatientQueue.Remove(patient);
    }

    public Transform WaitingPosition()
    {
        return null;
    }
}
