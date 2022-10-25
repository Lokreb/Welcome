using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    void Awake() {
        Instance = this;
    }

    void Start() {
        UpdateGameState(GameState.Playing);
    }

    public void UpdateGameState(GameState newState){
        State = newState;

        switch(newState){
            case GameState.Playing :
                break;
            case GameState.Pause :
                break;
            case GameState.Result :
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState,null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
}


public enum GameState {
    Playing,
    Minigame,
    Pause,
    Result
}