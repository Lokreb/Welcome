using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhialScript
{
    public int phial_ID;
    public int value_response;
    public bool completed = false;

    public PhialScript(int value)
    {
        value_response = value;
    }
}
