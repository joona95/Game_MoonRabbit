using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int ChType;
    // Start is called before the first frame update
    void Start()
    {
        ChType = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ch0()
    {
        ChType = 0;
    }

    public void Ch1()
    {
        ChType = 1;
    }

    public void Ch2()
    {
        ChType = 2;
    }

    public void Ch3()
    {
        ChType = 3;
    }
}
