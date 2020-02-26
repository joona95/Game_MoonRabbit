using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paper : MonoBehaviour
{
    semmanager sem;
    // Start is called before the first frame update
    void Start()
    {
        sem = FindObjectOfType<semmanager>();
        if (this.gameObject.activeSelf == true) { }
            //sem.play(10);//종이 폭죽 소리
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
