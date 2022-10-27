using System.Collections.Generic;
using UnityEngine;
using System.Collections;

//allows to add a Unity option to create the GameData object
[CreateAssetMenu(menuName = "Create GameData")]

public class GameDataScript : ScriptableObject
{
    //The data saved here is the player's name and score and key codes and the volume's value.
    public string playerName;

    public int score = 0;

    public int scoreSortGame = 0;

    public int volume = 100;
    public int music = 100;

    public List<int> idCible = new List<int>();

    public int count = 0;

    private void OnEnable()
    {

    }

}
