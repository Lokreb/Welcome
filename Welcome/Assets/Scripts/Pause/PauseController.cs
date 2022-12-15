using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseView;
    [SerializeField] private GameDataScript _gameData;

    [Header("Volume Settings")]
    [SerializeField] private Slider _volumeSlider = null;
    [SerializeField] private GameObject _confirmationPrompt = null;
    [SerializeField] private int _defaultVolume = 50;

    [Header("BGM settings")]
    [SerializeField] private Slider _bgmSlider = null;
    [SerializeField] private int _defaultBGM = 50;


    private void Start()
    {

    }

    public void Pause()
    {
        GameState currentGameState = GameStateManager.Instance.CurrentGameState;
        GameState newGameState = currentGameState == GameState.Gameplay
        ? GameState.Paused
        : GameState.Gameplay;

        GameStateManager.Instance.SetState(newGameState);

        switch (newGameState)
        {
            case GameState.Paused:
                _pauseView.SetActive(true);
                break;
            case GameState.Gameplay:
                _pauseView.SetActive(false);
                break;
        }
    }

    public void Replay()
    {
        GameState currentGameState = GameStateManager.Instance.CurrentGameState;

        currentGameState = GameState.Gameplay;

        GameStateManager.Instance.SetState(currentGameState);
    }

    public void VolumeApply()
    {
        //I save the player's preferences about the sound.
        //PlayerPrefs.SetFloat("masterVolume", AudioListener.volume/100f);
        StartCoroutine(ConfirmationBox());
        _gameData.volume = (int)_volumeSlider.value;
    }

    public void MusicApply()
    {
        //PlayerPrefs.SetFloat("masterMusic", bgmSlider.value);
        StartCoroutine(ConfirmationBox());
        _gameData.music = (int)_bgmSlider.value;
    }

    public void ModificationApply()
    {
        VolumeApply();
        MusicApply();
    }


    //Displays a "loading" box when the new settings are applied.
    public IEnumerator ConfirmationBox()
    {
        _confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        _confirmationPrompt.SetActive(false);
    }
}
