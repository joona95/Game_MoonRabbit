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

    public GameObject reflectline;
    public GameObject ch_reflectline; //길잡이 캐릭터의 경로(ch_로 시작하는 모든 변수)
    public GameObject starline; //길잡이 캐릭터 경로 중 일부

    SpriteRenderer sprite; //대포(경로) 이미지
    SpriteRenderer resprite;
    public Color color;
    public Color recolor;
    
    SpriteRenderer ch_resprite;
    public Color ch_recolor;

    public int jdg;
    int ChType = Character.ChType;
    Object starobj;

    public static float GetAngle(Vector3 from, Vector3 to)
    {
        Vector3 v = to - from;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    // Start is called before the first frame update
    void Start()
    {
        possible = true;
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        color = sprite.color;
        color.a = 0f; //시작할 때 투명함
        sprite.color = color;

        resprite = reflectline.GetComponent<SpriteRenderer>(); //반사 라인
        recolor = resprite.color;
        recolor.a = 0f; //시작할 때 투명함
        resprite.color = recolor;

        

        ch_resprite = ch_reflectline.GetComponent<SpriteRenderer>(); //길잡이 캐릭터 반사 라인
        ch_recolor = ch_resprite.color;
        ch_recolor.a = 0f; //시작할 때 투명함
        ch_resprite.color = ch_recolor;

        bgm = FindObjectOfType<bgmmanager>();
        bgm.stop();
        bgm.play(1);
        sem = FindObjectOfType<semmanager>();
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Manager.queCnt == 0)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
#if (UNITY_ANDROID || UNITY_IOS)
        if ((Input.touchCount > 0)&&(GameObject.Find("Optionbutton").GetComponent<optionbuttontouch>().isPressed == false))
        {
            //화면 touch 처음 하나만 인식
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began) //화면을 touch한 순간
            {
                touchPos = touch.position;
                degree = GetAngle(this.gameObject.transform.position, touchPos)-90;
                if(-80<degree&&degree<80) 
                    this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, degree);
            }
            if(touch.phase == TouchPhase.Moved) //손가락이 화면 위에서 터치한 상태로 이동
            {
                touchPos = touch.position;
                degree = GetAngle(this.gameObject.transform.position, touchPos)-90;
                if(-80<degree&&degree<80)        
                    this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, degree);
            }
            if(touch.phase == TouchPhase.Ended) //손가락이 화면에서 떨어지면 touch가 끝난 경우
            {
                if (-80 < degree && degree < 80 && possible==true && Manager.limit_cnt!=0){ //회전각도 조정, 과정 끝날때까지 작동안하게, 구슬갯수제한 끝나면 작동안하게
                    sem.play(0);
                    GameObject.Find("GameObject").GetComponent<Manager>().bubblepop();
                    possible = false;//연결되지 않은 게 떨어지기 전에 shooter 동작안하게
                    Manager.limit_cnt--; //제한 구슬 갯수 감소
                }
            }
        }
#else
            if ((Input.GetMouseButton(0))&&(GameObject.Find("Optionbutton").GetComponent<optionbuttontouch>().isPressed == false)&& (GameObject.Find("ChangeBall").GetComponent<ChangeBall>().isPressed == false))
            {//설정 버튼이 눌리지 않을때 대포의 궤적을 조절하고 발사할 수 있음
                color.a = 1f; //터치하고 있으면(누르고 있으면) 경로 보임
                sprite.color = color;
                touchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                degree = GetAngle(this.gameObject.transform.position, touchPos) - 90;
                if (-80 < degree && degree < 80)
                    this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, degree);

                colPos = this.gameObject.GetComponent<Transform>().position; //충돌 위치
                radian = degree * Mathf.PI / 180; // 충돌 각
                yPos = 2.3f / Mathf.Tan(radian);

                float stard = 0f;
                while (!ch_colcheck && stard<10f)
                {
                    starobj = Instantiate(starline, new Vector3(stard * -Mathf.Sin(radian), -3f + stard * Mathf.Cos(radian), 0), Quaternion.Euler(Mathf.Sin(radian), Mathf.Cos(radian), 0f));
                    stard += 0.4f;
                }
                


                if (colcheck) //경로와 벽이 충돌중인지 확인, 충돌중이라면 반사 경로 보여줌
                {
                    if(80 > degree && degree > 0)
                    {
                        if(ChType == 1)
                        {
                            ch_reflectline.transform.position = new Vector3(-2.3f, -3f + yPos, 0);
                            ch_reflectline.transform.rotation = Quaternion.Euler(0f, 0f, -degree);
                        }

                        else
                        {
                            reflectline.transform.position = new Vector3(-2.3f, -3f + yPos, 0);
                            reflectline.transform.rotation = Quaternion.Euler(0f, 0f, -degree);
                        }

                    }
                    else if(-80 < degree && degree < 0)
                    {
                        if (ChType == 1)
                        {
                            ch_reflectline.transform.position = new Vector3(2.3f, -3f - yPos, 0);
                            ch_reflectline.transform.rotation = Quaternion.Euler(0f, 0f, -degree);
                        }

                        else{
                            reflectline.transform.position = new Vector3(2.3f, -3f - yPos, 0);
                            reflectline.transform.rotation = Quaternion.Euler(0f, 0f, -degree);
                        }
                        
                    }

                    if (ChType == 1)
                    {
                        ch_recolor.a = 1f;
                        ch_resprite.color = ch_recolor;
                    }

                    else
                    {
                        recolor.a = 1f;
                        resprite.color = recolor;
                    }
                        
                }
                if (!colcheck)
                {
                    recolor.a = 0f;
                    resprite.color = recolor;
                }
                if (!ch_colcheck)
                {
                    ch_recolor.a = 0f;
                    ch_resprite.color = ch_recolor;
                }
            }
            if (Input.GetMouseButtonUp(0))//터치 뗄때
            {
                recolor.a = 0f;
                resprite.color = recolor;
                color.a = 0f; //터치 떼면 경로 안 보임
                sprite.color = color;
                ch_recolor.a = 0f;
                ch_resprite.color = ch_recolor;

                Destroy(starobj);

                if (-80 < degree && degree < 80 && possible == true && Manager.limit_cnt!=0) //회전각도 조정, 과정 끝날때까지 작동안하게, 구슬갯수제한 끝나면 작동안하게
                {
                    sem.play(0);//구슬 쏠때 효과음 생성
                    GameObject.Find("GameObject").GetComponent<Manager>().bubblepop();//구슬 생성함수 Manager에서 불러오기
                    Shooter.possible = false;//연결되지 않은 게 떨어지기 전에 shooter 동작안하게
                    Manager.limit_cnt--; //제한 구슬 갯수 감소
                }
            }
#endif
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "wall")
        {
            colcheck = true;
            ch_colcheck = true;
        }

        if(col.tag == "red")
        {
            ch_ballcheck = true;
        }

        if (col.tag == "yellow")
        {
            ch_ballcheck = true;
        }

        if (col.tag == "green")
        {
            ch_ballcheck = true;
        }

        if (col.tag == "blue")
        {
            ch_ballcheck = true;
        }

        if (col.tag == "purple")
        {
            ch_ballcheck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "wall")
        {
            colcheck = false;
            ch_colcheck = false;
        }

        if (col.tag == "red")
        {
            ch_ballcheck = false;
        }

        if (col.tag == "yellow")
        {
            ch_ballcheck = false;
        }

        if (col.tag == "green")
        {
            ch_ballcheck = false;
        }

        if (col.tag == "blue")
        {
            ch_ballcheck = false;
        }

        if (col.tag == "purple")
        {
            ch_ballcheck = false;
        }
    }

}
