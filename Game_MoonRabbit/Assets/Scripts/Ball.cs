﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.transform.position == GameObject.Find("Shotspawn").transform.position)//발사할 공에 한정하여 발사가 되게금 하는 함수
        {
            GetComponent<Rigidbody2D>().velocity = transform.right * 4f;//발사
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
