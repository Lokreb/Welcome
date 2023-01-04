using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class WayPointsValue : MonoBehaviour, IDropHandler
{
    public bool Dispo = true;
    public bool RoadMerge = false;
    public bool RoadSplit = false;
    public bool Service = false;
    public Services ServiceAffiliated;
    public int[] ID = { 0, 0 };

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null && Dispo)
        {
            Patient p = eventData.pointerDrag.GetComponent<Patient>();
            if(p.InMiniGame)return;
            p.transform.position = new Vector2(transform.position.x, transform.position.y+33f);

            DOTween.Kill(p.TweenID);
            GameManager.Instance.SetWayPointDispo(p.PathIn);
            p.PathIn[0] = ID[0];
            p.PathIn[1] = ID[1];
            Dispo=false;
        }
    }
}
