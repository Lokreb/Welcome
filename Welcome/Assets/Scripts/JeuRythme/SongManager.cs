using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System;

public class SongManager : MonoBehaviour
{
    public static bool _isPlayed;
    public static bool _isCompleted;
    public static bool _winner;
    private bool _flemme;

    [SerializeField] private Service _Service;
    [SerializeField] public GameObject _Jeu;
    //[SerializeField] public GameObject _lane;


    // Start is called before the first frame update
    void Start()
    {
        _isCompleted = false;
        
    }



    public void ThisIsTheEnd()
    {
       
    }

    public void NewGame() {
        
    }



    void Update()
    {
       ThisIsTheEnd();
    }
}