using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhialSlots : MonoBehaviour, IDropHandler
{
    public static bool pointerIsOnSlot = false;
    public static string nameSelectedSlot;

    public void OnDrop(PointerEventData eventData)
    {
        nameSelectedSlot = name;
    }

    public void PointerOnSlot()
    {
        pointerIsOnSlot = true;
    }

    public void PointerOutSlot()
    {
        pointerIsOnSlot = false;
    }
}
