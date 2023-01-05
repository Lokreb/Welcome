using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhialManager : MonoBehaviour
{
    public GameObject PhialPrefab;

    private int _ID;
    [SerializeField] private GameObject _spawnPosition;
    [SerializeField] private GameDataScript _gameData;

    [SerializeField]private List<int> spriteValueList = new List<int>();
    [SerializeField]private List<float> itemPosition = new List<float>();

    public static bool _winner;
    public static bool _isDraggable;
    public static bool _isCompleted;

    [SerializeField] private Service _Service;
    [SerializeField] private GameObject _phialItems;

    [SerializeField] private Sprite[] _FiolesSprite;
    public static Color[] CouleursPossible = {new Color(0.1882353f, 0.454902f, 0.5607843f, 1f),new Color(0.9294118f, 0.1098039f, 0.1411765f,1f),new Color(0.9921569f, 0.7764707f, 0.1803922f,1f) };
    public Image[] CouleursMelangeFiole;//Joueur - Final

    private int _trueValue;

    public void Start()
    {
        ThisIsTheBeginning();
    }

    private void Update()
    {
        ThisIsTheEnd();
    }

    
    public void Spawn()
    {
        //TIRER AU SORT LA PIECE QUI SPAWN EN PREMIER Blue - Red - Yellow
        int sprite_value = Random.Range(0, 3);
        //CHECKER SI CETTE SPRITE N'EST PAS DEJA UTILISEE
        while (spriteValueList.Contains(sprite_value))
            sprite_value = Random.Range(0, 3);
        spriteValueList.Add(sprite_value);
        
        GameObject fGO = Instantiate(PhialPrefab, new Vector3(itemPosition[_ID], _spawnPosition.transform.position.y, 0f), Quaternion.identity);
        fGO.transform.parent = _spawnPosition.transform;
        fGO.name = "Item" + sprite_value;

        fGO.GetComponent<Image>().sprite = _FiolesSprite[sprite_value];
        _ID++;
    }

    public void ThisIsTheBeginning()
    {
        //Init
        _gameData.idCiblePhial = 0;
        _ID = 0;
        spriteValueList.Clear();
        itemPosition.Add(_spawnPosition.transform.position.x);
        itemPosition.Add(_spawnPosition.transform.position.x + 300f);
        itemPosition.Add(_spawnPosition.transform.position.x + 600f);

        _winner = false;
        _isDraggable = false;
        _isCompleted = false;

        //We spawn the 3 phials.
        while (_ID < 3)
        {
            Spawn();
        }

        //We define which Phial is the right one and apply colors
        _trueValue = Random.Range(0, 3);
        PhialScript p = new PhialScript(_trueValue);
        _gameData.idCiblePhial = p.value_response;
        CouleursMelangeFiole[1].color = CouleursPossible[_gameData.idCiblePhial];
    }

    public void ThisIsTheEnd()
    {
        if (PhialManager._isCompleted)
        {
            //If the minigame is over, we send the result and init a new game.
            _gameData.idCiblePhial = 0;
            _Service.ResultMiniGame(PhialManager._winner);
            foreach (Transform child in _phialItems.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            NewGame();
        }
    }

    public void NewGame()
    {
        spriteValueList.Clear();
        _ID = 0;
        PhialManager._winner = false;
        PhialManager._isCompleted = false;
        PhialManager._isDraggable = false;
        _trueValue = Random.Range(0,3);
        _gameData.idCiblePhial = _trueValue;
        CouleursMelangeFiole[1].color = CouleursPossible[_trueValue];
        while (_ID < 3)
        {
            Spawn();
        }
    }
}