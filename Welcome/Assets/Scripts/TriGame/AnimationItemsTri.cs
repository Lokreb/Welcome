using UnityEngine;
using DG.Tweening;

public class AnimationItemsTri : MonoBehaviour
{
    public void Apparition(float delai)
    {
        float offsetX = Random.Range(-5f, 5f);
        float offsetY = Random.Range(-5f, 5f);

        Sequence sequence = DOTween.Sequence();

        sequence.SetDelay(delai);
        sequence.Append(transform.DOMove(new Vector2(transform.position.x + offsetX, transform.position.y + 150f + offsetY),.25f).SetEase(Ease.Linear));
        sequence.Join(transform.DORotate(new Vector3(0f,0f,180f), .25f).SetEase(Ease.Linear));
        sequence.Append(transform.DOMove(new Vector2(transform.position.x + offsetX, transform.position.y + 300f + offsetY), .25f).SetEase(Ease.Linear));
        sequence.Join(transform.DORotate(new Vector3(0f, 0f, 360f), .25f).SetEase(Ease.Linear));
    }
}
