using UnityEngine;
using System.Collections;

public class MOVE2 : MonoBehaviour {
    float timer;
    AudioSource Balls;
    public float timeSound = .5f;
    public AudioClip Wub;
    public string Path;
    public float time;

	// Use this for initialization
	void Start () {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(Path),"time",time,"loopType","pingpong", "easetype", iTween.EaseType.linear));
        Balls = GetComponent<AudioSource>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeSound)
        {
            Balls.clip = Wub;
            Balls.Play();
            timer = 0;
        }
    }
}
