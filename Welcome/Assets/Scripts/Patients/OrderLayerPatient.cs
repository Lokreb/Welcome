using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderLayerPatient : MonoBehaviour
{
    public SpriteRenderer[] BodyParts;
    public void OrderLayer()
    {
        int nb = BodyParts.Length;

        for(int a=nb;a>0;a--)
        {
            BodyParts[a-1].sortingOrder = a;
        }
    }
}
