using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmmanager : MonoBehaviour
{
    static public bgmmanager instance;//말 그대로 매니저 소리의 끄기 켜기의 함수가 여기에 있다
    public AudioClip[] clips;
    public AudioSource source;
    //private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else {
            DontDestroyOnLoad(this.gameObject);//씬이 넘어가도 지속
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        play(0);
    }
    void Update()
    {
        
    }
    public void play(int _playmusictrack)
    {
        source.clip = clips[_playmusictrack];
        source.Play();
    }
    public void stop()
    {
        source.Stop();
    }
    public void soundon()
    {
        source.volume = 1f;
    }
    public void soundoff()
    {
        source.volume = 0f;
    }
    /*public void FadeOutMusic()//불필요한 코드인데 혹시 모르니 일단 묵혀둠
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMusicCoroutine());
    }
    IEnumerator FadeOutMusicCoroutine()
    {
        for(float i = 1.0f; i >= 0f; i -= 0.01f)
        {
            source.volume = i;
            yield return waitTime;
        }
    }
    public void FadeInMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInMusicCoroutine());
    }
    IEnumerator FadeInMusicCoroutine()
    {
        for (float i = 0f; i <= 1.0f; i += 0.01f)
        {
            source.volume = i;
            yield return waitTime;
        }
    }*/
    // Update is called once per frame

}
