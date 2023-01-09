using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public AudioSource hitSFX;
    public AudioSource missSFX;
    public TextMeshProUGUI scoreText;
    public int comboScore;
    [SerializeField] private GameDataScript _gameData;
    void Start()
    {
        Instance = this;
        comboScore = 0;
    }
    public static void Hit()
    {
        Instance.comboScore += 1;
        Instance.hitSFX.Play();
    }
    public static void Miss()
    {
        //Instance.missSFX.Play();
    }
    private void Update()
    {
        scoreText.text = comboScore.ToString();
        scoreText.canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.None;
        Instance.hitSFX.volume = _gameData.volume;
        Instance.missSFX.volume = _gameData.volume;
    }
}