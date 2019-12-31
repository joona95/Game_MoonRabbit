using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooter : MonoBehaviour
{
    static public bool possible = true; //대포 발사 가능한 시점과 불가능한 시점 구분 용도
    Vector3 touchPos;
    Vector3 colPos;

    float degree;
    float redegree;
    float yPos;
    float radian;

    bool colcheck = false;

    public GameObject reflectline;
    
    SpriteRenderer sprite; //대포(경로) 이미지
    SpriteRenderer resprite;
    Color color;
    Color recolor;

    public static float GetAngle(Vector3 from, Vector3 to)
    {
        Vector3 v = to - from;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    // Start is called before the first frame update
    void Start()
    {
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        color = sprite.color;
        color.a = 0f; //시작할 때 투명함
        sprite.color = color;

        resprite = reflectline.GetComponent<SpriteRenderer>(); //반사 라인
        recolor = resprite.color;
        recolor.a = 0f; //시작할 때 투명함
        resprite.color = recolor;
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

#if (UNITY_ANDROID || UNITY_IOS)
        if (Input.touchCount > 0)
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
                    GameObject.Find("GameObject").GetComponent<Manager>().bubblepop();
                    possible = false;//연결되지 않은 게 떨어지기 전에 shooter 동작안하게
                    Manager.limit_cnt--; //제한 구슬 갯수 감소
                }
            }
        }
#else
            if (Input.GetMouseButton(0))
            {
                color.a = 1f; //터치하고 있으면(누르고 있으면) 경로 보임
                sprite.color = color;
                touchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                degree = GetAngle(this.gameObject.transform.position, touchPos) - 90;
                if (-80 < degree && degree < 80)
                    this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, degree);

                colPos = this.gameObject.GetComponent<Transform>().position; //충돌 위치
                radian = degree * Mathf.PI / 180; // 충돌 각
                yPos = 2.5f / Mathf.Tan(radian);
                if (colcheck) //경로와 벽이 충돌중인지 확인, 충돌중이라면 반사 경로 보여줌
                {
                    if(80 > degree && degree > 0)
                    {
                        reflectline.transform.position = new Vector3(-2.5f, -3.5f + yPos, 0);
                        reflectline.transform.rotation = Quaternion.Euler(0f, 0f, -degree);
                    }
                    else if(-80 < degree && degree < 0)
                    {
                        reflectline.transform.position = new Vector3(2.5f, -3.5f - yPos, 0);
                        reflectline.transform.rotation = Quaternion.Euler(0f, 0f, -degree);
                    }
                    recolor.a = 1f;
                    resprite.color = recolor;
                }
                if (!colcheck)
                {
                    recolor.a = 0f;
                    resprite.color = recolor;
                }
            }
            if (Input.GetMouseButtonUp(0))//터치 뗄때
            {
                recolor.a = 0f;
                resprite.color = recolor;
                color.a = 0f; //터치 떼면 경로 안 보임
                sprite.color = color;
                if (-80 < degree && degree < 80 && possible == true && Manager.limit_cnt!=0) //회전각도 조정, 과정 끝날때까지 작동안하게, 구슬갯수제한 끝나면 작동안하게
                {
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
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "wall")
        {
            colcheck = false;
        }
    }

}
