using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooter : MonoBehaviour
{
    static public bool possible; //대포 발사 가능한 시점과 불가능한 시점 구분 용도
    Vector3 touchPos;
    Vector3 colPos;
    bgmmanager bgm;//스테이지의 배경음악의 매개체이자 효과음의 매개체
    semmanager sem;//스크립트에서 함수 불러옴
    float degree;
    float redegree;
    float yPos;
    float radian;
    private float offest;
    bool colcheck = false;
    bool ch_colcheck = false;
    bool ch_ballcheck = false; //길잡이 캐릭터의 경로가 구슬과 충돌했는지 판단하는 변수
    public GameObject[] characters = new GameObject[4];
    bool lastCol = false;
    bool reflect = false;

    //public GameObject reflectline;
    //public GameObject ch_reflectline; //길잡이 캐릭터의 경로(ch_로 시작하는 모든 변수)
    public GameObject starline; //길잡이 캐릭터 경로 중 일부

    SpriteRenderer sprite; //대포(경로) 이미지
    //SpriteRenderer resprite;
    public Color color;
    //public Color recolor;

    //SpriteRenderer ch_resprite;
    //public Color ch_recolor;

    public static bool starlinepossible;
    SpriteRenderer starlineResprite; //일반 경로
    public Color starlineRecolor;

    public static bool restarlinepossible;
    SpriteRenderer restarlineResprite; //반사 경로
    public Color restarlineRecolor;

    public int jdg;
    int ChType = Character.ChType;
    Object starobj;

    List<GameObject> StarLine = new List<GameObject>(); //일반 경로 배열
    List<GameObject> NStarLine = new List<GameObject>();
    List<GameObject> reStarLine = new List<GameObject>(); //반사 경로 배열
    List<GameObject> reNStarLine = new List<GameObject>();

    public static float GetAngle(Vector3 from, Vector3 to)
    {
        Vector3 v = to - from;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    // Start is called before the first frame update
    void Start()
    {
        starInit();
        starlinepossible = true;
        restarInit();
        possible = true;

        NstarInit();
        reNstarInit();

        for (int i = 0; i < 20; i++) //길잡이-일반
        {
            starlineResprite = StarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 라인
            starlineRecolor = starlineResprite.color;
            starlineRecolor.a = 0f;
            starlineResprite.color = starlineRecolor;
        }

        for (int i = 0; i < 12; i++) //일반-일반
        {
            starlineResprite = NStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 라인
            starlineRecolor = starlineResprite.color;
            starlineRecolor.a = 0f;
            starlineResprite.color = starlineRecolor;
        }

        for (int i = 0; i < 15; i++) //길잡이-반사
        {
            restarlineResprite = reStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
            restarlineRecolor = restarlineResprite.color;
            restarlineRecolor.a = 0f;
            restarlineResprite.color = restarlineRecolor;
        }

        for (int i = 0; i < 5; i++) //일반-반사
        {
            restarlineResprite = reNStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
            restarlineRecolor = restarlineResprite.color;
            restarlineRecolor.a = 0f;
            restarlineResprite.color = restarlineRecolor;
        }



        //sprite = this.gameObject.GetComponent<SpriteRenderer>();
        //color = sprite.color;
        //color.a = 0f; //시작할 때 투명함
        //sprite.color = color;

        /*
        resprite = reflectline.GetComponent<SpriteRenderer>(); //반사 라인
        recolor = resprite.color;
        recolor.a = 0f; //시작할 때 투명함
        resprite.color = recolor;

        

        ch_resprite = ch_reflectline.GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
        ch_recolor = ch_resprite.color;
        ch_recolor.a = 0f; //시작할 때 투명함
        ch_resprite.color = ch_recolor;
        */
        /*
        for(int i = 0; i < 20; i++)
        {
            starlineResprite = StarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
            starlineRecolor = starlineResprite.color;
            starlineRecolor.a = 0f; //시작할 때 투명함
            starlineResprite.color = starlineRecolor;
        }*/


        bgm = FindObjectOfType<bgmmanager>();
        bgm.stop();
        bgm.play(1);
        sem = FindObjectOfType<semmanager>();
    }

    private void Awake()
    {
        starInit(); //처음 시작할때 starline 비활성화
        starlinepossible = true;
        restarInit(); //restarline 비활성화
        possible = true;
    }

    // Update is called once per frame
    void Update()
    {


        if (Manager.queCnt == 0)
        {
            //Time.timeScale = 0f;
            Manager.clear = true;

        }
        else if (Manager.limit_cnt == 0)
        {
            //Time.timeScale = 0f;
            Manager.fail = true;
        }
        else
        {
            Time.timeScale = 1f;
#if (UNITY_ANDROID || UNITY_IOS)


            if ((Input.touchCount > 0) && (GameObject.Find("Optionbutton").GetComponent<optionbuttontouch>().isPressed == false))
            {
            
                //화면 touch 처음 하나만 인식
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began) //화면을 touch한 순간
                {
                    touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, -Camera.main.transform.position.z));
                    degree = GetAngle(this.gameObject.transform.position, touchPos) - 90;
                    if (-80 < degree && degree < 80)
                        this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, degree); //여기부터 추가함

                    colPos = this.gameObject.GetComponent<Transform>().position; //충돌 위치
                    radian = degree * Mathf.PI / 180; // 충돌 각
                    yPos = 2.3f / Mathf.Tan(radian);

                    float stard = 0f; //별빛 간 간격
                    int starlinenum = 19; //구슬과 닿은 첫 번째 별빛 알기 위한 변수
                    if (Shooter.possible == true && (-80 < degree && degree < 80)) //대포가 동작하고, 가동 범위내 일때
                    {
                        if (ChType == 1) //길잡이 토끼
                        {
                            for (int i = 0; i < 20; i++)
                            {
                                StarLine[i].transform.position = new Vector3(stard * -Mathf.Sin(radian), -3f + stard * Mathf.Cos(radian), 0);
                                StarLine[i].transform.rotation = Quaternion.Euler(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
                                stard += 0.4f;
                                StarLine[i].SetActive(true);
                                //StarLine[i].SetActive(true);
                                //GameObject.Find("starline1").GetComponent<Line>().isBallCheck();

                            }

                            for (int i = 0; i < 20; i++) //처음 구슬과 닿은 별빛의 위치 i를 알기 위함
                            {
                                starlineResprite = StarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 라인
                                starlineRecolor = starlineResprite.color;
                                if (starlineRecolor.a == 0f)
                                {
                                    starlinenum = i;
                                    break;
                                }
                            }

                            for (int i = starlinenum; i < 20; i++) //StarLine[i]~[19]까지 모두 투명하게 만듦
                            {
                                starlineResprite = StarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 라인
                                starlineRecolor = starlineResprite.color;
                                starlineRecolor.a = 0f;
                                starlineResprite.color = starlineRecolor;
                            }
                        }

                        else
                        {
                            starlinenum = 8; //길잡이 토끼 아닐 때
                            for (int i = 0; i < 9; i++)
                            {
                                StarLine[i].SetActive(true);
                                StarLine[i].transform.position = new Vector3(stard * -Mathf.Sin(radian), -3f + stard * Mathf.Cos(radian), 0);
                                StarLine[i].transform.rotation = Quaternion.Euler(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
                                stard += 0.4f;

                            }
                        }
                    }

                    if (ChType == 1) // 일반 라인이 투명하다면, 반사라인은 무조건 투명
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            starlineResprite = StarLine[i].GetComponent<SpriteRenderer>();
                            starlineRecolor = starlineResprite.color;
                            if (starlineRecolor.a == 0f)
                            {
                                lastCol = false;
                                break;
                            }
                            else
                            {
                                lastCol = true;
                            }
                        }
                    }

                    /*
                    else
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            starlineResprite = StarLine[i].GetComponent<SpriteRenderer>();
                            starlineRecolor = starlineResprite.color;
                            if (starlineRecolor.a != 0f)
                            {
                                lastCol = true;

                            }
                            else
                            {
                                lastCol = false;
                                break;
                            }
                        }
                    }*/





                    if (ShooterLine.linecol) //경로와 벽이 충돌중인지 확인, 충돌중이라면 반사 경로 보여줌
                    {
                        if (80 > degree && degree > 0) //왼쪽 벽과 맞닿는 반사 경로
                        {
                            if (ChType == 1) //길잡이 토끼
                            {
                                //ch_reflectline.transform.position = new Vector3(-2.3f, -3f + yPos, 0);
                                //ch_reflectline.transform.rotation = Quaternion.Euler(0f, 0f, -degree);


                                if (Shooter.possible == true)
                                {
                                    float restard = 0f;
                                    int restarlinenum = 14;
                                    for (int i = 0; i < 15; i++)
                                    {
                                        reStarLine[i].transform.position = new Vector3(-2.3f + restard * Mathf.Sin(radian), -3f + yPos + restard * Mathf.Cos(radian), 0);
                                        reStarLine[i].transform.rotation = Quaternion.Euler(0f, 0f, -degree);
                                        restard += 0.4f;
                                        reStarLine[i].SetActive(true);
                                        //StarLine[i].SetActive(true);
                                        //GameObject.Find("starline1").GetComponent<Line>().isBallCheck();

                                    }

                                    for (int i = 0; i < 15; i++)
                                    {
                                        restarlineResprite = reStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                        restarlineRecolor = restarlineResprite.color;
                                        if (restarlineRecolor.a == 0f)
                                        {
                                            restarlinenum = i;
                                            break;
                                        }
                                    }

                                    for (int i = restarlinenum; i < 15; i++)
                                    {
                                        restarlineResprite = reStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                        restarlineRecolor = restarlineResprite.color;
                                        restarlineRecolor.a = 0f;
                                        restarlineResprite.color = restarlineRecolor;
                                    }
                                }
                            }

                            else //길잡이 토끼 아닐 때
                            {
                                //reflectline.transform.position = new Vector3(-2.3f, -3f + yPos, 0);
                                //reflectline.transform.rotation = Quaternion.Euler(0f, 0f, -degree);

                                if (Shooter.possible == true)
                                {
                                    float restard = 0f;
                                    int restarlinenum = 4;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        reStarLine[i].transform.position = new Vector3(-2.3f + restard * Mathf.Sin(radian), -3f + yPos + restard * Mathf.Cos(radian), 0);
                                        reStarLine[i].transform.rotation = Quaternion.Euler(0f, 0f, -degree);
                                        restard += 0.4f;
                                        reStarLine[i].SetActive(true);

                                    }


                                }



                            }

                        }
                        else if (-80 < degree && degree < 0) //오른쪽 벽과 맞닿는 반사 경로
                        {
                            if (ChType == 1) //길잡이 토끼
                            {
                                //ch_reflectline.transform.position = new Vector3(2.3f, -3f - yPos, 0);
                                //ch_reflectline.transform.rotation = Quaternion.Euler(0f, 0f, -degree);



                                if (Shooter.possible == true)
                                {
                                    float restard = 0f;
                                    int restarlinenum = 14;
                                    for (int i = 0; i < 15; i++)
                                    {
                                        reStarLine[i].transform.position = new Vector3(2.3f + restard * Mathf.Sin(radian), -3f - yPos + restard * Mathf.Cos(radian), 0);
                                        reStarLine[i].transform.rotation = Quaternion.Euler(0f, 0f, -degree);
                                        restard += 0.4f;
                                        reStarLine[i].SetActive(true);
                                        //StarLine[i].SetActive(true);
                                        //GameObject.Find("starline1").GetComponent<Line>().isBallCheck();

                                    }

                                    for (int i = 0; i < 15; i++)
                                    {
                                        restarlineResprite = reStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                        restarlineRecolor = restarlineResprite.color;
                                        if (restarlineRecolor.a == 0f)
                                        {
                                            restarlinenum = i;
                                            break;
                                        }
                                    }

                                    for (int i = restarlinenum; i < 15; i++)
                                    {
                                        restarlineResprite = reStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                        restarlineRecolor = restarlineResprite.color;
                                        restarlineRecolor.a = 0f;
                                        restarlineResprite.color = restarlineRecolor;
                                    }
                                }
                            }

                            else
                            { //길잡이 토끼 아닐 때
                              //reflectline.transform.position = new Vector3(2.3f, -3f - yPos, 0);
                              //reflectline.transform.rotation = Quaternion.Euler(0f, 0f, -degree);


                                if (Shooter.possible == true)
                                {
                                    float restard = 0f;
                                    int restarlinenum = 4;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        reStarLine[i].transform.position = new Vector3(2.3f + restard * Mathf.Sin(radian), -3f - yPos + restard * Mathf.Cos(radian), 0);
                                        reStarLine[i].transform.rotation = Quaternion.Euler(0f, 0f, -degree);
                                        restard += 0.4f;
                                        reStarLine[i].SetActive(true);
                                        //StarLine[i].SetActive(true);
                                        //GameObject.Find("starline1").GetComponent<Line>().isBallCheck();

                                    }


                                }
                            }

                        }

                    }

                    if (ChType == 1) //길잡이 토끼에서 벽과 궤적이 충돌하지 않으면 반사경로를 투명하게 함
                    {
                        if ((StarLine[19].transform.position.x >= -2.5f) && (StarLine[19].transform.position.x <= 2.5f))
                        {
                            for (int i = 0; i < 15; i++)
                            {
                                reStarLine[i].SetActive(false);
                            }
                        }

                        else
                        {
                            for (int i = 0; i < 15; i++)
                            {
                                reStarLine[i].SetActive(true);
                            }
                        }
                    }

                    else //길잡이 토끼 이외 다른 토끼에서 벽과 궤적 미충돌시 반사 경로 투명하게 함
                    {
                        if ((StarLine[8].transform.position.x >= -2.5f) && (StarLine[8].transform.position.x <= 2.5f))
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                restarlineResprite = reStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                restarlineRecolor = restarlineResprite.color;
                                restarlineRecolor.a = 0f;
                                restarlineResprite.color = restarlineRecolor;
                            }
                        }

                        else if ((StarLine[8].transform.position.x <= -2.5f) || (StarLine[8].transform.position.x >= 2.5f))
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                restarlineResprite = reStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                restarlineRecolor = restarlineResprite.color;
                                restarlineRecolor.a = 1f;
                                restarlineResprite.color = restarlineRecolor;
                            }
                        }
                    }
                }
                if (touch.phase == TouchPhase.Moved) //손가락이 화면 위에서 터치한 상태로 이동
                {
                    touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, -Camera.main.transform.position.z));
                    degree = GetAngle(this.gameObject.transform.position, touchPos) - 90;
                    if (-80 < degree && degree < 80)
                        this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, degree); //여기부터 추가

                    colPos = this.gameObject.GetComponent<Transform>().position; //충돌 위치
                    radian = degree * Mathf.PI / 180; // 충돌 각
                    yPos = 2.3f / Mathf.Tan(radian);

                    float stard = 0f; //별빛 간 간격
                    int starlinenum = 19; //구슬과 닿은 첫 번째 별빛 알기 위한 변수
                    if (Shooter.possible == true && (-80 < degree && degree < 80)) //대포가 동작하고, 가동 범위내 일때
                    {
                        if (ChType == 1) //길잡이 토끼
                        {
                            for (int i = 0; i < 20; i++)
                            {
                                StarLine[i].transform.position = new Vector3(stard * -Mathf.Sin(radian), -3f + stard * Mathf.Cos(radian), 0);
                                StarLine[i].transform.rotation = Quaternion.Euler(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
                                stard += 0.4f;
                                StarLine[i].SetActive(true);
                                //StarLine[i].SetActive(true);
                                //GameObject.Find("starline1").GetComponent<Line>().isBallCheck();

                            }

                            for (int i = 0; i < 20; i++) //처음 구슬과 닿은 별빛의 위치 i를 알기 위함
                            {
                                starlineResprite = StarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 라인
                                starlineRecolor = starlineResprite.color;
                                if (starlineRecolor.a == 0f)
                                {
                                    starlinenum = i;
                                    break;
                                }
                            }

                            for (int i = starlinenum; i < 20; i++) //StarLine[i]~[19]까지 모두 투명하게 만듦
                            {
                                starlineResprite = StarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 라인
                                starlineRecolor = starlineResprite.color;
                                starlineRecolor.a = 0f;
                                starlineResprite.color = starlineRecolor;
                            }
                        }

                        else
                        {
                            starlinenum = 8; //길잡이 토끼 아닐 때
                            for (int i = 0; i < 9; i++)
                            {
                                StarLine[i].SetActive(true);
                                StarLine[i].transform.position = new Vector3(stard * -Mathf.Sin(radian), -3f + stard * Mathf.Cos(radian), 0);
                                StarLine[i].transform.rotation = Quaternion.Euler(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
                                stard += 0.4f;

                            }
                        }
                    }

                    if (ChType == 1) // 일반 라인이 투명하다면, 반사라인은 무조건 투명
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            starlineResprite = StarLine[i].GetComponent<SpriteRenderer>();
                            starlineRecolor = starlineResprite.color;
                            if (starlineRecolor.a == 0f)
                            {
                                lastCol = false;
                                break;
                            }
                            else
                            {
                                lastCol = true;
                            }
                        }
                    }

                    /*
                    else
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            starlineResprite = StarLine[i].GetComponent<SpriteRenderer>();
                            starlineRecolor = starlineResprite.color;
                            if (starlineRecolor.a != 0f)
                            {
                                lastCol = true;

                            }
                            else
                            {
                                lastCol = false;
                                break;
                            }
                        }
                    }*/





                    if (ShooterLine.linecol) //경로와 벽이 충돌중인지 확인, 충돌중이라면 반사 경로 보여줌
                    {
                        if (80 > degree && degree > 0) //왼쪽 벽과 맞닿는 반사 경로
                        {
                            if (ChType == 1) //길잡이 토끼
                            {
                                //ch_reflectline.transform.position = new Vector3(-2.3f, -3f + yPos, 0);
                                //ch_reflectline.transform.rotation = Quaternion.Euler(0f, 0f, -degree);


                                if (Shooter.possible == true)
                                {
                                    float restard = 0f;
                                    int restarlinenum = 14;
                                    for (int i = 0; i < 15; i++)
                                    {
                                        reStarLine[i].transform.position = new Vector3(-2.3f + restard * Mathf.Sin(radian), -3f + yPos + restard * Mathf.Cos(radian), 0);
                                        reStarLine[i].transform.rotation = Quaternion.Euler(0f, 0f, -degree);
                                        restard += 0.4f;
                                        reStarLine[i].SetActive(true);
                                        //StarLine[i].SetActive(true);
                                        //GameObject.Find("starline1").GetComponent<Line>().isBallCheck();

                                    }

                                    for (int i = 0; i < 15; i++)
                                    {
                                        restarlineResprite = reStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                        restarlineRecolor = restarlineResprite.color;
                                        if (restarlineRecolor.a == 0f)
                                        {
                                            restarlinenum = i;
                                            break;
                                        }
                                    }

                                    for (int i = restarlinenum; i < 15; i++)
                                    {
                                        restarlineResprite = reStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                        restarlineRecolor = restarlineResprite.color;
                                        restarlineRecolor.a = 0f;
                                        restarlineResprite.color = restarlineRecolor;
                                    }
                                }
                            }

                            else //길잡이 토끼 아닐 때
                            {
                                //reflectline.transform.position = new Vector3(-2.3f, -3f + yPos, 0);
                                //reflectline.transform.rotation = Quaternion.Euler(0f, 0f, -degree);

                                if (Shooter.possible == true)
                                {
                                    float restard = 0f;
                                    int restarlinenum = 4;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        reStarLine[i].transform.position = new Vector3(-2.3f + restard * Mathf.Sin(radian), -3f + yPos + restard * Mathf.Cos(radian), 0);
                                        reStarLine[i].transform.rotation = Quaternion.Euler(0f, 0f, -degree);
                                        restard += 0.4f;
                                        reStarLine[i].SetActive(true);

                                    }


                                }



                            }

                        }
                        else if (-80 < degree && degree < 0) //오른쪽 벽과 맞닿는 반사 경로
                        {
                            if (ChType == 1) //길잡이 토끼
                            {
                                //ch_reflectline.transform.position = new Vector3(2.3f, -3f - yPos, 0);
                                //ch_reflectline.transform.rotation = Quaternion.Euler(0f, 0f, -degree);



                                if (Shooter.possible == true)
                                {
                                    float restard = 0f;
                                    int restarlinenum = 14;
                                    for (int i = 0; i < 15; i++)
                                    {
                                        reStarLine[i].transform.position = new Vector3(2.3f + restard * Mathf.Sin(radian), -3f - yPos + restard * Mathf.Cos(radian), 0);
                                        reStarLine[i].transform.rotation = Quaternion.Euler(0f, 0f, -degree);
                                        restard += 0.4f;
                                        reStarLine[i].SetActive(true);
                                        //StarLine[i].SetActive(true);
                                        //GameObject.Find("starline1").GetComponent<Line>().isBallCheck();

                                    }

                                    for (int i = 0; i < 15; i++)
                                    {
                                        restarlineResprite = reStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                        restarlineRecolor = restarlineResprite.color;
                                        if (restarlineRecolor.a == 0f)
                                        {
                                            restarlinenum = i;
                                            break;
                                        }
                                    }

                                    for (int i = restarlinenum; i < 15; i++)
                                    {
                                        restarlineResprite = reStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                        restarlineRecolor = restarlineResprite.color;
                                        restarlineRecolor.a = 0f;
                                        restarlineResprite.color = restarlineRecolor;
                                    }
                                }
                            }

                            else
                            { //길잡이 토끼 아닐 때
                              //reflectline.transform.position = new Vector3(2.3f, -3f - yPos, 0);
                              //reflectline.transform.rotation = Quaternion.Euler(0f, 0f, -degree);


                                if (Shooter.possible == true)
                                {
                                    float restard = 0f;
                                    int restarlinenum = 4;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        reStarLine[i].transform.position = new Vector3(2.3f + restard * Mathf.Sin(radian), -3f - yPos + restard * Mathf.Cos(radian), 0);
                                        reStarLine[i].transform.rotation = Quaternion.Euler(0f, 0f, -degree);
                                        restard += 0.4f;
                                        reStarLine[i].SetActive(true);
                                        //StarLine[i].SetActive(true);
                                        //GameObject.Find("starline1").GetComponent<Line>().isBallCheck();

                                    }


                                }
                            }

                        }

                    }

                    if (ChType == 1) //길잡이 토끼에서 벽과 궤적이 충돌하지 않으면 반사경로를 투명하게 함
                    {
                        if ((StarLine[19].transform.position.x >= -2.5f) && (StarLine[19].transform.position.x <= 2.5f))
                        {
                            for (int i = 0; i < 15; i++)
                            {
                                reStarLine[i].SetActive(false);
                            }
                        }

                        else
                        {
                            for (int i = 0; i < 15; i++)
                            {
                                reStarLine[i].SetActive(true);
                            }
                        }
                    }

                    else //길잡이 토끼 이외 다른 토끼에서 벽과 궤적 미충돌시 반사 경로 투명하게 함
                    {
                        if ((StarLine[8].transform.position.x >= -2.5f) && (StarLine[8].transform.position.x <= 2.5f))
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                restarlineResprite = reStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                restarlineRecolor = restarlineResprite.color;
                                restarlineRecolor.a = 0f;
                                restarlineResprite.color = restarlineRecolor;
                            }
                        }

                        else if ((StarLine[8].transform.position.x <= -2.5f) || (StarLine[8].transform.position.x >= 2.5f))
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                restarlineResprite = reStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                restarlineRecolor = restarlineResprite.color;
                                restarlineRecolor.a = 1f;
                                restarlineResprite.color = restarlineRecolor;
                            }
                        }
                    }
                }

                if (touch.phase == TouchPhase.Ended) //손가락이 화면에서 떨어지면 touch가 끝난 경우
                {
                    if (-80 < degree && degree < 80 && possible == true && Manager.limit_cnt != 0)
                    { //회전각도 조정, 과정 끝날때까지 작동안하게, 구슬갯수제한 끝나면 작동안하게
                        sem.play(0);
                        GameObject.Find("GameObject").GetComponent<Manager>().bubblepop();
                        possible = false;//연결되지 않은 게 떨어지기 전에 shooter 동작안하게
                        Manager.limit_cnt--; //제한 구슬 갯수 감소

                        for (int i = 0; i < 20; i++)
                        {
                            StarLine[i].SetActive(false); //터치 떼면 경로 비활성화
                        }

                        for (int i = 0; i < 15; i++)
                        {
                            reStarLine[i].SetActive(false); //터치 떼면 반사경로 비활성화
                        }

                        if (-80 < degree && degree < 80 && possible == true && Manager.limit_cnt != 0 && starlinepossible == true) //회전각도 조정, 과정 끝날때까지 작동안하게, 구슬갯수제한 끝나면 작동안하게
                        {
                            if (Character.ChType == 0)
                            {
                                characters[0].GetComponent<Animator>().SetTrigger("shooting");
                            }
                            else if (Character.ChType == 1)
                            {
                                characters[1].GetComponent<Animator>().SetTrigger("shooting");
                            }
                            else if (Character.ChType == 2)
                            {
                                characters[2].GetComponent<Animator>().SetTrigger("shooting");
                            }
                            else if (Character.ChType == 3)
                            {
                                characters[3].GetComponent<Animator>().SetTrigger("shooting");
                            }


                            starlinepossible = false;
                            Manager.ing = true;
                        }
                    }
                }
            }
#else
            if ((Input.GetMouseButton(0))&&(GameObject.Find("Optionbutton").GetComponent<optionbuttontouch>().isPressed == false)&& (GameObject.Find("ChangeBall").GetComponent<ChangeBall>().isPressed == false))
            {//설정 버튼이 눌리지 않을때 대포의 궤적을 조절하고 발사할 수 있음
                //color.a = 1f; //터치하고 있으면(누르고 있으면) 경로 보임
                //sprite.color = color;
                touchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                degree = GetAngle(this.gameObject.transform.position, touchPos) - 90;
                if (-80 < degree && degree < 80)
                    this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, degree);

                colPos = this.gameObject.GetComponent<Transform>().position; //충돌 위치
                radian = degree * Mathf.PI / 180; // 충돌 각
                yPos = 2.3f / Mathf.Tan(radian);

                float stard = 0f; //별빛 간 간격
                int starlinenum = 19; //구슬과 닿은 첫 번째 별빛 알기 위한 변수
                if (Shooter.possible == true && (-80 < degree && degree < 80)) //대포가 동작하고, 가동 범위내 일때
                {
                    if (ChType == 1) //길잡이 토끼
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            StarLine[i].transform.position = new Vector3(stard * -Mathf.Sin(radian), -3f + stard * Mathf.Cos(radian), 0);
                            StarLine[i].transform.rotation = Quaternion.Euler(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
                            stard += 0.3f;
                            //StarLine[i].SetActive(true);
                            //StarLine[i].SetActive(true);
                            //GameObject.Find("starline1").GetComponent<Line>().isBallCheck();

                        }

                        for (int i = 0; i < 20; i++) //처음 구슬과 닿은 별빛의 위치 i를 알기 위함
                        {
                            starlineResprite = StarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 라인
                            starlineRecolor = starlineResprite.color;
                            if (starlineRecolor.a == 0f)
                            {
                                starlinenum = i;
                                break;
                            }
                        }

                        for (int i = starlinenum; i < 20; i++) //StarLine[i]~[19]까지 모두 투명하게 만듦
                        {
                            starlineResprite = StarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 라인
                            starlineRecolor = starlineResprite.color;
                            starlineRecolor.a = 0f;
                            starlineResprite.color = starlineRecolor;
                        }
                    }

                    else //일반-일반
                    {
                        stard = 0f;
                        for (int i = 0; i < 12; i++)
                        {
                            //NStarLine[i].SetActive(true);
                            NStarLine[i].transform.position = new Vector3(stard * -Mathf.Sin(radian), -3f + stard * Mathf.Cos(radian), 0);
                            NStarLine[i].transform.rotation = Quaternion.Euler(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
                            stard += 0.3f;

                            starlineResprite = NStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 라인
                            starlineRecolor = starlineResprite.color;
                            starlineRecolor.a = 1f;
                            starlineResprite.color = starlineRecolor;
                        }
                    }
                }

                if (ChType == 1) // 일반 라인이 투명하다면, 반사라인은 무조건 투명
                {
                    for (int i = 0; i < 20; i++)
                    {
                        starlineResprite = StarLine[i].GetComponent<SpriteRenderer>();
                        starlineRecolor = starlineResprite.color;
                        if (starlineRecolor.a == 0f)
                        {
                            lastCol = false;
                            break;
                        }
                        else
                        {
                            lastCol = true;
                        }
                    }
                }

                /*
                else
                {
                    for (int i = 0; i < 9; i++)
                    {
                        starlineResprite = StarLine[i].GetComponent<SpriteRenderer>();
                        starlineRecolor = starlineResprite.color;
                        if (starlineRecolor.a != 0f)
                        {
                            lastCol = true;
                            
                        }
                        else
                        {
                            lastCol = false;
                            break;
                        }
                    }
                }*/





                if (ShooterLine.linecol) //경로와 벽이 충돌중인지 확인, 충돌중이라면 반사 경로 보여줌
                {
                    if (80 > degree && degree > 0) //왼쪽 벽과 맞닿는 반사 경로
                    {
                        if (ChType == 1) //길잡이 토끼
                        {
                            //ch_reflectline.transform.position = new Vector3(-2.3f, -3f + yPos, 0);
                            //ch_reflectline.transform.rotation = Quaternion.Euler(0f, 0f, -degree);


                            if (Shooter.possible == true)
                            {
                                float restard = 0f;
                                int restarlinenum = 14;
                                for (int i = 0; i < 15; i++)
                                {
                                    reStarLine[i].transform.position = new Vector3(-2.3f + restard * Mathf.Sin(radian), -3f + yPos + restard * Mathf.Cos(radian), 0);
                                    reStarLine[i].transform.rotation = Quaternion.Euler(0f, 0f, -degree);
                                    restard += 0.3f;
                                    reStarLine[i].SetActive(true);
                                    //StarLine[i].SetActive(true);
                                    //GameObject.Find("starline1").GetComponent<Line>().isBallCheck();

                                }

                                for (int i = 0; i < 15; i++)
                                {
                                    restarlineResprite = reStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                    restarlineRecolor = restarlineResprite.color;
                                    if (restarlineRecolor.a == 0f)
                                    {
                                        restarlinenum = i;
                                        break;
                                    }
                                }

                                for (int i = restarlinenum; i < 15; i++)
                                {
                                    restarlineResprite = reStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                    restarlineRecolor = restarlineResprite.color;
                                    restarlineRecolor.a = 0f;
                                    restarlineResprite.color = restarlineRecolor;
                                }
                            }
                        }

                        else //일반-반사
                        {
                            //reflectline.transform.position = new Vector3(-2.3f, -3f + yPos, 0);
                            //reflectline.transform.rotation = Quaternion.Euler(0f, 0f, -degree);

                            if (Shooter.possible == true)
                            {
                                float restard = 0f;
                                for (int i = 0; i < 5; i++)
                                {
                                    //reNStarLine[i].SetActive(true);
                                    reNStarLine[i].transform.position = new Vector3(-2.3f + restard * Mathf.Sin(radian), -3f + yPos + restard * Mathf.Cos(radian), 0);
                                    reNStarLine[i].transform.rotation = Quaternion.Euler(0f, 0f, -degree);
                                    restard += 0.3f;

                                    restarlineResprite = reNStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                    restarlineRecolor = restarlineResprite.color;
                                    restarlineRecolor.a = 1f;
                                    restarlineResprite.color = restarlineRecolor;

                                }
                                
                                
                            }



                        }

                    }
                    else if (-80 < degree && degree < 0) //오른쪽 벽과 맞닿는 반사 경로
                    {
                        if (ChType == 1) //길잡이 토끼
                        {
                            //ch_reflectline.transform.position = new Vector3(2.3f, -3f - yPos, 0);
                            //ch_reflectline.transform.rotation = Quaternion.Euler(0f, 0f, -degree);



                            if (Shooter.possible == true)
                            {
                                float restard = 0f;
                                int restarlinenum = 14;
                                for (int i = 0; i < 15; i++)
                                {
                                    reStarLine[i].transform.position = new Vector3(2.3f + restard * Mathf.Sin(radian), -3f - yPos + restard * Mathf.Cos(radian), 0);
                                    reStarLine[i].transform.rotation = Quaternion.Euler(0f, 0f, -degree);
                                    restard += 0.3f;
                                    reStarLine[i].SetActive(true);
                                    //StarLine[i].SetActive(true);
                                    //GameObject.Find("starline1").GetComponent<Line>().isBallCheck();

                                }

                                for (int i = 0; i < 15; i++)
                                {
                                    restarlineResprite = reStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                    restarlineRecolor = restarlineResprite.color;
                                    if (restarlineRecolor.a == 0f)
                                    {
                                        restarlinenum = i;
                                        break;
                                    }
                                }

                                for (int i = restarlinenum; i < 15; i++)
                                {
                                    restarlineResprite = reStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                    restarlineRecolor = restarlineResprite.color;
                                    restarlineRecolor.a = 0f;
                                    restarlineResprite.color = restarlineRecolor;
                                }
                            }
                        }

                        else //일반-반사
                        { //길잡이 토끼 아닐 때
                            //reflectline.transform.position = new Vector3(2.3f, -3f - yPos, 0);
                            //reflectline.transform.rotation = Quaternion.Euler(0f, 0f, -degree);


                            if (Shooter.possible == true)
                            {
                                float restard = 0f;
                                for (int i = 0; i < 5; i++)
                                {
                                    //reNStarLine[i].SetActive(true);
                                    reNStarLine[i].transform.position = new Vector3(2.3f + restard * Mathf.Sin(radian), -3f - yPos + restard * Mathf.Cos(radian), 0);
                                    reNStarLine[i].transform.rotation = Quaternion.Euler(0f, 0f, -degree);
                                    restard += 0.3f;
                                    //StarLine[i].SetActive(true);
                                    //GameObject.Find("starline1").GetComponent<Line>().isBallCheck();

                                    restarlineResprite = reNStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                    restarlineRecolor = restarlineResprite.color;
                                    restarlineRecolor.a = 1f;
                                    restarlineResprite.color = restarlineRecolor;

                                }
                                
                                
                            }
                        }

                    }

                }

                else //벽과 충돌 안 할 때
                {
                    if (ChType == 1) //길잡이 토끼에서 벽과 궤적이 충돌하지 않으면 반사경로를 투명하게 함
                    {
                        if ((StarLine[19].transform.position.x >= -2.5f) && (StarLine[19].transform.position.x <= 2.5f))
                        {
                            for (int i = 0; i < 15; i++)
                            {
                                //reStarLine[i].SetActive(false);
                            }
                        }

                        else
                        {
                            for (int i = 0; i < 15; i++)
                            {
                                //reStarLine[i].SetActive(true);
                            }
                        }
                    }

                    else //일반-반사 투명하게
                    {
                        if(NStarLine[11].transform.position.x >= -2.5f && NStarLine[11].transform.position.x <= 2.5f)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                restarlineResprite = reNStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                                restarlineRecolor = restarlineResprite.color;
                                restarlineRecolor.a = 0f;
                                restarlineResprite.color = restarlineRecolor;
                            }
                        }
                        
                        
                    }
                }

                

            }
            if (Input.GetMouseButtonUp(0))//터치 뗄때
            {
                //color.a = 0f; //터치 떼면 경로 안 보임
                //sprite.color = color;

                for (int i = 0; i < 20; i++) //길잡이-일반
                {
                    starlineResprite = StarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 라인
                    starlineRecolor = starlineResprite.color;
                    starlineRecolor.a = 0f;
                    starlineResprite.color = starlineRecolor;
                }

                for (int i = 0; i < 12; i++) //일반-일반
                {
                    starlineResprite = NStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 라인
                    starlineRecolor = starlineResprite.color;
                    starlineRecolor.a = 0f;
                    starlineResprite.color = starlineRecolor;
                }

                for (int i = 0; i < 15; i++) //길잡이-반사
                {
                    restarlineResprite = reStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                    restarlineRecolor = restarlineResprite.color;
                    restarlineRecolor.a = 0f;
                    restarlineResprite.color = restarlineRecolor;
                }

                for (int i = 0; i < 5; i++) //일반-반사
                {
                    restarlineResprite = reNStarLine[i].GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
                    restarlineRecolor = restarlineResprite.color;
                    restarlineRecolor.a = 0f;
                    restarlineResprite.color = restarlineRecolor;
                }

                if (-80 < degree && degree < 80 && possible == true && Manager.limit_cnt!=0 && starlinepossible == true) //회전각도 조정, 과정 끝날때까지 작동안하게, 구슬갯수제한 끝나면 작동안하게
                {
                    if (Character.ChType == 0)
                    {
                        characters[0].GetComponent<Animator>().SetTrigger("shooting");
                    }
                    else if (Character.ChType == 1)
                    {
                        characters[1].GetComponent<Animator>().SetTrigger("shooting");
                    }
                    else if (Character.ChType == 2)
                    {
                        characters[2].GetComponent<Animator>().SetTrigger("shooting");
                    }
                    else if (Character.ChType == 3)
                    {
                        characters[3].GetComponent<Animator>().SetTrigger("shooting");
                    }


                    sem.play(0);//구슬 쏠때 효과음 생성
                    GameObject.Find("GameObject").GetComponent<Manager>().bubblepop();//구슬 생성함수 Manager에서 불러오기
                    Shooter.possible = false;//연결되지 않은 게 떨어지기 전에 shooter 동작안하게
                    starlinepossible = false;
                    Manager.ing = true;
                    Manager.limit_cnt--; //제한 구슬 갯수 감소


                    /*
                    if (Character.ChType == 0)
                    {
                        characters[0].GetComponent<Animator>().SetBool("shooting", false);
                    }
                    else if (Character.ChType == 1)
                    {
                        characters[1].GetComponent<Animator>().SetBool("shooting", false);
                    }
                    else if (Character.ChType == 2)
                    {
                        characters[2].GetComponent<Animator>().SetBool("shooting", false);
                    }
                    else if (Character.ChType == 3)
                    {
                        characters[3].GetComponent<Animator>().SetBool("shooting", false);
                    }
                    */
                }
            }

            
#endif


        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "wall")
        {
            colcheck = true;
            ch_colcheck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "wall")
        {
            colcheck = false;
            ch_colcheck = false;
        }

    }

    void starInit() //길잡이 -일반 경로
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject _starline = Instantiate(starline) as GameObject;
            StarLine.Add(_starline);
        }
    }

    void NstarInit() //길잡이X - 일반 경로
    {
        for (int i = 0; i < 12; i++)
        {
            GameObject _starline = Instantiate(starline) as GameObject;
            NStarLine.Add(_starline);
        }
    }

    void restarInit()
    {
        for (int i = 0; i < 15; i++)
        {
            GameObject _restarline = Instantiate(starline) as GameObject;
            reStarLine.Add(_restarline);
        }
    }

    void reNstarInit()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject _restarline = Instantiate(starline) as GameObject;
            reNStarLine.Add(_restarline);
        }
    }


}
