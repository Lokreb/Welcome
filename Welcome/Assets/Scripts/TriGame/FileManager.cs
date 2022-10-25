using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public GameObject FilePrefab;
    public List<File> FileList = new List<File>();
    public List<GameObject> FileListGO = new List<GameObject>();

    private int _ID;
    [SerializeField] private GameObject _serviceList;
    [SerializeField] private GameObject _spawnPosition;


    // Start is called before the first frame update
    void Start()
    {
        _ID = 0;
        FileList.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Spawn()
    {
        GameObject fGO = Instantiate(FilePrefab, new Vector3(_spawnPosition.transform.position.x, _spawnPosition.transform.position.y, 0f), Quaternion.identity);
        fGO.transform.parent = transform;
        int valueService = Random.Range(1, 5);
        File f = new File(valueService);
        f.file_ID = _ID;
        _ID++;
    }
}
