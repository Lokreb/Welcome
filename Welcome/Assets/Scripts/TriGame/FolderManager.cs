using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FolderManager : MonoBehaviour
{
    public GameObject FolderPrefab;
    public List<FolderScript> FolderList = new List<FolderScript>();
    [HideInInspector] public List<GameObject> FolderListGO = new List<GameObject>();

    private int _ID;
    [SerializeField] private GameObject _spawnPosition;

    [SerializeField] private GameDataScript _gameData;


    // Start is called before the first frame update
    void Start()
    {
        _ID = 0;
        FolderList.Clear();
        _gameData.idCible.Clear();
        _gameData.count = 0;
        _gameData.scoreSortGame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        while(_ID < 40)
        {
            Spawn();
        }
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
        Debug.Log("ID = " + f.folder_ID + "  CIBLE ====== " + _gameData.idCible[_ID]);
        for(int i = 0; i < 5; i++)
        {
            if (f.trueService[i])
                Debug.Log("ID : " + f.folder_ID + " TRUE SERVICE ==== " + i);
        }
        _ID++;
    }


    public Sprite CreateSprite(int trueServiceValue)
    {
        string[] imagesName = { "Passoire", "Poêle", "Passoire", "Poêle" };
        string pathImage = Application.dataPath + "/Resources/Sprites/" + imagesName[trueServiceValue-1] + ".png";
        byte[] pngBytes = System.IO.File.ReadAllBytes(pathImage);
        Texture2D tex = new Texture2D(200, 200);
        tex.LoadImage(pngBytes);
        Sprite result = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(1f, 1f));
        return result;
    }
}
