using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    public GameObject night, star, left_wall, right_wall;

    static public List<GameObject[]> Map; //관리할 맵
    static public int total_row, total_col; //맵 전체 행, 열
    static public List<Dictionary<string, object>> StageInfo; //스테이지마다 전체 row,col정보읽어오기
    static public int current_stage;//현재 스테이지 레벨
    static public int[,] stage; //각 스테이지마다 맵 구슬 생성 정보 저장
    static public bool clear = false, fail = false, die = false;
    public GameObject black, opacity, endbutton, confirm;//성공/실패/아이템랜덤 시 ui
    //아이템 랜덤시
    public GameObject hat, confetti;
    public GameObject[] hat_items = new GameObject[3];
    //성공, 실패시
    public GameObject back_night, back_ground; //배경
    public GameObject clear_rabbit, clear_fireworks;//성공
    public GameObject fail_rabbit, fail_one;//실패
    public GameObject clear_ment, fail_ment; //성공 실패 문구
    static public bool ing = false;
    public GameObject allclear;
    public GameObject allclear_star;
    public GameObject reward, rewardbutton, endinfobutton;
    public GameObject reward_1row, reward_1six, reward_1rain, reward_1r1s, reward_1r1r, reward_1s1r, reward_1r1s1r;
    //차례대로 1가로, 1육각형, 1무지개, 1가로1육각, 1가로1무지개, 1육각형1무지개, 1가로1육각1무지개

    public GameObject InfoRow, InfoSix, InfoRain, InfoPnM, InfoQ, InfoStone, InfoBomb, infobutton;
    //차례대로 가로줄, 육각형, 무지개, +-2, 퀘스트, 돌, 폭탄 설명 ui

    //ui 퀘스트 구슬 표시
    public Sprite[] questimages = new Sprite[5];
    public GameObject[] quests = new GameObject[5];
    public GameObject[] texts = new GameObject[5];
    public GameObject[] textboxes = new GameObject[5];

    public GameObject[] BallType=new GameObject[24];//구슬 색깔별로 종류 저장
    //-1:10,9열구분 -2:없는거 
    //0,1,2,3,4: 빨,노,초,파,보   5,6,7,8,9: 퀘스트빨,노,초,파,보 
    //10,11,12,13,14:갯수증가+2 빨,노,초,파,보 15,16,17,18,19:갯수감소-2 빨,노,초,파,보 
    //20:돌 21:건드리면죽는폭탄 22:가로줄폭탄 23:6개폭탄 24:무지개

    public GameObject shotspawn;//구슬 발사의 출발점을 정하기 위한 것
    public GameObject[] ballPrefabs;//발사할 구슬의 배열

    public string[] recentballs; //최근 발사한 공색깔 저장 배열
    public int recent_idx = 0;

    public int size;//발사할 구슬의 배열의 사이즈
    float purpl, re, blu, yello, gre;//구슬 개수 비율

    static public int limit_cnt; //구슬 제한 갯수
    static public int redCnt=0, yelCnt=0, greCnt=0, bluCnt=0, purCnt=0, queCnt=0, red_queCnt=0, yel_queCnt=0, gre_queCnt=0, blu_queCnt=0, pur_queCnt=0; //구슬 개수 카운트 변수
    static public int red_quest = 0, yel_quest = 0, gre_quest = 0, blu_quest = 0, pur_quest = 0; //지금까지 깬 퀘스트 구슬 갯수
    static public int[] rewardis;
    int total = 0;//전체 구슬 개수

    public GameObject ceil;
    float top_y;
    bool start = true;
    int rewardnum = 0, gotreward; //reward 창으로 넘어갈 때 이용하는 변수

    public GameObject[] characters=new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Stage3_GetItem"))
        {
            PlayerPrefs.SetInt("Stage3_GetItem", 0);
        }
        if (!PlayerPrefs.HasKey("Stage6_GetItem"))
        {
            PlayerPrefs.SetInt("Stage6_GetItem", 0);
        }
        if (!PlayerPrefs.HasKey("Stage9_GetItem"))
        {
            PlayerPrefs.SetInt("Stage9_GetItem", 0);
        }
        if (!PlayerPrefs.HasKey("Stage12_GetItem"))
        {
            PlayerPrefs.SetInt("Stage12_GetItem", 0);
        }
        if (!PlayerPrefs.HasKey("Stage15_GetItem"))
        {
            PlayerPrefs.SetInt("Stage15_GetItem", 0);
        }
        if (!PlayerPrefs.HasKey("Stage18_GetItem"))
        {
            PlayerPrefs.SetInt("Stage18_GetItem", 0);
        }
        if (!PlayerPrefs.HasKey("Stage21_GetItem"))
        {
            PlayerPrefs.SetInt("Stage21_GetItem", 0);
        }
        if (!PlayerPrefs.HasKey("Stage25_GetItem"))
        {
            PlayerPrefs.SetInt("Stage25_GetItem", 0);
        }
        if (!PlayerPrefs.HasKey("Stage29_GetItem"))
        {
            PlayerPrefs.SetInt("Stage29_GetItem", 0);
        }
        if (!PlayerPrefs.HasKey("Stage34_GetItem"))
        {
            PlayerPrefs.SetInt("Stage34_GetItem", 0);
        }
        if (!PlayerPrefs.HasKey("Stage39_GetItem"))
        {
            PlayerPrefs.SetInt("Stage39_GetItem", 0);
        }






        rewardis = new int[11];
        rewardnum = 0;
        total = redCnt + bluCnt + yelCnt + purCnt + greCnt;//일단 첫째로 색깔별 구슬 생성확률을 정해둡니다. 
        purpl = ((float)purCnt / total) * 100f;
        re = purpl + (((float)redCnt / total) * 100f);
        blu = re + (((float)bluCnt / total) * 100f);
        yello = blu + (((float)yelCnt / total) * 100f);
        size = 2;
        ballPrefabs = new GameObject[size];//발사할 구슬의 배열을 처음부터 정해줍니다.

        //첫번째 발사할 구슬
        float a;
        a = Random.Range(0.0f, 100.0f);
        
        if ((a >= 0.0f) && (a < purpl))
        {
            ballPrefabs[0] = (GameObject)Instantiate(BallType[4], new Vector3(0f, -3.5f, 0f), Quaternion.identity);
        }// (0~ 20퍼) 보라색
        else if ((a >= purpl) && (a < re))
        {
            ballPrefabs[0] = (GameObject)Instantiate(BallType[0], new Vector3(0f, -3.5f, 0f), Quaternion.identity); 
        }//(20~40퍼) 레드
        else if ((a >= re) && (a < blu))
        {
            ballPrefabs[0] = (GameObject)Instantiate(BallType[3], new Vector3(0f, -3.5f, 0f), Quaternion.identity); 
        }//(40~60퍼) 블루
        else if ((a >= blu) && (a < yello))
        {
            ballPrefabs[0] = (GameObject)Instantiate(BallType[1], new Vector3(0f, -3.5f, 0f), Quaternion.identity);
        }//(60~80퍼) 노랑
        else if ((a >= yello) && (a <= 100.0))
        {
            ballPrefabs[0] = (GameObject)Instantiate(BallType[2], new Vector3(0f, -3.5f, 0f), Quaternion.identity);
        }//(80퍼 이상 초록)
   

        //두번째 발사할 구슬
        a = Random.Range(0.0f, 100.0f);
        
        if ((a >= 0.0f) && (a < purpl))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[4], new Vector3(1.5f, -3.7f, 0f), Quaternion.identity); 
        }// (0~ 20퍼) 보라색
        else if ((a >= purpl) && (a < re))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[0], new Vector3(1.5f, -3.7f, 0f), Quaternion.identity); 
        }//(20~40퍼) 레드
        else if ((a >= re) && (a < blu))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[3], new Vector3(1.5f, -3.7f, 0f), Quaternion.identity); 
        }//(40~60퍼) 블루
        else if ((a >= blu) && (a < yello))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[1], new Vector3(1.5f, -3.7f, 0f), Quaternion.identity); 
        }//(60~80퍼) 노랑
        else if ((a >= yello) && (a <= 100.0))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[2], new Vector3(1.5f, -3.7f, 0f), Quaternion.identity); 
        }//(80퍼 이상 초록)

        recentballs = new string[5];
        recentballs[recent_idx++] = ballPrefabs[0].tag;
        recentballs[recent_idx++] = ballPrefabs[1].tag;
        //ballPrefabs[0].GetComponent<Ball>().willshoot = true;
        //ballPrefabs[1].GetComponent<Ball>().willshoot = true;
    }

    void Awake()
    {
        //ui감춤
        clear = false;
        fail = false;
        die = false;
        opacity.SetActive(false);
        endbutton.SetActive(false);
        confirm.SetActive(false);
        hat.SetActive(false);
        for (int i = 0; i < 3; i++)
            hat_items[i].SetActive(false);
        confetti.SetActive(false);
        clear_fireworks.SetActive(false);
        clear_rabbit.SetActive(false);
        fail_one.SetActive(false);
        fail_rabbit.SetActive(false);
        clear_ment.SetActive(false);
        fail_ment.SetActive(false);
        reward.SetActive(false);
        rewardbutton.SetActive(false);
        reward_1row.SetActive(false);
        reward_1six.SetActive(false);
        reward_1rain.SetActive(false);
        reward_1r1s.SetActive(false);
        reward_1r1r.SetActive(false);
        reward_1s1r.SetActive(false);
        reward_1r1s1r.SetActive(false);
        InfoRow.SetActive(false);
        InfoSix.SetActive(false);
        InfoRain.SetActive(false);
        InfoPnM.SetActive(false);
        InfoQ.SetActive(false);
        InfoStone.SetActive(false);
        InfoBomb.SetActive(false);
        infobutton.SetActive(false);
        endinfobutton.SetActive(false);
        ing = false;
        Ball.anim_cnt = 0;
        GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
        Shooter.possible = true;
        Shooter.starlinepossible = true;


        for (int i = 0; i < 5; i++)
        {
            textboxes[i].SetActive(false);
        }


        //캐릭터 모양 표시
        if (Character.ChType == 0)
        {
            characters[0].SetActive(true);
        }
        else if (Character.ChType == 1)
        {
            characters[1].SetActive(true);
        }
        else if (Character.ChType == 2)
        {
            characters[2].SetActive(true);
        }
        else if (Character.ChType == 3)
        {
            characters[3].SetActive(true);
            opacity.SetActive(true);
            GameObject.Find("대포").GetComponent<Shooter>().enabled = false;
            hat.SetActive(true);
            confetti.SetActive(true);

            float p = Random.Range(0, 100);
            if (0 <= p && p < 33.33f)
            {
                hat_items[0].SetActive(true);

            }
            else if (33.33f <= p && p < 66.66f)
            {
                hat_items[1].SetActive(true);

            }
            else
            {
                hat_items[2].SetActive(true);
            }

            confirm.SetActive(true);

        }

        //맵생성        

        StageInfo = MapLoader.StageRead("StageInfo");//stage정보 csv 읽어오기
        total_row = int.Parse(StageInfo[current_stage-1]["Row"].ToString()); //stage에 따른 total row
        total_col = int.Parse(StageInfo[current_stage-1]["Col"].ToString()); //stage에 따른 total col
        limit_cnt = int.Parse(StageInfo[current_stage - 1]["Count"].ToString());
        stage = new int[total_row, total_col];
        Debug.Log(total_row + "," + total_col);
        MapLoader.MapRead(current_stage.ToString()); //구슬 생성 맵 정보 csv에서 읽어오기
        Map = new List<GameObject[]>();
        redCnt = 0; yelCnt = 0; greCnt = 0; bluCnt = 0; purCnt = 0; queCnt = 0;
        red_queCnt = 0; yel_queCnt = 0; gre_queCnt = 0; blu_queCnt = 0; pur_queCnt = 0; //구슬 개수 카운트 변수
        red_quest = 0; yel_quest = 0; gre_quest = 0; blu_quest = 0; pur_quest = 0;

        float x, y=3.1f; //구슬 생성 위치 지정 변수.  제일 위에서부터 쌓아올라가기
        int t; //행 구분을 위한 변수
        //Stack<GameObject[]> temp = new Stack<GameObject[]>();//List Map에 거꾸로 넣어주기
        for(int i = 0; i<total_row; i++)
        {
            if (stage[i, total_col - 1] == -1) //9개 행
            {
                x = 2.08f;
                t = 9;
            }
            else //10개 행
            {
                x = 2.34f;
                t = 10;
            }

        
            GameObject[] tmp = new GameObject[t];//관리할 맵 리스트에 넣을 한 줄의 구슬 전체 배열
            for(int j = t - 1; j >= 0; j--)
            {
                if (stage[i, j] == -2) //비어있을때
                {
                    tmp[j] = null;
                }
                else //비어있지않을때
                {
                    tmp[j] = Instantiate(BallType[stage[i, j]], new Vector3(x, y, 0f), Quaternion.Euler(0f, 0f, 0f)); //구슬 생성
                    tmp[j].GetComponent<Ball>().type = total_col - t; //구슬 종류 (0:10개, 1:9개)
                    tmp[j].GetComponent<Ball>().row = i; //해당 구슬이 배치된 행
                    tmp[j].GetComponent<Ball>().col = j; //해당 구슬이 배치된 열


                    //처음 배치되는 전체 구슬 숫자 카운트
                    switch (stage[i, j])
                    {
                        case 0:
                            redCnt++;
                            break;
                        case 1:
                            yelCnt++;
                            break;
                        case 2:
                            greCnt++;
                            break;
                        case 3:
                            bluCnt++;
                            break;
                        case 4:
                            purCnt++;
                            break;
                        case 5:
                            redCnt++;
                            red_queCnt++;
                            queCnt++;
                            tmp[j].GetComponent<Ball>().quest = true;
                            break;
                        case 6:
                            yelCnt++;
                            yel_queCnt++;
                            queCnt++;
                            tmp[j].GetComponent<Ball>().quest = true;
                            break;
                        case 7:
                            greCnt++;
                            gre_queCnt++;
                            queCnt++;
                            tmp[j].GetComponent<Ball>().quest = true;
                            break;
                        case 8:
                            bluCnt++;
                            blu_queCnt++;
                            queCnt++;
                            tmp[j].GetComponent<Ball>().quest = true;
                            break;
                        case 9:
                            purCnt++;
                            pur_queCnt++;
                            queCnt++;
                            tmp[j].GetComponent<Ball>().quest = true; //퀘스트 구슬인 경우 각 구슬마다 표시
                            break;
                        case 10:
                            redCnt++;
                            tmp[j].GetComponent<Ball>().count += 2;
                            break;
                        case 11:
                            yelCnt++;
                            tmp[j].GetComponent<Ball>().count += 2;
                            break;
                        case 12:
                            greCnt++;
                            tmp[j].GetComponent<Ball>().count += 2;
                            break;
                        case 13:
                            bluCnt++;
                            tmp[j].GetComponent<Ball>().count += 2;
                            break;
                        case 14:
                            purCnt++;
                            tmp[j].GetComponent<Ball>().count += 2;
                            break;
                        case 15:
                            redCnt++;
                            tmp[j].GetComponent<Ball>().count -= 2;
                            break;
                        case 16:
                            yelCnt++;
                            tmp[j].GetComponent<Ball>().count -= 2;
                            break;
                        case 17:
                            greCnt++;
                            tmp[j].GetComponent<Ball>().count -= 2;
                            break;
                        case 18:
                            bluCnt++;
                            tmp[j].GetComponent<Ball>().count -= 2;
                            break;
                        case 19:
                            purCnt++;
                            tmp[j].GetComponent<Ball>().count -= 2;
                            break;
                        case 21:
                            tmp[j].GetComponent<Ball>().diebomb = true;
                            break;
                        case 22:
                            tmp[j].GetComponent<Ball>().rowbomb = true;
                            break;
                        case 23:
                            tmp[j].GetComponent<Ball>().sixbomb = true;
                            break;

                    }
                }

                x -= 0.52f;
            }
            Map.Add(tmp); //리스트에 구슬 한 줄의 배열 넣기
            

            y -= 0.45f;
        }

        /*
        //Map에 GameObject[] 처음 행부터 모든 행 전부 넣어주기
        while (temp.Count != 0)
        {
            Map.Add(temp.Pop());
        }
        */

        //top_y = y;
        //ceil.transform.position = new Vector3(0f, top_y-0.15f, 0f);
        ceil.transform.position = new Vector3(0f, 3.4f, 0f);


        int cc = 0;
        if (red_queCnt != 0)
        {           
            textboxes[cc].SetActive(true);
            quests[cc].SetActive(true);
            texts[cc].SetActive(true);
            quests[cc].GetComponent<Image>().sprite = questimages[0];
            //texts[cc].GetComponent<QuestCnt>().Text.text = red_quest.ToString() + "/" + red_queCnt.ToString();
            texts[cc].GetComponent<QuestCnt>().color = "red";
            cc++;
        }

        if (yel_queCnt != 0)
        {            
            textboxes[cc].SetActive(true);
            quests[cc].SetActive(true);
            texts[cc].SetActive(true);
            quests[cc].GetComponent<Image>().sprite = questimages[1];
            //texts[cc].GetComponent<QuestCnt>().Text.text = yel_quest.ToString() + "/" + yel_queCnt.ToString();
            texts[cc].GetComponent<QuestCnt>().color = "yellow";
            cc++;
        }

        if (gre_queCnt != 0)
        {
            
            textboxes[cc].SetActive(true);
            quests[cc].SetActive(true);
            texts[cc].SetActive(true);
            quests[cc].GetComponent<Image>().sprite = questimages[2];
            //texts[cc].GetComponent<QuestCnt>().Text.text = gre_quest.ToString() + "/" + gre_queCnt.ToString();
            texts[cc].GetComponent<QuestCnt>().color = "green";
            cc++;
        }

        if (blu_queCnt != 0)
        {
            textboxes[cc].SetActive(true);
            quests[cc].SetActive(true);
            texts[cc].SetActive(true);
            quests[cc].GetComponent<Image>().sprite = questimages[3];
            //texts[cc].GetComponent<QuestCnt>().Text.text = blu_quest.ToString() + "/" + blu_queCnt.ToString();
            texts[cc].GetComponent<QuestCnt>().color = "blue";
            cc++;
        }

        if (pur_queCnt != 0)
        {
            textboxes[cc].SetActive(true);
            quests[cc].SetActive(true);
            texts[cc].SetActive(true);
            quests[cc].GetComponent<Image>().sprite = questimages[4];
            //texts[cc].GetComponent<QuestCnt>().Text.text = pur_quest.ToString() + "/" + pur_queCnt.ToString();
            texts[cc].GetComponent<QuestCnt>().color = "purple";
            cc++;
        }

    }


    // Update is called once per frame
    void Update()//구슬 색깔별로 개수 비율 맞춰놓은 겁니다.(발사 구슬 랜덤) 구슬 색깔 비율 변수를 여기 저기 넣어봤는데 update에 넣어둬야 제대로 되더라고요
    {
        /*
        int cc = 0;
        if (red_queCnt != 0)
        {
            texts[cc].GetComponent<QuestCnt>().Text.text = red_quest.ToString() + "/" + red_queCnt.ToString();
            cc++;
        }

        if (yel_queCnt != 0)
        {
            texts[cc].GetComponent<QuestCnt>().Text.text = yel_quest.ToString() + "/" + yel_queCnt.ToString();
            cc++;
        }

        if (gre_queCnt != 0)
        {
            texts[cc].GetComponent<QuestCnt>().Text.text = gre_quest.ToString() + "/" + gre_queCnt.ToString();
            cc++;
        }

        if (blu_queCnt != 0)
        {
            texts[cc].GetComponent<QuestCnt>().Text.text = blu_quest.ToString() + "/" + blu_queCnt.ToString();
            cc++;
        }


        if (pur_queCnt != 0)
        {
            texts[cc].GetComponent<QuestCnt>().Text.text = pur_quest.ToString() + "/" + pur_queCnt.ToString();
            cc++;
        }
        */


        if (clear == true)//성공
        {
            GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
            Shooter.possible = false;
            Shooter.starlinepossible = false;
            

            Ball[] balls = (Ball[])GameObject.FindObjectsOfType(typeof(Ball));
            foreach (Ball ball in balls)
            {
                ball.enabled = false;
                //Destroy(ball);
            }


            if (Mathf.Round(night.transform.position.y * 100) / 100 > -6f)
            {
                night.transform.position = new Vector3(night.transform.position.x, night.transform.position.y - 0.2f, night.transform.position.z);
                star.transform.position = new Vector3(star.transform.position.x, star.transform.position.y - 0.2f, star.transform.position.z);
                left_wall.transform.position = new Vector3(left_wall.transform.position.x, left_wall.transform.position.y - 0.2f, left_wall.transform.position.z);
                right_wall.transform.position = new Vector3(right_wall.transform.position.x, right_wall.transform.position.y - 0.2f, right_wall.transform.position.z);
            }
            else
            {
                if (current_stage == 40)
                {
                    allclear.SetActive(true);
                    allclear_star.SetActive(true);
                }
                else
                {
                    if (rewardnum == 0)
                    {
                        black.SetActive(true);
                        back_night.SetActive(true);
                        clear_fireworks.SetActive(true);
                        clear_rabbit.SetActive(true);
                        //endbutton.SetActive(true);
                        clear_ment.SetActive(true);
                        switch (current_stage)
                        {
                            case 3:
                                gotreward = PlayerPrefs.GetInt("Stage3_GetItem");
                                if (gotreward == 0)
                                {
                                    rewardbutton.SetActive(true);
                                    rewardnum++;
                                }
                                else
                                {
                                    endbutton.SetActive(true);
                                }
                                break;
                            case 6:
                                gotreward = PlayerPrefs.GetInt("Stage6_GetItem");
                                if (gotreward == 0)
                                {
                                    rewardbutton.SetActive(true);
                                    rewardnum++;
                                }
                                else
                                {
                                    endbutton.SetActive(true);
                                }
                                break;
                            case 9:
                                gotreward = PlayerPrefs.GetInt("Stage9_GetItem");
                                if (gotreward == 0)
                                {
                                    rewardbutton.SetActive(true);
                                    rewardnum++;
                                }
                                else
                                {
                                    endbutton.SetActive(true);
                                }
                                break;
                            case 12:
                                gotreward = PlayerPrefs.GetInt("Stage12_GetItem");
                                if (gotreward == 0)
                                {
                                    rewardbutton.SetActive(true);
                                    rewardnum++;
                                }
                                else
                                {
                                    endbutton.SetActive(true);
                                }
                                break;
                            case 15:
                                gotreward = PlayerPrefs.GetInt("Stage15_GetItem");
                                if (gotreward == 0)
                                {
                                    rewardbutton.SetActive(true);
                                    rewardnum++;
                                }
                                else
                                {
                                    endbutton.SetActive(true);
                                }
                                break;
                            case 18:
                                gotreward = PlayerPrefs.GetInt("Stage18_GetItem");
                                if (gotreward == 0)
                                {
                                    rewardbutton.SetActive(true);
                                    rewardnum++;
                                }
                                else
                                {
                                    endbutton.SetActive(true);
                                }
                                break;
                            case 21:
                                gotreward = PlayerPrefs.GetInt("Stage21_GetItem");
                                if (gotreward == 0)
                                {
                                    rewardbutton.SetActive(true);
                                    rewardnum++;
                                }
                                else
                                {
                                    endbutton.SetActive(true);
                                }
                                break;
                            case 25:
                                gotreward = PlayerPrefs.GetInt("Stage25_GetItem");
                                if (gotreward == 0)
                                {
                                    rewardbutton.SetActive(true);
                                    rewardnum++;
                                }
                                else
                                {
                                    endbutton.SetActive(true);
                                }
                                break;
                            case 29:
                                gotreward = PlayerPrefs.GetInt("Stage29_GetItem");
                                if (gotreward == 0)
                                {
                                    rewardbutton.SetActive(true);
                                    rewardnum++;
                                }
                                else
                                {
                                    endbutton.SetActive(true);
                                }
                                break;
                            case 34:
                                gotreward = PlayerPrefs.GetInt("Stage34_GetItem");
                                if (gotreward == 0)
                                {
                                    rewardbutton.SetActive(true);
                                    rewardnum++;
                                }
                                else
                                {
                                    endbutton.SetActive(true);
                                }
                                break;
                            case 39:
                                gotreward = PlayerPrefs.GetInt("Stage39_GetItem");
                                if (gotreward == 0)
                                {
                                    rewardbutton.SetActive(true);
                                    rewardnum++;
                                }
                                else
                                {
                                    endbutton.SetActive(true);
                                }
                                break;
                            default:
                                endbutton.SetActive(true);
                                break;
                        }
                    }
                    
                }
            }
            //GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
            clear = false;
        }
        else if (fail == true)
        {
            //GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
            if (Shooter.possible == true||die==true)
            {
                GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
                Shooter.possible = false;
                Shooter.starlinepossible = false;
                //GameObject.Find("대포").GetComponent<Shooter>().enabled = false;
                Ball[] balls = (Ball[])GameObject.FindObjectsOfType(typeof(Ball));
                foreach (Ball ball in balls)
                {
                    ball.enabled = false;
                    //Destroy(ball);
                }
                black.SetActive(true);
                back_night.SetActive(true);
                back_ground.SetActive(true);
                fail_rabbit.SetActive(true);
                fail_one.SetActive(true);
                endbutton.SetActive(true);
                fail_ment.SetActive(true);

                //GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
                fail = false;
            }

        }


        int c = 0;
        if (redCnt != 0)
            c++;
        if (yelCnt != 0)
            c++;
        if (bluCnt != 0)
            c++;
        if (greCnt != 0)
            c++;
        if (purCnt != 0)
            c++;

        float plus = 10f / (float)c;

        re = 0; purpl = 0; yello = 0; blu = 0;
        if (c > 1)
        {      
            for (int i = 0; i < 5; i++)
            {
                if (recentballs[i] == "red")
                {
                    if (redCnt != 0)
                    {
                        re -= 10f;

                        if (purCnt != 0)
                            purpl += plus;
                        if (yelCnt != 0)
                            yello += plus;
                        if (bluCnt != 0)
                            blu += plus;
                    }

                }
                else if (recentballs[i] == "yellow")
                {
                    if (yelCnt != 0)
                    {
                        yello -= 10f;
                        if (purCnt != 0)
                            purpl += plus;
                        if (redCnt != 0)
                            re += plus;
                        if (bluCnt != 0)
                            blu += plus;
                    }

                }
                else if (recentballs[i] == "green")
                {
                    if (redCnt != 0)
                        re += plus;
                    if (bluCnt != 0)
                        blu += plus;
                    if (yelCnt != 0)
                        yello += plus;
                    if (purCnt != 0)
                        purpl += plus;

                }
                else if (recentballs[i] == "blue")
                {
                    if (bluCnt != 0)
                    {
                        blu -= 10f;
                        if (redCnt != 0)
                            re += plus;
                        if (yelCnt != 0)
                            yello += plus;
                        if (purCnt != 0)
                            purpl += plus;
                    }

                }
                else if (recentballs[i] == "purple")
                {
                    if (purCnt != 0)
                    {
                        purpl -= 10f;
                        if (redCnt != 0)
                            re += plus;
                        if (yelCnt != 0)
                            yello += plus;
                        if (bluCnt != 0)
                            blu += plus;
                    }

                }
            }
        }
        total = redCnt + bluCnt + yelCnt + purCnt + greCnt;

        purpl += ((float)purCnt / total) * 100f;

        re += purpl + (((float)redCnt / total) * 100f);

        blu += re + (((float)bluCnt / total) * 100f);

        yello += blu + (((float)yelCnt / total) * 100f);


        
        //Debug.Log("pur:" + purCnt + ",re:" + redCnt + ",blu:" + bluCnt + ",yel:" + yelCnt + ",gre:"+greCnt);
        //Debug.Log("pur:"+purpl + ",re:" + re + ",blu:" + blu + ",yel:" + yello + ",gre:100");

        //각 row에 구슬이 하나도 없다면 total_row 감소
        for (int i = total_row - 1; i >= 0; i--)
        {
            int cnt = 0;
            for (int j = Map[i].Length - 1; j >= 0; j--)
            {
                if (Map[i][j] == null)
                    cnt++;
            }
            if (cnt == Map[i].Length)
            {
                total_row--;
                Map.Remove(Map[i]);
            }

        }

        if (queCnt==0) //게임 성공
        {
            //Time.timeScale = 0f;
            Manager.clear = true;
            GameObject.Find("대포").GetComponent<Shooter>().enabled = false;
            if (PlayerPrefs.GetInt("User_stage") < current_stage)
            {
                PlayerPrefs.SetInt("User_stage", current_stage);
                Map_Lock.jump = true;
                if (current_stage == 10)
                {
                    Map_Lock.give1 = true;
                }
                if (current_stage == 20)
                {
                    Map_Lock.give2 = true;
                }
                if (current_stage == 30)
                {
                    Map_Lock.give3 = true;
                }
            }

        }
        else if (Manager.limit_cnt == 0)
        {
            //Time.timeScale = 0f;
            //Manager.fail = true;
        }
 

        if (start == true)
        {
            Shooter.possible = false;
            Shooter.starlinepossible = false;

            bool end2 = false;
            if (Mathf.Round(night.transform.position.y * 100) / 100 < 6f)
            {
                night.transform.position = new Vector3(night.transform.position.x, night.transform.position.y + 0.15f, night.transform.position.z);
                star.transform.position = new Vector3(star.transform.position.x, star.transform.position.y + 0.15f, star.transform.position.z);
                left_wall.transform.position = new Vector3(left_wall.transform.position.x, left_wall.transform.position.y + 0.15f, left_wall.transform.position.z);
                right_wall.transform.position = new Vector3(right_wall.transform.position.x, right_wall.transform.position.y + 0.15f, right_wall.transform.position.z);
            }
            else
            {
                end2 = true;
            }
            

            bool end = false;
            float min_y = 0.85f;
            int map_index = 0;
            map_index = Map[total_row - 1].Length - 1;

            for (int i = map_index; i >= 0; i--)
            {

                if (Map[total_row - 1][i] != null)
                {
                    min_y = Mathf.Round(Map[total_row - 1][i].transform.position.y * 100) / 100;
                    break;
                }
            }

            float max_y = 4f;
            for (int i = Map[0].Length - 1; i >= 0; i--)
            {
                if (Map[0][i] != null)
                {
                    max_y = Mathf.Round(Map[0][i].transform.position.y * 100) / 100;
                    break;
                }
            }

            //Debug.Log(min_y+","+max_y);

            if (min_y < 0.85f)
            {
                float[,] end_y = new float[total_row, total_col];
                for (int i = 0; i < total_row; i++)
                {
                    for (int j = 0; j < Map[i].Length; j++)
                    {
                        if (Map[i][j] != null)
                        {
                            end_y[i, j] = Map[i][j].transform.position.y + (0.85f - min_y);
                        }
                    }
                }


                for (int i = 0; i < total_row; i++)
                {
                    for (int j = 0; j < Map[i].Length; j++)
                    {
                        if (Map[i][j] != null)
                        {
                            if (Mathf.Round(Map[i][j].transform.position.y * 100) / 100 < end_y[i, j])
                            {
                                Map[i][j].transform.position = new Vector3(Map[i][j].transform.position.x, Map[i][j].transform.position.y + 0.03f, Map[i][j].transform.position.z);
                            }

                        }
                    }
                }


            }
            else if (min_y > 0.85f && max_y > 4.0f)
            {
                float[,] end_y = new float[total_row, total_col];
                for (int i = 0; i < total_row; i++)
                {
                    for (int j = 0; j < Map[i].Length; j++)
                    {
                        if (Map[i][j] != null)
                            end_y[i, j] = Map[i][j].transform.position.y - (max_y - 4f);
                    }
                }

                for (int i = 0; i < total_row; i++)
                {
                    for (int j = 0; j < Map[i].Length; j++)
                    {
                        if (Map[i][j] != null)
                        {

                            if (Mathf.Round(Map[i][j].transform.position.y * 100) / 100 > end_y[i, j])
                            {
                                Map[i][j].transform.position = new Vector3(Map[i][j].transform.position.x, Map[i][j].transform.position.y - 0.03f, Map[i][j].transform.position.z);

                            }

                        }
                    }
                }


            }
            else if (min_y >= 0.85f && max_y < 4.0f)
            {
                float[,] end_y = new float[total_row, total_col];
                for (int i = 0; i < total_row; i++)
                {
                    for (int j = 0; j < Map[i].Length; j++)
                    {
                        if (Map[i][j] != null)
                            end_y[i, j] = Map[i][j].transform.position.y + (4f - max_y);
                    }
                }

                for (int i = 0; i < total_row; i++)
                {
                    for (int j = 0; j < Map[i].Length; j++)
                    {
                        if (Map[i][j] != null)
                        {

                            if (Mathf.Round(Map[i][j].transform.position.y * 100) / 100 < end_y[i, j])
                            {
                                Map[i][j].transform.position = new Vector3(Map[i][j].transform.position.x, Map[i][j].transform.position.y + 0.03f, Map[i][j].transform.position.z);

                            }

                        }
                    }
                }

            }
            else
            {
                end = true;
            }


            for (int i = Map[0].Length - 1; i >= 0; i--)
            {
                if (Map[0][i] != null)
                {
                    top_y = Mathf.Round(Map[0][i].transform.position.y * 100) / 100 + 0.3f;
                    ceil.transform.position = new Vector3(0f, top_y, 0f);
                    break;
                }
            }

            if (end == true&&end2==true)
            {
                Shooter.possible = true;
                Shooter.starlinepossible = true;
                ing = false;
                start = false;
            }

        }
        else
        {

            //가장 낮은 위치에 있는 구슬 y값 구하기
            float min_y = 0.85f;
            int map_index = 0;
            if (total_row > 0)
            {
                map_index = Map[total_row - 1].Length - 1;

                for (int i = map_index; i >= 0; i--)
                {

                    if (Map[total_row - 1][i] != null)
                    {
                        min_y = Mathf.Round(Map[total_row - 1][i].transform.position.y * 100) / 100;
                        break;
                    }
                }


       
                if (ing == false)
                {
                    bool end = false;
                    Shooter.possible = false;
                    Shooter.starlinepossible = false;
                    //가장 높은 위치에 있는 구슬 y 값 구하기
                    float max_y = 4f;
                    for (int i = Map[0].Length - 1; i >= 0; i--)
                    {
                        if (Map[0][i] != null)
                        {
                            max_y = Mathf.Round(Map[0][i].transform.position.y * 100) / 100;
                            break;
                        }
                    }

                    //Debug.Log(min_y+","+max_y);

                    if (min_y < 0.85f)
                    {
                        float[,] end_y = new float[total_row, total_col];
                        for (int i = 0; i < total_row; i++)
                        {
                            for (int j = 0; j < Map[i].Length; j++)
                            {
                                if (Map[i][j] != null&&Map[i][j].GetComponent<Ball>().connect!=false)
                                {
                                    end_y[i, j] = Map[i][j].transform.position.y + (0.85f - min_y);
                                }
                            }
                        }


                        for (int i = 0; i < total_row; i++)
                        {
                            for (int j = 0; j < Map[i].Length; j++)
                            {
                                if (Map[i][j] != null && Map[i][j].GetComponent<Ball>().connect != false)
                                {
                                    if (Mathf.Round(Map[i][j].transform.position.y * 100) / 100 < end_y[i, j])
                                    {
                                        Map[i][j].transform.position = new Vector3(Map[i][j].transform.position.x, Map[i][j].transform.position.y + 0.05f, Map[i][j].transform.position.z);
                                    }

                                }
                            }
                        }


                    }
                    else if (min_y > 0.85f && max_y > 4.0f)
                    {
                        float[,] end_y = new float[total_row, total_col];
                        for (int i = 0; i < total_row; i++)
                        {
                            for (int j = 0; j < Map[i].Length; j++)
                            {
                                if (Map[i][j] != null && Map[i][j].GetComponent<Ball>().connect != false)
                                    end_y[i, j] = Map[i][j].transform.position.y - (max_y - 4f);
                            }
                        }

                        for (int i = 0; i < total_row; i++)
                        {
                            for (int j = 0; j < Map[i].Length; j++)
                            {
                                if (Map[i][j] != null && Map[i][j].GetComponent<Ball>().connect != false)
                                {

                                    if (Mathf.Round(Map[i][j].transform.position.y * 100) / 100 > end_y[i, j])
                                    {
                                        Map[i][j].transform.position = new Vector3(Map[i][j].transform.position.x, Map[i][j].transform.position.y - 0.05f, Map[i][j].transform.position.z);

                                    }

                                }
                            }
                        }

                        if (Mathf.Round(night.transform.position.y * 100) / 100 > -6f)
                        {
                            night.transform.position = new Vector3(night.transform.position.x, night.transform.position.y - 0.1f, night.transform.position.z);
                            star.transform.position = new Vector3(star.transform.position.x, star.transform.position.y - 0.1f, star.transform.position.z);
                            left_wall.transform.position = new Vector3(left_wall.transform.position.x, left_wall.transform.position.y - 0.1f, left_wall.transform.position.z);
                            right_wall.transform.position = new Vector3(right_wall.transform.position.x, right_wall.transform.position.y - 0.1f, right_wall.transform.position.z);
                        }


                    }
                    else if (min_y >= 0.85f && max_y < 4.0f)
                    {
                        float[,] end_y = new float[total_row, total_col];
                        for (int i = 0; i < total_row; i++)
                        {
                            for (int j = 0; j < Map[i].Length; j++)
                            {
                                if (Map[i][j] != null && Map[i][j].GetComponent<Ball>().connect != false)
                                    end_y[i, j] = Map[i][j].transform.position.y + (4f - max_y);
                            }
                        }

                        for (int i = 0; i < total_row; i++)
                        {
                            for (int j = 0; j < Map[i].Length; j++)
                            {
                                if (Map[i][j] != null && Map[i][j].GetComponent<Ball>().connect != false)
                                {

                                    if (Mathf.Round(Map[i][j].transform.position.y * 100) / 100 < end_y[i, j])
                                    {
                                        Map[i][j].transform.position = new Vector3(Map[i][j].transform.position.x, Map[i][j].transform.position.y + 0.05f, Map[i][j].transform.position.z);

                                    }

                                }
                            }
                        }

                    }
                    else
                    {
                        end = true;
                    }


                    for (int i = Map[0].Length - 1; i >= 0; i--)
                    {
                        if (Map[0][i] != null)
                        {
                            top_y = Mathf.Round(Map[0][i].transform.position.y * 100) / 100 + 0.3f;
                            ceil.transform.position = new Vector3(0f, top_y, 0f);
                            break;
                        }
                    }

                    if (min_y >= 0.85f && Ball.discon_total == Ball.discon_cnt && end == true && Ball.anim_cnt == 0)
                    {
                        Shooter.possible = true;
                        Shooter.starlinepossible = true;
                        end = false;
                    }
                }
                
            }
        }

    }

    public void bubblepop()//구슬 생성 함수
    {
        //발사할 첫번째 구슬 위치를 대포 위치로 변경
        ballPrefabs[0].transform.position = shotspawn.transform.position;
        //ballPrefabs[0].transform.rotation = shotspawn.transform.rotation;
        ballPrefabs[0].GetComponent<Ball>().shootball = true;

        //두번째 발사 구슬을 첫번째 발사 구슬로 바꾸고 첫번째 구슬 위치로 변경
        if (ballPrefabs[0].tag == "red")
        {
            redCnt++;
        }
        else if (ballPrefabs[0].tag == "yellow")
        {
            yelCnt++;
        }
        else if (ballPrefabs[0].tag == "green")
        {
            greCnt++;
        }
        else if (ballPrefabs[0].tag == "blue")
        {
            bluCnt++;
        }
        else if (ballPrefabs[0].tag == "purple")
        {
            purCnt++;
        }

        ballPrefabs[0] = ballPrefabs[1];
        ballPrefabs[1] = null;
        ballPrefabs[0].transform.position = new Vector3(0f, -3.5f, 0f);
        

     //구슬 배열의 마지막 순서의 색깔을 정해줍니다.

        float a;
        a = Random.Range(0.0f, 100.0f);
        
        if ((a >= 0.0f) && (a < purpl))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[4], new Vector3(1.5f, -3.7f, 0f), Quaternion.identity);
        }// (0~ 20퍼) 보라색
        else if ((a >= purpl) && (a < re))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[0], new Vector3(1.5f, -3.7f, 0f), Quaternion.identity);
        }//(20~40퍼) 레드
        else if ((a >= re) && (a < blu))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[3], new Vector3(1.5f, -3.7f, 0f), Quaternion.identity);
        }//(40~60퍼) 블루
        else if ((a >= blu) && (a < yello))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[1], new Vector3(1.5f, -3.7f, 0f), Quaternion.identity);
        }//(60~80퍼) 노랑
        else if ((a >= yello) && (a <= 100.0))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[2], new Vector3(1.5f, -3.7f, 0f), Quaternion.identity);
        }


        recentballs[recent_idx%5] = ballPrefabs[1].tag;
        recent_idx++;
    }

    
}
