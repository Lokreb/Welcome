using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServicesManager : MonoBehaviour
{ 
    public Service[] Services;

    [SerializeField]private int _maxServicesToSee = 5;

    public void AddServicesToSee(Patient p)
    {
        int nbServices = Random.Range(1, _maxServicesToSee);
        int lenServices = Services.Length;
        for(int a=0; a < nbServices; a++)
        {
            p.ServicesToSee.Enqueue(Services[Random.Range(0,lenServices)]);
        }
    }

    public void CanGoTo(Patient p)
    {
        if(p.ServicesToSee.Peek().AddPatient(p))
        {
            print("added patient");
        }
    }
}
