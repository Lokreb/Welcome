using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    public static Soundtrack instance;

    public AudioSource bgm;

    public GameDataScript gameData;
    // Start is called before the first frame update

    private void Update()
    {
        bgm.volume = gameData.music / 100f;
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
