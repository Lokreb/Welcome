using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;

public class AnimationsClips : MonoBehaviour
{
    public VideoClip[] WinLooseClip;
    public VideoPlayer VideoPlayerComponent;
    public Canvas CanvasComponent;

    public void Result(bool result,Service service)
    {
        if(!result) VideoPlayerComponent.clip = WinLooseClip[1];

        CanvasComponent.sortingLayerName = "Minigames";
        VideoPlayerComponent.Prepare();

        StartCoroutine(LoadingResult(service));
    }

    IEnumerator LoadingResult(Service service)
    {
        while(!VideoPlayerComponent.isPrepared)
        {
            yield return new WaitForFixedUpdate();
        }

        VideoPlayerComponent.Play();
        service.EndResult();

        while (VideoPlayerComponent.isPlaying)
        {
            yield return new WaitForFixedUpdate();
        }

        Start();
        GameManager.Instance.InMinigame(false);
        GameStateManager.Instance.SetState(GameState.Gameplay);
    }

    void Start()
    {
        CanvasComponent.sortingLayerName = "Background";
        VideoPlayerComponent.clip = WinLooseClip[0];
    }
}
