using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInfoButton : MonoBehaviour
{
    public GameObject infobutton, endbutton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkinfo();
    }

    public void checkinfo()
    {
        if (this.gameObject.activeSelf)
        {
            infobutton.SetActive(false);
            endbutton.SetActive(true);
        }
    }
}
