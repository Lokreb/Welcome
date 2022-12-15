
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

    float _angle = 155f / 424f;
    float test = 0;
    void FixedUpdate()
    {
        
        if(Point.transform.localPosition.x<=-205)return;
        float x = Point.transform.localPosition.x -1;
        float y = CurveTension.Evaluate(x);

        if(x>189 || x<-175)
        {
            Point.transform.Rotate(0f, 0f, _angle*2);
            test += 2;
        }else if((x < 113 && x > 52) || (x > -92 && x < -31))
        {
            Point.transform.Rotate(0f, 0f, _angle*.5f);
            test += .5f;
        }
        else
        {
            Point.transform.Rotate(0f, 0f, _angle);
            test++;
        }
        print(test);
        
        Point.transform.localPosition = new Vector2(x,y);
        
    }


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
    }
    void MoveNeedle(int value)
    {
        if (_needleAnglevalue <= -180) return;

        //calcul
        float r_value = value * 1.8f;

        if(_needleAnglevalue + r_value < -180)
        {
            r_value = 180 + _needleAnglevalue;
            r_value *= -1;
        }
        _needleAnglevalue += r_value;

        _Needle.transform.Rotate(0f,0f,r_value);
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
