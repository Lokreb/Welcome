using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject cubePrefab; // reference to the cube prefab
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    public float spawnInterval = 1f; // interval between each spawn
    public int numCubes = 20; // number of cubes to spawn

    void Start()
    {
        // spawn the cubes with a delay of "spawnInterval" seconds between each spawn
        StartCoroutine(SpawnCubes());
    }

    IEnumerator SpawnCubes()
    {
        for (int i = 0; i < numCubes; i++)
        {
            // spawn a cube at the same position as the spawner
            Formes _formes = Instantiate(cubePrefab, transform.position, Quaternion.identity);
            _formes._image.sprite = sprites[UnityEngine.Random.Range(0, sprites.Count)];
            // wait for "spawnInterval" seconds before spawning the next cube
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}