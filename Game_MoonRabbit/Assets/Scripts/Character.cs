using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject[] select=new GameObject[4];
    public static int ChType;
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
        if (!select[0].activeSelf)
        {
            select[0].SetActive(true);
        }
        if (select[1].activeSelf)
        {
            select[1].SetActive(false);
        }
        if (select[2].activeSelf)
        {
            select[2].SetActive(false);
        }
        if (select[3].activeSelf)
        {
            select[3].SetActive(false);
        }
    }

    public void Ch1()
    {
        ChType = 1;
        if (!select[1].activeSelf)
        {
            select[1].SetActive(true);
        }
        if (select[0].activeSelf)
        {
            select[0].SetActive(false);
        }
        if (select[2].activeSelf)
        {
            select[2].SetActive(false);
        }
        if (select[3].activeSelf)
        {
            select[3].SetActive(false);
        }
    }

    public void Ch2()
    {
        ChType = 2;
        if (!select[2].activeSelf)
        {
            select[2].SetActive(true);
        }
        if (select[0].activeSelf)
        {
            select[0].SetActive(false);
        }
        if (select[1].activeSelf)
        {
            select[1].SetActive(false);
        }
        if (select[3].activeSelf)
        {
            select[3].SetActive(false);
        }
    }

    public void Ch3()
    {
        ChType = 3;
        if (!select[3].activeSelf)
        {
            select[3].SetActive(true);
        }
        if (select[0].activeSelf)
        {
            select[0].SetActive(false);
        }
        if (select[1].activeSelf)
        {
            select[1].SetActive(false);
        }
        if (select[2].activeSelf)
        {
            select[2].SetActive(false);
        }
        
    }
}
