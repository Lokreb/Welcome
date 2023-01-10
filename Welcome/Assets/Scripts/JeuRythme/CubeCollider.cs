using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeCollider : MonoBehaviour
{
    private bool _isIn;
    [SerializeField] private TrueValueManager _trueValueManager;

    public void Start()
    {
        _trueValueManager = FindObjectOfType<TrueValueManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ground")) {
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
        if(Input.GetKeyDown(KeyCode.Space) && _isIn == true && ValidTarget()) {
            Destroy(gameObject);
            Hit();
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            foreach(Touch touch in Input.touches)
                if(touch.phase == TouchPhase.Began)
                    if(_isIn && ValidTarget())
                    {
                        Destroy(gameObject);
                        Hit();
                    }
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

    //A function that checks whether the target being pressed is valid or not.
    private bool ValidTarget()
    {
        for(int i = 0; i < _trueValueManager._trueSpritesList.Count; i++)
            if (GetComponentInParent<Image>().sprite.name == _trueValueManager._trueSpritesList[i].name)
                return true;

        return false;
    }
}
