using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServicesManager : MonoBehaviour
{
    public ServiceState[] Services;
    public GameObject[] ParentsUI;
    public GameObject Background;
    public GameObject Folder;

    public event EventHandler OnWaitingService;

    public void ShowOnClick(ServiceState sc)
    {
        GameObject bg = Instantiate(Background,new Vector3(transform.position.x-260f,transform.position.y,0f),Quaternion.identity);
        GameObject folder =Instantiate(Folder,new Vector3(transform.position.x+620f,transform.position.y,0f),Quaternion.identity);

        bg.transform.parent = ParentsUI[0].transform;
        folder.transform.parent = ParentsUI[1].transform;
    }


}
