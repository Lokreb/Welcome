using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameDataScript _gameData;
    // Start is called before the first frame update
    void Start()
    {
        newGame();
    }

    public void newGame()
    {
        _gameData.playerName = "";
        _gameData.volume = 50;
        _gameData.score = 0;
        _gameData.music = 50;
    }
 
}
