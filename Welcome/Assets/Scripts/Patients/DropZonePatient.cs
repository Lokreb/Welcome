using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DropZonePatient : MonoBehaviour, IDropHandler
{
    public event Action<DropZonePatient> OnDropPatient;
    public string Name;

    [SerializeField] private Transform[] _waypoints;
    private int _waypointIndex;

    void Start()
    {
        _waypointIndex = _waypoints.Length - 1;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.position = transform.position;
            GoToStartingPoint(eventData.pointerDrag.transform);

            OnDropPatient?.Invoke(this);
        }
    }
    

    void GoToStartingPoint(Transform patient)
    {
        patient.DOMove(_waypoints[0].transform.position, .5f).OnComplete(() =>
        {
            if (_waypointIndex > 0) NextPoint(patient);
        });
    }

    void NextPoint(Transform patient)
    {
        patient.DOMove(_waypoints[_waypointIndex].transform.position, 2f);
        _waypointIndex--;
    }
}
