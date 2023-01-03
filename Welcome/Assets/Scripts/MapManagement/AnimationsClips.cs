using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;

public class AnimationsClips : MonoBehaviour
{
    public VideoClip[] WinLoseClip;
    public VideoPlayer VideoPlayerComponent;
    public Canvas CanvasComponent;

    public GameObject Support;
    public GameObject[] ResultSprites;

    public void Result(bool result,Service service)
    {
        //VideoPlayerComponent.clip = result ? WinLoseClip[0] : WinLoseClip[1];

        //CanvasComponent.sortingLayerName = "Minigames";

        //StartCoroutine(LoadingResult(service));

        int win = result ? 0 : 1;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(Support.transform.DOLocalMoveY(0f, .35f).SetEase(Ease.OutSine));
        sequence.Append(ResultSprites[win].transform.DOScale(1f, .35f).SetEase(Ease.OutSine));
        sequence.AppendInterval(.25f);

        sequence.OnComplete(()=>
        {
            service.EndResult();
            Start();
        });
    }

    IEnumerator LoadingResult(Service service)
    {

        yield return new WaitForFixedUpdate();


        VideoPlayerComponent.Play();
        
        while (VideoPlayerComponent.isPlaying)
        {
            yield return new WaitForFixedUpdate();
        }
        service.EndResult();
        Start();
    }

    void Start()
    {
        //CanvasComponent.sortingLayerName = "Background";
        Support.transform.localPosition = new Vector2(0f, 1000f);
        ResultSprites[0].transform.localScale = new Vector2(0f,0f);
        ResultSprites[1].transform.localScale = new Vector2(0f, 0f);
    }

}
