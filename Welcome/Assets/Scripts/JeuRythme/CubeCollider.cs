using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollider : MonoBehaviour
{
    private bool _isIn;

    private bool _hit;

    private bool _miss;
    public int _countHit;
    public int _countMiss;   
 

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
            _miss = true;
            Destroy(gameObject);
        }
    }

    private void PressToDestroy() {
        if(Input.GetKeyDown(KeyCode.Space) && _isIn == true) {
            Destroy(gameObject);
            Hit();
            _hit = true;
        }
    }

    void Update() {
        PressToDestroy();
        if(_hit == true) {
            _countHit++;
            Debug.Log("Touché: " + _countHit);
            _hit = false;
        }
        if(_miss == true) {
            _countMiss++;
            Debug.Log("Raté: " + _countMiss);
            _miss = false;
        }
    }

    private void Hit()
    {
        ScoreManager.Hit();
        _countHit++;
        Debug.Log("Touché" + _countHit);
    }
    private void Miss()
    {
        ScoreManager.Miss();
        _countMiss++;
        Debug.Log("Raté" + _countMiss);
    }
}
