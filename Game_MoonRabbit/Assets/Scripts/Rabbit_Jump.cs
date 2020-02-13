using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        gameobject.GetComponent<Map_Lock>().rabbits[Map_Lock.stage].SetActive(false);
        gameobject.GetComponent<Map_Lock>().rabbits[++Map_Lock.stage].SetActive(true);

        if (Map_Lock.give1 == true)
        {
            gameobject.GetComponent<Map_Lock>().back_ground.SetActive(true);
            gameobject.GetComponent<Map_Lock>().gives[0].SetActive(true);
            Map_Lock.give1 = false;
        }

        if (Map_Lock.give2 == true)
        {
            gameobject.GetComponent<Map_Lock>().back_ground.SetActive(true);
            gameobject.GetComponent<Map_Lock>().gives[1].SetActive(true);
            Map_Lock.give2 = false;
        }

        if (Map_Lock.give3 == true)
        {
            gameobject.GetComponent<Map_Lock>().back_ground.SetActive(true);
            gameobject.GetComponent<Map_Lock>().gives[2].SetActive(true);
            Map_Lock.give3 = false;
        }
    }
}
