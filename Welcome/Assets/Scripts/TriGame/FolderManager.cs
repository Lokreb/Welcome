using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FolderManager : MonoBehaviour
{
    public GameObject FolderPrefab;

    private int _ID;
    [SerializeField] private GameObject _spawnPosition;

    [SerializeField] private GameDataScript _gameData;

    [SerializeField] private Service _Service;


    // Start is called before the first frame update
    void Start()
    {
        _ID = 0;
        _gameData.idCible.Clear();
        _gameData.count = 0;
        _gameData.scoreSortGame = 0;

    }

    // Update is called once per frame
    void Update()
    {
        while(_ID < 10)
            {
                Spawn();
            }
        EndGame();
    }


    public void Spawn()
    {
        GameObject fGO = Instantiate(FolderPrefab, new Vector3(_spawnPosition.transform.position.x, _spawnPosition.transform.position.y, 0f), Quaternion.identity);
        
        fGO.transform.parent = _spawnPosition.transform;
        
        int valueService = Random.Range(1, 5);

        fGO.GetComponent<Image>().sprite = CreateSprite(valueService);

        _gameData.idCible.Add(valueService);

        FolderScript f = new FolderScript(valueService);

        f.folder_ID = _ID;

        _ID++;
    }


    public Sprite CreateSprite(int trueServiceValue)
    {
        string[] imagesName = { "Passoire", "Poêle", "Passoire", "Poêle" };
        string image = "Sprites/" + imagesName[trueServiceValue - 1];
        Sprite result = Resources.Load<Sprite>(image);
        return result;
    }

    public void EndGame()
    {
        if(_gameData.count == _gameData.idCible.Capacity)
        {
            _gameData.count = 0;
            _gameData.scoreSortGame = 0;
            _Service.ResultMiniGame(true);
            NewGame();
        }
    }

    public void NewGame()
    {
        _gameData.idCible.Clear();
        _ID = 0;
    }
}
