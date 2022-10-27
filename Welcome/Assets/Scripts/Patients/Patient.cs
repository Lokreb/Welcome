using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Patient
{
    public int ID;
    public bool[] ServiceCompleted = {false,true,true,true,true};//5 servives
    public bool Completed = false;

    public Patient()//add difficulty
    {

        for(int a=1;a<5;a++)
        {
            ServiceCompleted[a] = (.5 >= Random.value);
        }
        
    }

    //Remplacement
}
