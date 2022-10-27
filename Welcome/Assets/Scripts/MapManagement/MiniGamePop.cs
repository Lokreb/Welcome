using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGamePop : MonoBehaviour
{
    public event Action<bool,MiniGamePop> Result;

    public void OnClick(bool result)
    {
        Result?.Invoke(result, this);
        Destroy(this.gameObject);
    }
}
