using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class semmanager : MonoBehaviour
{
    static public semmanager instance;//씬이 넘어가도 이 스크립트는 지속됨
    public AudioClip[] clips;
    /*
     * 0:공쏠때(shooter)
     * 1:3개이상터질때, 2: 공쌓일때, 3:무지개구슬, 4:폭탄, 5:캐릭터보상, 6:아이템보상, 7:승리, 8:실패, 9:마술랜덤아이템, 10:ui버튼음 11:부활, 12: 가로줄/육각형구슬 (ball)
     */
    public AudioSource source;
    //private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
        {
            DontDestroyOnLoad(this.gameObject);//씬이 넘어가도 지속
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        if ((this.gameObject.GetComponent<AudioSource>().clip == clips[10])&&(source.volume!=0f))
        {
            source.volume = 0.5f;
        }
        else if((this.gameObject.GetComponent<AudioSource>().clip != (clips[10])) && (source.volume != 0f)&& (this.gameObject.GetComponent<AudioSource>().clip != clips[4]))
            source.volume = 1.0f;
        else if((this.gameObject.GetComponent<AudioSource>().clip == clips[4])&&(source.volume != 0f))
        {
            source.volume = 0.7f;
        }

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
}
