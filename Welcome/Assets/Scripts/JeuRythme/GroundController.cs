using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public int _count = 0;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Cube")) {
            _count++;
            Debug.Log("Entrée: " + _count);
        }
    }

}
