using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderManager : MonoBehaviour
{
    public GameObject FolderPrefab;
    public List<FolderScript> FolderList = new List<FolderScript>();
    [HideInInspector] public List<GameObject> FolderListGO = new List<GameObject>();

    private int _ID;
    [SerializeField] private GameObject[] _serviceList;
    [SerializeField] private GameObject _spawnPosition;


    // Start is called before the first frame update
    void Start()
    {
        _ID = 0;
        FolderList.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        while(_ID < 100)
            Spawn();
    }


    public void Spawn()
    {
        GameObject fGO = Instantiate(FolderPrefab, new Vector3(_spawnPosition.transform.position.x, _spawnPosition.transform.position.y, 0f), Quaternion.identity);
        fGO.transform.parent = _spawnPosition.transform;
        int valueService = Random.Range(1, 5);
        FolderScript f = new FolderScript(valueService);
        f.folder_ID = _ID;
        Debug.Log("ID = " + f.folder_ID);
        for(int i = 0; i < 5; i++)
        {
            if (f.trueService[i])
                Debug.Log("TRUE SERVICE ==== " + i);
        }
        _ID++;
    }
}
