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

    public IEnumerator SpawnCubes()
    {
        for (int i = 0; i < numCubes; i++)
        {
            // spawn a cube at the same position as the spawner
            Formes _formes = Instantiate(cubePrefab, transform.position, Quaternion.identity);
            _formes._image.sprite = sprites[UnityEngine.Random.Range(0, sprites.Count)];
            _formes.transform.SetParent(zone.transform);
            // wait for "spawnInterval" seconds before spawning the next cube
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}