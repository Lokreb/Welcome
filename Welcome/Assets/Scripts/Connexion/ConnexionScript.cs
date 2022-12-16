using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
public class ConnexionScript : MonoBehaviour
{
    [Header("Username zone")]
    [SerializeField] private TMP_InputField userName;
    [Header("Game Data")]
    [SerializeField] private GameDataScript gameData;

    public void SetName()
    {
        gameData.playerName = userName.text;
    }
}
