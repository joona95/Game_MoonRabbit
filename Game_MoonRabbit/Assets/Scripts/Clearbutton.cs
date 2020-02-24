using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clearbutton : MonoBehaviour
{
    public GameObject allclear, allclear_star;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clear()
    {
        for (int i = 0; i < Manager.total_row; i++)
        {
            for (int j = 0; j < Manager.Map[i].Length; j++)
            {
                if (Manager.Map[i][j])
                {
                    Manager.Map[i][j].GetComponent<Ball>().end = true;
                }
            }
        }
        allclear.SetActive(true);
        allclear_star.SetActive(true);
    }
}
