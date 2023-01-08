using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 5f; // falling speed of the cubes

    void Update()
    {
        // move the cube down at the specified speed
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}