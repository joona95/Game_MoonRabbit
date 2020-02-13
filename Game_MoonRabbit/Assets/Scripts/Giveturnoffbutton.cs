using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giveturnoffbutton : MonoBehaviour
{
    public GameObject back_ground;
    public GameObject[] gives = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnoff()
    {
        this.gameObject.SetActive(false);
        for(int i = 0; i < 3; i++)
        {
            if (gives[i].activeSelf == true)
            {
                gives[i].SetActive(false);
            }
        }
        back_ground.SetActive(false);
    }
}
