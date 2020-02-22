using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToRewardButton : MonoBehaviour
{
    public GameObject reward;
    public GameObject reward_1row, reward_1six, reward_1rain, reward_1r1s, reward_1r1r, reward_1s1r, reward_1r1s1r;
    //차례대로 1가로, 1육각형, 1무지개, 1가로1육각, 1가로1무지개, 1육각형1무지개, 1가로1육각1무지개

    public GameObject InfoRow, InfoSix, InfoRain, InfoPnM, InfoQ, InfoStone, InfoBomb;
    //차례대로 가로줄, 육각형, 무지개, +-2, 퀘스트, 돌, 폭탄 설명 ui
    
    public GameObject clear_rabbit, clear_fireworks, clear_ment;//성공
    public GameObject back_night, endbutton, infobutton;

    static public int reward_done = 0, infonum = 0, rownum = 0, sixnum = 0, rainnum = 0;
    int cnt, giveitem, giveitem2, giveitem3;
    
    void Start()
    {
        reward_done = 0;
        infonum = 0;

        


    }

    void Awake()
    {
        infobutton.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckReward()
    {
        if (this.gameObject.activeSelf)
        {
            
            back_night.SetActive(false);
            clear_fireworks.SetActive(false);
            clear_rabbit.SetActive(false);
            //endbutton.SetActive(true);
            clear_ment.SetActive(false);
            reward.SetActive(true);
            switch (Manager.current_stage)
            {
                case 3: 
                    reward_1six.SetActive(true);
                    giveitem = PlayerPrefs.GetInt("Item2_Cnt");
                    PlayerPrefs.SetInt("Item2_Cnt", giveitem + 1);

                    //Manager.rewardis[0]++;
                    cnt = PlayerPrefs.GetInt("Stage3_GetItem");
                    cnt++;
                    PlayerPrefs.SetInt("Stage3_GetItem", cnt);
                    //infonum++;
                    if (sixnum == 0)
                    {
                        infobutton.SetActive(true);
                    }
                    else endbutton.SetActive(true);
                    sixnum++;
                    break;
                case 6:
                    reward_1row.SetActive(true);
                    giveitem = PlayerPrefs.GetInt("Item1_Cnt");
                    PlayerPrefs.SetInt("Item1_Cnt", giveitem + 1);

                    cnt = PlayerPrefs.GetInt("Stage6_GetItem");
                    cnt++;
                    PlayerPrefs.SetInt("Stage6_GetItem", cnt);
                    //Manager.rewardis[1]++;
                    //infonum++;
                    if (rownum == 0)
                    {
                        infobutton.SetActive(true);
                    }
                    else endbutton.SetActive(true);
                    rownum++;
                    break;
                case 9:
                    reward_1rain.SetActive(true);
                    giveitem = PlayerPrefs.GetInt("Item3_Cnt");
                    PlayerPrefs.SetInt("Item3_Cnt", giveitem + 1);

                    cnt = PlayerPrefs.GetInt("Stage9_GetItem");
                    cnt++;
                    PlayerPrefs.SetInt("Stage9_GetItem", cnt);
                    //Manager.rewardis[2]++;
                    //infonum++;
                    if (rainnum == 0)
                    {
                        infobutton.SetActive(true);
                    }
                    else endbutton.SetActive(true);
                    rainnum++;
                    break;
                case 12:
                    reward_1six.SetActive(true);
                    giveitem = PlayerPrefs.GetInt("Item2_Cnt");
                    PlayerPrefs.SetInt("Item2_Cnt", giveitem + 1);

                    cnt = PlayerPrefs.GetInt("Stage12_GetItem");
                    cnt++;
                    PlayerPrefs.SetInt("Stage12_GetItem", cnt);
                    //Manager.rewardis[3]++;
                    endbutton.SetActive(true);
                    //reward_done++;
                    break;
                case 15:
                    reward_1row.SetActive(true);
                    giveitem = PlayerPrefs.GetInt("Item1_Cnt");
                    PlayerPrefs.SetInt("Item1_Cnt", giveitem + 1);

                    cnt = PlayerPrefs.GetInt("Stage15_GetItem");
                    cnt++;
                    PlayerPrefs.SetInt("Stage15_GetItem", cnt);
                    //Manager.rewardis[4]++;
                    endbutton.SetActive(true);
                    //reward_done++;
                    break;
                case 18:
                    reward_1rain.SetActive(true);
                    giveitem = PlayerPrefs.GetInt("Item3_Cnt");
                    PlayerPrefs.SetInt("Item3_Cnt", giveitem + 1);

                    cnt = PlayerPrefs.GetInt("Stage18_GetItem");
                    cnt++;
                    PlayerPrefs.SetInt("Stage18_GetItem", cnt);
                    //Manager.rewardis[5]++;
                    endbutton.SetActive(true);
                    //reward_done++;
                    break;
                case 21:
                    reward_1r1s.SetActive(true);
                    giveitem = PlayerPrefs.GetInt("Item1_Cnt");
                    PlayerPrefs.SetInt("Item1_Cnt", giveitem + 1);
                    giveitem2 = PlayerPrefs.GetInt("Item2_Cnt");
                    PlayerPrefs.SetInt("Item2_Cnt", giveitem2 + 1);

                    cnt = PlayerPrefs.GetInt("Stage21_GetItem");
                    cnt++;
                    PlayerPrefs.SetInt("Stage21_GetItem", cnt);
                    //Manager.rewardis[6]++;
                    endbutton.SetActive(true);
                    //reward_done++;
                    break;
                case 25:
                    reward_1r1s.SetActive(true);
                    giveitem = PlayerPrefs.GetInt("Item1_Cnt");
                    PlayerPrefs.SetInt("Item1_Cnt", giveitem + 1);
                    giveitem2 = PlayerPrefs.GetInt("Item2_Cnt");
                    PlayerPrefs.SetInt("Item2_Cnt", giveitem2 + 1);

                    cnt = PlayerPrefs.GetInt("Stage25_GetItem");
                    cnt++;
                    PlayerPrefs.SetInt("Stage25_GetItem", cnt);
                    //Manager.rewardis[7]++;
                    endbutton.SetActive(true);
                    //reward_done++;
                    break;
                case 29:
                    reward_1r1r.SetActive(true);
                    giveitem = PlayerPrefs.GetInt("Item1_Cnt");
                    PlayerPrefs.SetInt("Item1_Cnt", giveitem + 1);
                    giveitem2 = PlayerPrefs.GetInt("Item3_Cnt");
                    PlayerPrefs.SetInt("Item3_Cnt", giveitem2 + 1);

                    cnt = PlayerPrefs.GetInt("Stage29_GetItem");
                    cnt++;
                    PlayerPrefs.SetInt("Stage29_GetItem", cnt);
                    //Manager.rewardis[8]++;
                    endbutton.SetActive(true);
                    //reward_done++;
                    break;
                case 34:
                    reward_1s1r.SetActive(true);
                    giveitem = PlayerPrefs.GetInt("Item2_Cnt");
                    PlayerPrefs.SetInt("Item2_Cnt", giveitem + 1);
                    giveitem2 = PlayerPrefs.GetInt("Item3_Cnt");
                    PlayerPrefs.SetInt("Item3_Cnt", giveitem2 + 1);

                    cnt = PlayerPrefs.GetInt("Stage34_GetItem");
                    cnt++;
                    PlayerPrefs.SetInt("Stage34_GetItem", cnt);
                    //Manager.rewardis[9]++;
                    endbutton.SetActive(true);
                    //reward_done++;
                    break;
                case 39:
                    reward_1r1s1r.SetActive(true);
                    giveitem = PlayerPrefs.GetInt("Item1_Cnt");
                    PlayerPrefs.SetInt("Item1_Cnt", giveitem + 1);
                    giveitem2 = PlayerPrefs.GetInt("Item2_Cnt");
                    PlayerPrefs.SetInt("Item2_Cnt", giveitem2 + 1);
                    giveitem3 = PlayerPrefs.GetInt("Item3_Cnt");
                    PlayerPrefs.SetInt("Item3_Cnt", giveitem3 + 1);

                    cnt = PlayerPrefs.GetInt("Stage39_GetItem");
                    cnt++;
                    PlayerPrefs.SetInt("Stage39_GetItem", cnt);
                    //Manager.rewardis[10]++;
                    endbutton.SetActive(true);
                    //reward_done++;
                    break;
                default:
                    endbutton.SetActive(true);
                    break;

            }
        }
    }
}
