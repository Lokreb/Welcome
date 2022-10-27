using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FolderShowServices : MonoBehaviour
{
    public Image[] ParagrapheServices;

    [SerializeField]private ServicesManager _ServicesManager;

    void Start()
    {
        _ServicesManager.OnMiniGameStart += ShowServices;
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        _ServicesManager.OnMiniGameStart -= ShowServices;
    }

    private void ShowServices(ServiceState ss, Patient p)
    {
        print("ciyciy");
        gameObject.SetActive(true);

        int x =  p.ServiceCompleted.Length;
        for(int a=0; a<x;a++)
        {
            ParagrapheServices[a].color = p.ServiceCompleted[a] ? Color.green : Color.grey;
        }
    }
}
