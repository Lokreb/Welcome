using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SettingsControllerScript : MonoBehaviour
{
    [Header("Connexion Settings")]
    [SerializeField] private TMP_InputField user_prenom;
    [SerializeField] private GameDataScript gameData;

    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private GameObject confirmationPrompt = null;
    [SerializeField] private int defaultVolume = 100;

    [Header("BGM settings")]
    [SerializeField] private TMP_Text volumeBGMValue = null;
    [SerializeField] private Slider bgmSlider = null;
    [SerializeField] private int defaultBGM = 100;

    public void ExitGame()
    {
        //SaveSystem.SaveData(gameData);
        Application.Quit();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SetVolume()
    {
        //Here, I update the text of the slider according to the value of the bar.
        //And I update the sound volume of the game.
        //AudioListener.volume = volumeSlider.value/100f;
        volumeTextValue.text = volumeSlider.value.ToString();
    }

    public void SetMusic()
    {
        volumeBGMValue.text = bgmSlider.value.ToString();
    }

    public void VolumeApply()
    {
        //I save the player's preferences about the sound.
        //PlayerPrefs.SetFloat("masterVolume", AudioListener.volume/100f);
        StartCoroutine(ConfirmationBox());
        gameData.volume = (int)volumeSlider.value;
    }

    public void MusicApply()
    {
        PlayerPrefs.SetFloat("masterMusic", bgmSlider.value / 100f);
        StartCoroutine(ConfirmationBox());
        gameData.music = (int)bgmSlider.value;
    }

    public void ModificationApply()
    {
        VolumeApply();
        MusicApply();
        SetName();
        //SaveSystem.SaveData(gameData);
    }

    //We reset the gameData and the audio.
    public void ResetButton()
    {

        //AudioListener.volume = defaultVolume/100f;
        volumeSlider.value = defaultVolume;
        volumeTextValue.text = defaultVolume.ToString();
        VolumeApply();

        bgmSlider.value = defaultBGM;
        volumeBGMValue.text = defaultBGM.ToString();
        MusicApply();
    }

    //When we make a "return", I erase what the player has written
    public void BackButton()
    {
        user_prenom.text = "";
    }

    public void LoadVolumeAndMusic()
    {
        volumeSlider.value = gameData.volume;
        volumeTextValue.text = gameData.volume.ToString();
        bgmSlider.value = gameData.music;
        volumeBGMValue.text = gameData.music.ToString();
    }

    //We update the display of the nickname in the parameters
    public void Update_username()
    {
        user_prenom.text = gameData.playerName;
    }

    //Displays a "loading" box when the new settings are applied.
    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }

    //The player's name is updated according to what he has entered.
    public void SetName()
    {
        if (user_prenom.text != "")
            gameData.playerName = user_prenom.text;

        PlayerPrefs.SetString("Username", user_prenom.text);
    }

}