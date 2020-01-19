using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class semmanager : MonoBehaviour
{
    static public semmanager instance;//씬이 넘어가도 이 스크립트는 지속됨
    public AudioClip[] clips;
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
