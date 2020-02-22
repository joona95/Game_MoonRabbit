using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToInfoButton : MonoBehaviour
{
    public GameObject reward;
    public GameObject reward_1row, reward_1six, reward_1rain;
    public GameObject info_row, info_six, info_rain, InfoPnM, InfoQ, InfoStone, InfoBomb;
    public GameObject endbutton, rewardbutton;
    public GameObject check;

    static public int info_done = 0;
    // Start is called before the first frame update
    void Start()
    {
        info_done = 0;
    }

    void Awake()
    {
        info_row.SetActive(false);
        info_six.SetActive(false);
        info_rain.SetActive(false);
        check.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckInfo()
    {
        if (this.gameObject.activeSelf)
        {

            reward.SetActive(false);
            rewardbutton.SetActive(false);
            switch (Manager.current_stage)
            {
                case 3:
                    reward_1six.SetActive(false);
                    info_six.SetActive(true);
                    info_done++;
                    //endbutton.SetActive(true);
                    check.SetActive(true);
                    break;
                case 6:
                    reward_1row.SetActive(false);
                    info_row.SetActive(true);
                    info_done++;
                    //endbutton.SetActive(true);
                    check.SetActive(true);
                    break;
                case 9:
                    reward_1rain.SetActive(false);
                    info_rain.SetActive(true);
                    info_done++;
                    //endbutton.SetActive(true);
                    check.SetActive(true);
                    break;
                default:
                    endbutton.SetActive(true);
                    break;

            }

        }
    }
}
