using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Patient2 : MonoBehaviour
{
    public void Drag()
    {
        transform.position = Input.mousePosition;
    }
}
