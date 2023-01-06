using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Formes : MonoBehaviour
{
    public Image _image;

    void Update() {
         GetComponent<Image>().enabled = true;
    }
}
