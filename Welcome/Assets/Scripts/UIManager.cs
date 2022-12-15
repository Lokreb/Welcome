
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _Needle;
    private float _needleAnglevalue = 0;

    [SerializeField] private TextMeshProUGUI _PatientDone;

    [SerializeField] private TextMeshProUGUI _TimeLeft;
    [SerializeField] private TextMeshProUGUI _Score;

    public AnimationCurve CurveTension;
    public GameObject Point;

    void Start()
    {
        GameManager.Instance.OnHumorChange += MoveNeedle;
        GameManager.Instance.OnPatientEnd += PatientUpdate;
        GameManager.Instance.OnTimerChange += TimerUpdate;
        GameManager.Instance.OnScoreChange += ScoreUpdate;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnHumorChange -= MoveNeedle;
        GameManager.Instance.OnPatientEnd -= PatientUpdate;
        GameManager.Instance.OnTimerChange -= TimerUpdate;
        GameManager.Instance.OnScoreChange -= ScoreUpdate;
    }

    int _lastRotate = 100;
    void MoveNeedle(int value)
    {
        /*
        if (_needleAnglevalue <= -180) return;
        
        //calcul
        float r_value = value * 1.8f;

        if(_needleAnglevalue + r_value < -180)
        {
            r_value = 180 + _needleAnglevalue;
            r_value *= -1;
        }
        _needleAnglevalue += r_value;

        _Needle.transform.Rotate(0f,0f,r_value);*/

        if (Point.transform.localPosition.x <= -205) return;

        float x = value * 4.24f - 205f;
        float y = CurveTension.Evaluate(x);
        float angle = _lastRotate - value;
        
        Point.transform.localPosition = new Vector2(x, y);
        Point.transform.Rotate(0f, 0f, 1.55f * angle);

        _lastRotate = value;
    }

    void PatientUpdate()
    {
        _PatientDone.text = GameManager.Instance.PatientDone.ToString();
    }

    void TimerUpdate()
    {
        float timer = GameManager.Instance.Timer;

        int heures = Mathf.FloorToInt(timer / 3600F);
        timer %= 3600;
        int minutes = Mathf.FloorToInt(timer/60F);
        int seconds = Mathf.FloorToInt(timer % 60);
        string niceTime = string.Format("{0:00}:{1:00}:{2:00}", heures, minutes, seconds);

        _TimeLeft.text = niceTime;
    }

    void ScoreUpdate()
    {
        _Score.text = string.Format("{0:0000}", GameManager.Instance.Score.ToString());
    }
}
