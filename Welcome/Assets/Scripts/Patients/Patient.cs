using System.Collections;
using System.Collections.Generic;

public enum PositionMap {nothing, Conveyor, Service};

[System.Serializable]
public class Patient
{
    public int ID;
    public PositionMap CurrentPos = PositionMap.nothing;
    public bool[] ServiceCompleted = {true,true,true,true,true};//5 servives
    public bool Completed = false;
    public PatientState State = PatientState.Walking;

    public Patient(List<int> toSee)
    {
        foreach(int a in toSee)
        {
            ServiceCompleted[a] = false;
        }
    }
}
