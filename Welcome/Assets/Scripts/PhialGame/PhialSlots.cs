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
        if(name == eventData.pointerDrag.name)return;

        if(eventData.pointerDrag.name == "Phial")
        {
            nameSelectedSlot = "FinalSlot";
        }

        if(eventData.pointerDrag.name != "Phial")
        {
            if(PhialManager._Step1Finish)return;
            
            if(name != "FinalSlot")
            {
                nameSelectedSlot = "Phial";
            }else
            {
                nameSelectedSlot = null;
            }
            
        }


        

        
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
