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

    public List<Sprite> puzzle1 = new List<Sprite>();
    public List<Sprite> puzzle2 = new List<Sprite>();

    private int actualPuzzle;
    [SerializeField] Sprite slotSprite;
    [SerializeField] Transform[] slotTransforms;

    // Start is called before the first frame update
    void Start()
    {
        _ID = 0;
        _gameData.idCiblePuzzle.Clear();
        _gameData.count = 0;
        _gameData.scorePuzzleGame = 0;
        /*spriteValueList.Clear();
        actualPuzzle = Random.Range(0, 2);
        while (_ID < 9)
            Spawn();*/
    }

    // Update is called once per frame
    void Update()
    {
        EndGame();
    }

    public void Spawn()
    {
        GameObject fGO = Instantiate(puzzlePiecePrefab, new Vector3(_spawnPosition.transform.position.x, _spawnPosition.transform.position.y-300f, 0f), Quaternion.identity);
        fGO.transform.parent = _spawnPosition.transform;

        //TIRER AU SORT LA PIECE QUI SPAWN EN PREMIER
        int sprite_value = Random.Range(0,9);
        //CHECKER SI CETTE SPRITE N'EST PAS DEJA UTILISEE

        while (spriteValueList.Contains(sprite_value))
            sprite_value = Random.Range(0, 9);
        
        spriteValueList.Add(sprite_value);

        if(actualPuzzle == 0)
            fGO.GetComponent<Image>().sprite = puzzle1[sprite_value];
        else
            fGO.GetComponent<Image>().sprite = puzzle2[sprite_value];

        fGO.name = "Piece" + (sprite_value + 1);
        
        PuzzleScript p = new PuzzleScript(sprite_value);
        _gameData.idCiblePuzzle.Add(sprite_value);
        _ID++;

        fGO.transform.position = slotTransforms[sprite_value].position;
        fGO.GetComponent<AnimationPuzzle>().CutPieces();
    }

    public void EndGame() {
        if(_gameData.scorePuzzleGame >= 90)
        {
            _gameData.scorePuzzleGame = 0;
            _gameData.idCiblePuzzle.Clear();
            _gameData.count = 0;
            _Service.ResultMiniGame(true);
            //NewGame();
            _ID = 0;
        }
    }

    public void StartMinigame(GameObject go)
    {
        if(! go.activeSelf) return;

        NewGame();
    }

    public void NewGame()
    {
        spriteValueList.Clear();
        _ID = 0;
        ResetAllSprites();
        actualPuzzle = Random.Range(0,2);
        while (_ID < 9)
        {
            Spawn();
        }
        print("Decoupage sound");
    }

    public void ResetSprite(string name)
    {
        GameObject go = GameObject.Find("/Canvas/ServicesManager/MiniGame/PuzzleGame/Slots/" + name);
        go.GetComponent<Image>().sprite = slotSprite;
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
