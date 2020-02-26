using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class failsound : MonoBehaviour
{
    semmanager sem;
    bgmmanager bgm;
    // Start is called before the first frame update
    void Start()
    {
        sem = FindObjectOfType<semmanager>();
        bgm = FindObjectOfType<bgmmanager>();
        if (this.gameObject.activeSelf == true)
        {
            bgm.source.volume = 0.2f;
            sem.play(8);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
