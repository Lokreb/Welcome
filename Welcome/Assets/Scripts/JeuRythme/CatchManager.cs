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
    [SerializeField] private GroundController _GC;
    [SerializeField] private Spawner _Spawner;



    // Start is called before the first frame update
    void Start()
    {
        _isPlayed = false;
        _isCompleted = false;
        _winner = false;
        
    }



    public void ThisIsTheEnd()
    {
        if (_GC._count == 20) {
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
            StartCoroutine(NewGame());
        }
    }

    IEnumerator NewGame() {
        _GC._count = 0;
        _SM.comboScore = 0;
        yield return new WaitForSeconds(3);
        StartCoroutine(_Spawner.SpawnCubes());
    }



    void Update()
    {
       ThisIsTheEnd();
    }
}
