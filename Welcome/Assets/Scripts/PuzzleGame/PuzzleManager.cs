using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public GameObject puzzlePiecePrefab;

    private int _ID;
    [SerializeField] private GameObject _spawnPosition;

    [SerializeField] private GameDataScript _gameData;

    [SerializeField] private Service _Service;

    public List<int> spriteValueList = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        _ID = 0;
        _gameData.idCiblePuzzle.Clear();
        _gameData.count = 0;
        _gameData.scorePuzzleGame = 0;
        spriteValueList.Clear();
        while (_ID < 9)
            Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        EndGame();
    }

    public void Spawn()
    {
        GameObject fGO = Instantiate(puzzlePiecePrefab, new Vector3(_spawnPosition.transform.position.x, _spawnPosition.transform.position.y, 0f), Quaternion.identity);
        fGO.transform.parent = _spawnPosition.transform;

        //TIRER AU SORT LA PIECE QUI SPAWN EN PREMIER
        int sprite_value = Random.Range(0,9);
        //CHECKER SI CETTE SPRITE N'EST PAS DEJA UTILISEE

        while (spriteValueList.Contains(sprite_value))
            sprite_value = Random.Range(0, 9);
        spriteValueList.Add(sprite_value);
        fGO.GetComponent<Image>().sprite = CreateSprite(sprite_value);
        for(int i = 0; i < 9; i++)
        {
            Debug.Log("ID : " + _ID + " & sprite_value actuelle : " + sprite_value);
        }
        PuzzleScript p = new PuzzleScript(sprite_value);
        _gameData.idCiblePuzzle.Add(sprite_value);
        _ID++;
    }

    public Sprite CreateSprite(int truePosition)
    {
        string[] imagesName = { "Passoire", "Poêle", "Passoire", "Poêle", "Passoire", "Poêle", "Passoire", "Poêle", "Passoire" };
        string image = "Sprites/" + imagesName[truePosition];
        Sprite result = Resources.Load<Sprite>(image);
        return result;
    }

    public void EndGame() {
        if(_gameData.scorePuzzleGame >= 90)
        {
            _gameData.scorePuzzleGame = 0;
            _gameData.idCiblePuzzle.Clear();
            _gameData.count = 0;
            _Service.ResultMiniGame(true);
            NewGame();
            _ID = 0;
        }
    }

    public void NewGame()
    {
        spriteValueList.Clear();
        _ID = 0;
        ResetAllSprites();
        while (_ID < 9)
        {
            Spawn();
        }
    }

    public void ResetSprite(string name)
    {
        GameObject go = GameObject.Find("/Canvas/ServicesManager/MiniGame/PuzzleGame/Slots/" + name);
        go.GetComponent<Image>().sprite = null;
    }

    public void ResetAllSprites()
    {
        ResetSprite("Slot_A");
        ResetSprite("Slot_B");
        ResetSprite("Slot_C");
        ResetSprite("Slot_D");
        ResetSprite("Slot_E");
        ResetSprite("Slot_F");
        ResetSprite("Slot_G");
        ResetSprite("Slot_H");
        ResetSprite("Slot_I");
    }
}
