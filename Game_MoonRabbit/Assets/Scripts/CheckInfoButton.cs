using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInfoButton : MonoBehaviour
{
    public GameObject opacity;
    public GameObject infobutton, endbutton, checkbutton;
    public GameObject infoPM, infoQ, infoStone, infoBomb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //checkinfo();
    }

    public void checkinfo()
    {
        opacity.SetActive(false);

        if (infoPM.activeSelf)
        {
            infoPM.SetActive(false);
        }

        if (infoQ.activeSelf)
        {
            infoQ.SetActive(false);
        }

        if (infoStone.activeSelf)
        {
            infoStone.SetActive(false);
        }

        if (infoBomb.activeSelf)
        {
            infobutton.SetActive(false);
        }

        if (this.gameObject.activeSelf)
        {
            //infobutton.SetActive(false);
            //endbutton.SetActive(true);
            checkbutton.SetActive(false);
        }

        GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
    }
}
