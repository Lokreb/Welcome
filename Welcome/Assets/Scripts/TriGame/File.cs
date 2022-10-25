using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class File
{
    public int file_ID;
    public bool[] trueService = { false, false, false, false };
    public bool completed = false;
    public int service_ID;

    public File(int valueService)
    {
        service_ID = valueService;
        trueService[service_ID] = true;
    }
}
