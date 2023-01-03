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

    [SerializeField] private Service _Service;
    [SerializeField] public GameObject _Jeu;

    public static MidiFile midiFile;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ReadFromFile();
        _isPlayed = false;
        _isCompleted = false;
    }

    private void ReadFromFile()
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
            _Service.ResultMiniGame(true);
            //Debug.Log("La musique ne joue plus: " + _isPlayed);
            SongManager._isCompleted = true;
            NewGame();
            SongManager._isCompleted = false;
        }
    }

    public void NewGame() {
        if(SongManager._isCompleted == true) {
            //Debug.Log("Restart");
            ReadFromFile();
        }
    }

    void Update()
    {
       ThisIsTheEnd();
    }
}