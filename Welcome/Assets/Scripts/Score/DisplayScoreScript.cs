using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScoreScript : MonoBehaviour
{
    //To tell the prefab "ScoreLine"
    //(which is used to display the score in the leaderboard such as "PlayerName: Score")
    //that it should use the specified IUs.
    public TextMeshProUGUI myScore;
    public TextMeshProUGUI myName;
}
