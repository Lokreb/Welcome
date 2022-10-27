using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZonePatient : MonoBehaviour, IDropHandler
{
    public event Action<DropZonePatient> OnDropPatient;
    public string Name;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.position = transform.position;
            OnDropPatient?.Invoke(this);
        }
    }
}
