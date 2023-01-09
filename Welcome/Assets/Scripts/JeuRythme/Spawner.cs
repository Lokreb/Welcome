using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Formes cubePrefab; // reference to the cube prefab
    [SerializeField] public List<Sprite> sprites = new List<Sprite>();
    public float spawnInterval = 1f; // interval between each spawn
    public int numCubes = 20; // number of cubes to spawn

    [SerializeField] private GameObject zone;
    [SerializeField] private TrueValueManager _trueValueManager;

    public int _scoreMax = 0;
    public int numberStillAlive = 1;
    public bool _gameStarted = false;

    private void Update()
    {
        if(_gameStarted)
            numberStillAlive = zone.transform.childCount;
    }

    public IEnumerator SpawnCubes()
    {
        yield return new WaitForSeconds(.5f);

        for (int i = 0; i < numCubes; i++)
        {
            // spawn a cube at the same position as the spawner
            Formes _formes = Instantiate(cubePrefab, transform.position, Quaternion.identity);
            int _randomValue = UnityEngine.Random.Range(0, sprites.Count);
            _formes._image.sprite = sprites[_randomValue];

            //Some shapes are rectangles
            if (_randomValue == 0 || _randomValue == 1 || _randomValue == 2 || _randomValue == 3)
                _formes._image.rectTransform.sizeDelta = new Vector2(75,100);

            //The _scoreMax is modified when creating a new shape if it is valid.
            for (int j = 0; j < _trueValueManager._trueSpritesList.Count; j++)
                if (_formes._image.sprite.name == _trueValueManager._trueSpritesList[j].name)
                    _scoreMax++;
            
            _formes.transform.SetParent(zone.transform);
            _gameStarted = true;
            // wait for "spawnInterval" seconds before spawning the next cube
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}