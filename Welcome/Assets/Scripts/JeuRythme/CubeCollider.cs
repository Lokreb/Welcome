using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollider : MonoBehaviour
{
    private bool _isIn;
 
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ground")) {
            //Debug.Log("Entre");
            _isIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Ground")) {
            //Debug.Log("Sort");
            _isIn = false;
            Miss();
            Destroy(gameObject);
        }
    }

    private void PressToDestroy() {
        if(Input.GetKeyDown(KeyCode.Space) && _isIn == true) {
            Destroy(gameObject);
            Hit();
        }
    }

    void Update() {
        PressToDestroy();
    }

    private void Hit()
    {
        ScoreManager.Hit();
    }
    private void Miss()
    {
        ScoreManager.Miss();
    }
}
