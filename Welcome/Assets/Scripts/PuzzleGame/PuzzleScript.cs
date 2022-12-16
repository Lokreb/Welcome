using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuzzleScript
{
    public int puzzle_ID;
    public bool[] truePiecePosition = { false, false, false, false, false, false, false, false, false };

    public PuzzleScript(int truePosition)
    {
        truePiecePosition[truePosition] = true;
    }
}
