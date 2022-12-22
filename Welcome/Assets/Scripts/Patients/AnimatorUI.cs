using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorUI : MonoBehaviour
{
    public Animator AnimatorComponent;
    public Renderer[] BodyPartsRender;

    public void Transparent(bool isDrag)
    {
        int nb = BodyPartsRender.Length;
        for(int a=0;a<nb;a++)
        {
            BodyPartsRender[a].material.color = isDrag ? new Color(1f,1f,1f,.5f) : new Color(1f, 1f, 1f, 1f);
        }
    }
}
