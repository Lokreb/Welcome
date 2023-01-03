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

        _winner = false;
        _isDraggable = false;
        _isCompleted = false;

        while (_ID < 3)
        {
            Spawn();
        }

        _trueValue = Random.Range(0,3);
        PhialScript p = new PhialScript(_trueValue);
        _gameData.idCiblePhial = p.value_response;
        CouleursMelangeFiole[1].color = CouleursPossible[_gameData.idCiblePhial];
        //Debug.Log("La cible de notre fiole est : " + _gameData.idCiblePhial);
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

        //fGO.GetComponent<Image>().sprite = CreateSprite(sprite_value);
        fGO.GetComponent<Image>().sprite = _FiolesSprite[sprite_value];
        _ID++;
    }

    public Sprite CreateSprite(int truePosition)
    {
        string[] imagesName = {"Passoire", "Po�le", "Passoire"};
        string image = "Sprites/" + imagesName[truePosition];
        Sprite result = Resources.Load<Sprite>(image);
        return result;
    }

    public void ThisIsTheBeginning()
    {
        _gameData.idCiblePhial = 0;
        _ID = 0;
        spriteValueList.Clear();
        itemPosition.Add(_spawnPosition.transform.position.x);
        itemPosition.Add(_spawnPosition.transform.position.x + 300f);
        itemPosition.Add(_spawnPosition.transform.position.x + 600f);
    }

    public void ThisIsTheEnd()
    {
        if (PhialManager._isCompleted)
        {
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
        //ResetAllSprites();
        while (_ID < 3)
        {
            Spawn();
        }
    }

    /*
    public void ResetSprite(string name)
    {
        GameObject go = GameObject.Find("/Canvas/ServicesManager/MiniGame/PhialGame/Slots/" + name);
        go.GetComponent<Image>().sprite = null;
    }

    public void ResetAllSprites()
    {
        ResetSprite("Phial");
        ResetSprite("FinalSlot");
    }*/
}