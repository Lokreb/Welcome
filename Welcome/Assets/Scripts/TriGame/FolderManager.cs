using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FolderManager : MonoBehaviour
{
    //public GameObject FolderPrefab;
    public Items FolderPrefab;

    private int _ID;
    [SerializeField] private GameObject _spawnPosition;

    [SerializeField] private GameDataScript _gameData;

    [SerializeField] private Service _Service;

    [SerializeField] private Sprite[] _ItemsSprite;

    public int NBFolder = 3;
    [Header("Sound Effect")]
    public AudioSource AudioComponent;
    public AudioClip Clip;

    // Start is called before the first frame update
    bool _start;
    void Start()
    {
        _ID = 0;
        _gameData.idCible.Clear();
        _gameData.count = 0;
        _gameData.scoreSortGame = 0;
        _start = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_start) return;
        EndGame();
        AudioComponent.volume = _gameData.volume;
    }

    bool _hasBeenClicked = false;
    public void StartMinigame(GameObject go)
    {
        if(! go.activeSelf || _hasBeenClicked) return;
        _delaiSpawn = 0f;
        NewGame();
        AudioComponent.clip = Clip;
        AudioComponent.Play();
        while (_ID < NBFolder)
        {
            Spawn();
        }
        _start = true;
        _hasBeenClicked = true;



    }
    private float _delaiSpawn;
    public void Spawn()
    {
        Items fGO = Instantiate(FolderPrefab, new Vector3(_spawnPosition.transform.position.x, _spawnPosition.transform.position.y - 300f, 0f), Quaternion.identity);

        fGO.transform.parent = _spawnPosition.transform;
        
        int valueService = Random.Range(1, 5);

        fGO.GetComponent<Image>().sprite = _ItemsSprite[valueService - 1];

        _gameData.idCible.Add(valueService);

        FolderScript f = new FolderScript(valueService);

        f.folder_ID = _ID;

        _ID++;

        if (_ID== NBFolder)
        {
            _gameData.idCible.Reverse();
        }

        fGO.AnimationComponent.Apparition(_delaiSpawn);
        _delaiSpawn += .02f;

    }

    public void EndGame()
    {
        if (_gameData.count == NBFolder)
        {
            _Service.ResultMiniGame(NBFolder*10 == _gameData.scoreSortGame);
            _gameData.count = 0;
            _gameData.scoreSortGame = 0;
            NewGame();
            _hasBeenClicked = false;
        }
    }

    public void NewGame()
    {
        _gameData.idCible.Clear();
        _ID = 0;
        _start = false;
    }
}
