using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public AudioSource audioSource;
    public Lane[] lanes;
    public float songDelayInSeconds;
    public double marginOfError; // in seconds

    public int inputDelayInMilliseconds;


    public string fileLocation;
    public float noteTime;
    public float noteSpawnY;
    public float noteTapY;
    public float noteDespawnY
    {
        get
        {
            return noteTapY - (noteSpawnY - noteTapY);
        }
    }

    public static bool _isPlayed;
    public static bool _isCompleted;
    public static bool _winner;
    private bool _flemme;

    [SerializeField] private Service _Service;
    [SerializeField] private Lane _Lane;
    [SerializeField] public GameObject _Jeu;
    [SerializeField] public GameObject _lane;
    [SerializeField] private ScoreManager _SM;

    public static MidiFile midiFile;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _isPlayed = false;
        _isCompleted = false;
        ReadFromFile();
        _winner = false;
        
    }

    public void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }
    public void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);

        foreach (var lane in lanes) lane.SetTimeStamps(array);

        Invoke(nameof(StartSong), songDelayInSeconds);
    }
    public void StartSong()
    {
        audioSource.Play();
        //_isPlayed = true;
        _flemme = false;
    }
    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }

    public void ThisIsTheEnd()
    {
       if (audioSource.isPlaying == false && _Jeu.activeSelf == true)
        {
            //_isPlayed = false;
            SongManager._isCompleted = true;
            if(SongManager._isCompleted && !_flemme)
            {
                if (_SM.comboScore > 10)
                {
                    _winner = true;
                    _Service.ResultMiniGame(_winner);
                } else
                {
                    _winner = false;
                    _Service.ResultMiniGame(_winner);
                }
                _flemme = true;
                _lane.SetActive(false);
                StartCoroutine(NewGame());
            }
            //Debug.Log("La musique ne joue plus: " + _isPlayed);
        }
    }

    IEnumerator NewGame() {
        if(SongManager._isCompleted == true) {
            //Debug.Log("Restart");
            _SM.comboScore = 0;
            ReadFromFile();
            yield return new WaitForSeconds(5);
            _lane.SetActive(true);
        }
    }



    void Update()
    {
       ThisIsTheEnd();
    }
}