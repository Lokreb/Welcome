using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Jeu_GeneralFonction : MonoBehaviour
{
    public event Action<bool> OnGameResponse;

    public void Result(bool win)
    {
        OnGameResponse?.Invoke(win);
    }
}
