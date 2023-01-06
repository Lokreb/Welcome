using UnityEngine;
using DG.Tweening;

public class AnimationItemsTri : MonoBehaviour
{
    float _vitesse = .18f;
    public void Apparition(float delai)
    {
        transform.gameObject.SetActive(false);
        float offsetX = Random.Range(-5f, 5f);
        float offsetY = Random.Range(-5f, 5f);

        transform.localPosition = new Vector2(0f,-300f);
        Sequence sequenceDelai = DOTween.Sequence();
        

        sequenceDelai.SetDelay(delai);
        sequenceDelai.AppendInterval(AnimationPopUpMinigames.DELAIGLOBALEANIM);
        sequenceDelai.OnComplete(()=>
        {
            transform.gameObject.SetActive(true);
            
            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOLocalMove(new Vector2(offsetX,offsetY-150f),_vitesse).SetEase(Ease.Linear));
            sequence.Join(transform.DORotate(new Vector3(0f,0f,180f), _vitesse).SetEase(Ease.Linear));
            sequence.Append(transform.DOLocalMove(new Vector2(offsetX,offsetY), _vitesse).SetEase(Ease.Linear));
            sequence.Join(transform.DORotate(new Vector3(0f, 0f, 360f),_vitesse).SetEase(Ease.Linear));
        });

        
        

        
    }
}
