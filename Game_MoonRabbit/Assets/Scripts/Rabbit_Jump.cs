using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rabbit_Jump : MonoBehaviour
{
    public GameObject gameobject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void setting()
    {
        Debug.Log("1:"+Map_Lock.stage);
        Map_Lock.stage++;
        Debug.Log("2:"+Map_Lock.stage);
        gameobject.GetComponent<Map_Lock>().rabbits[Map_Lock.stage].SetActive(true);
        gameobject.GetComponent<Map_Lock>().buttons[Map_Lock.stage+1].GetComponent<Button>().interactable = true;
        gameobject.GetComponent<Map_Lock>().locks[Map_Lock.stage+1].SetActive(false);

        if (Map_Lock.give1 == true)
        {
            gameobject.GetComponent<Map_Lock>().back_ground.SetActive(true);
            gameobject.GetComponent<Map_Lock>().gives[0].SetActive(true);
            gameobject.GetComponent<Map_Lock>().confirm.SetActive(true);
            Map_Lock.give1 = false;
        }

        if (Map_Lock.give2 == true)
        {
            gameobject.GetComponent<Map_Lock>().back_ground.SetActive(true);
            gameobject.GetComponent<Map_Lock>().gives[1].SetActive(true);
            gameobject.GetComponent<Map_Lock>().confirm.SetActive(true);
            Map_Lock.give2 = false;
        }

        if (Map_Lock.give3 == true)
        {
            gameobject.GetComponent<Map_Lock>().back_ground.SetActive(true);
            gameobject.GetComponent<Map_Lock>().gives[2].SetActive(true);
            gameobject.GetComponent<Map_Lock>().confirm.SetActive(true);
            Map_Lock.give3 = false;
        }

        gameobject.GetComponent<Map_Lock>().rabbits[Map_Lock.stage-1].SetActive(false);
    }
}
