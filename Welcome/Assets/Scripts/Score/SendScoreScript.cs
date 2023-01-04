using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SendScoreScript : MonoBehaviour
{
    public dreamloLeaderBoard MyBoard;

    public GameDataScript myData;

    public GameObject ScoreLinePrefab;

    public Transform scoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            StartGetScores();
        }
    }

    public void SendScore()
    {
        MyBoard.AddScore(myData.playerName, myData.score);
    }

    public void StartGetScores()
    {
        StartCoroutine(routine: GetScores());
    }

    private IEnumerator GetScores()
    {
        while (MyBoard.ToStringArray() == null)
        {

            MyBoard.GetScores();
            yield return new WaitForSeconds(1);
        }

        DisplayScore();
    }

    private void DisplayScore()
    {
        var i = 0;

        foreach (var line in MyBoard.ToScoreArray())
        {
            i++;
            GameObject myLine = Instantiate(ScoreLinePrefab, scoreBoard);
            var r_score = myLine.GetComponent<DisplayScoreScript>();
            r_score.myName.text = line.playerName;
            r_score.myScore.text = line.score.ToString();
            if (i == 5)
            {
                break;
            }
        }
    }
}
