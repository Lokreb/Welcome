using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScoreScript : MonoBehaviour
{
    public GameDataScript myData;

    public GameObject score;

    // Start is called before the first frame update
    void Start()
    {
        //at the end of the game, the score of the current player is displayed above the ranking,
        //so here I simply modify the text concerned like "Votre score est 500 !".
        score.GetComponent<TextMeshProUGUI>().text = "Your score : " + myData.score + " !";
    }
}
