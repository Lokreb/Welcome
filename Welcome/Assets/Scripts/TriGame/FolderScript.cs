using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FolderScript
{
    public int folder_ID;
    public bool[] trueService = { false, false, false, false, false };
    public int service_ID;

    public FolderScript(int valueService)
    {
        service_ID = valueService;
        trueService[service_ID] = true;
    }
}
