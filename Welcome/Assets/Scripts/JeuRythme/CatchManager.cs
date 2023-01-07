using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchManager : MonoBehaviour
{
    public static bool _isPlayed;
    public static bool _isCompleted;
    public static bool _winner;
    private bool _flemme;

    [SerializeField] private Service _Service;
    [SerializeField] public GameObject _Jeu;
    [SerializeField] private ScoreManager _SM;
    [SerializeField] private CubeCollider _CC;


    // Start is called before the first frame update
    void Start()
    {
        _isPlayed = false;
        _isCompleted = false;
        _winner = false;
        
    }



    public void ThisIsTheEnd()
    {
        if (_CC._countMiss + _CC._countHit == 20) {
            _isCompleted = true;
            if (_isCompleted && _SM.comboScore > 10)
            {
                _winner = true;
                _Service.ResultMiniGame(_winner);
            } else
            {
                _winner = false;
                _Service.ResultMiniGame(_winner);
            }
        }
    }

    public void NewGame() {
        
    }



    void Update()
    {
       ThisIsTheEnd();
    }
}
