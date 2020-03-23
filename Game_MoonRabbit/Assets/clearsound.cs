using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearsound : MonoBehaviour
{
    semmanager sem;
    bgmmanager bgm;
    // Start is called before the first frame update
    void Start()
    {
        bgm = FindObjectOfType<bgmmanager>();
        sem = FindObjectOfType<semmanager>();
        if((this.gameObject.activeSelf ==true)&&(bgm.source.volume != 0f))
        {
            bgm.source.volume = 0.2f;
            sem.play(7);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
