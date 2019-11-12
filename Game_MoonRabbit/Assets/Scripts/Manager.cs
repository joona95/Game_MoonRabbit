using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject red; //빨간 구슬
    public GameObject yellow; //노란 구슬
    public GameObject green; //초록 구슬
    public GameObject blue; //파란 구슬
    public GameObject purple; //보라 구슬
    public GameObject quest; //퀘스트 구슬

    int[] ballCnt = new int[6]; //구슬 개수 알려주는 배열. red,yellow,green,blue,purple,quest 순서
    float x, y; //구슬 생성 위한 좌표
    static int redCnt=0, yelCnt=0, greCnt=0, bluCnt=0, purCnt=0, queCnt=0; //구슬 개수 카운트 변수

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        x = -2.34f;
        y =4.0f; //구슬 초기 생성 좌표 설정
        for(int i=0; i < 10; i++)
        {
            Instantiate(red, new Vector3(x, y, 0), Quaternion.Euler(0f, 0f, 0f));
            x += 0.52f;
            redCnt++; //구슬 개수 카운트
        } //위에서 두번째 줄 빨간 구슬 생성

        x = -2.08f;
        y = 4.45f; //구슬 초기 생성 좌표 설정
        for (int i = 0; i < 9; i++)
        {
            Instantiate(red, new Vector3(x, y, 0), Quaternion.Euler(0f, 0f, 0f));
            x += 0.52f;
            redCnt++; //구슬 개수 카운트
        } //위에서 첫번째 줄 빨간 구슬 생성

        Instantiate(red, new Vector3(0.52f, 3.55f, 0), Quaternion.Euler(0f, 0f, 0f));
        redCnt++;
        Instantiate(red, new Vector3(-0.52f, 3.55f, 0), Quaternion.Euler(0f, 0f, 0f));
        redCnt++;
        Instantiate(red, new Vector3(0.26f, 3.10f, 0), Quaternion.Euler(0f, 0f, 0f));
        redCnt++;
        Instantiate(red, new Vector3(-0.26f, 3.10f, 0), Quaternion.Euler(0f, 0f, 0f));
        redCnt++; //빨간 구슬 나머지 생성

        x = -2.08f;
        y = 3.55f;
        for (int i = 0; i < 3; i++)
        {
            Instantiate(blue, new Vector3(x, y, 0), Quaternion.Euler(0f, 0f, 0f));
            bluCnt++;
            Instantiate(blue, new Vector3(-x, y, 0), Quaternion.Euler(0f, 0f, 0f));
            x += 0.52f;
            bluCnt++; //구슬 개수 카운트
        } //위에서 세번째 줄 생성

        x = -2.34f;
        y = 3.10f;
        for (int i = 0; i < 4; i++)
        {
            Instantiate(blue, new Vector3(x, y, 0), Quaternion.Euler(0f, 0f, 0f));
            bluCnt++;
            Instantiate(blue, new Vector3(-x, y, 0), Quaternion.Euler(0f, 0f, 0f));
            x += 0.52f;
            bluCnt++; //구슬 개수 카운트
        } //위에서 네번째 줄 생성

        Instantiate(blue, new Vector3(0f, 2.65f, 0), Quaternion.Euler(0f, 0f, 0f));
        bluCnt++;
        Instantiate(blue, new Vector3(0.52f, 2.65f, 0), Quaternion.Euler(0f, 0f, 0f));
        bluCnt++;
        Instantiate(blue, new Vector3(-0.52f, 2.65f, 0), Quaternion.Euler(0f, 0f, 0f));
        bluCnt++; //파란 구슬 나머지 생성

        x = -2.08f;
        y = 2.65f;
        for (int i = 0; i < 3; i++)
        {
            Instantiate(green, new Vector3(x, y, 0), Quaternion.Euler(0f, 0f, 0f));
            x += 0.52f;
            greCnt++; //구슬 개수 카운트
        } //위에서 다섯번째 줄 생성

        x = -2.08f;
        y = 1.75f;
        for (int i = 0; i < 4; i++)
        {
            Instantiate(green, new Vector3(x, y, 0), Quaternion.Euler(0f, 0f, 0f));
            x += 0.52f;
            greCnt++; //구슬 개수 카운트
        } //위에서 일곱번째 줄 생성

        x = -2.34f;
        y = 2.20f;
        for (int i = 0; i < 5; i++)
        {
            Instantiate(green, new Vector3(x, y, 0), Quaternion.Euler(0f, 0f, 0f));
            x += 0.52f;
            greCnt++; //구슬 개수 카운트
        } //위에서 여섯번째 줄 생성

        x = 2.08f;
        y = 2.65f;
        for (int i = 0; i < 3; i++)
        {
            Instantiate(yellow, new Vector3(x, y, 0), Quaternion.Euler(0f, 0f, 0f));
            x -= 0.52f;
            yelCnt++; //구슬 개수 카운트
        } //위에서 다섯번째 줄 생성

        x = 2.08f;
        y = 1.75f;
        for (int i = 0; i < 4; i++)
        {
            Instantiate(yellow, new Vector3(x, y, 0), Quaternion.Euler(0f, 0f, 0f));
            x -= 0.52f;
            yelCnt++; //구슬 개수 카운트
        } //위에서 일곱번째 줄 생성

        x = 2.34f;
        y = 2.2f;
        for (int i = 0; i < 5; i++)
        {
            Instantiate(yellow, new Vector3(x, y, 0), Quaternion.Euler(0f, 0f, 0f));
            x -= 0.52f;
            yelCnt++; //구슬 개수 카운트
        } //위에서 여섯번째 줄 생성

        x = 2.34f;
        y = 1.30f;
        for (int i = 0; i < 10; i++)
        {
            Instantiate(purple, new Vector3(x, y, 0), Quaternion.Euler(0f, 0f, 0f));
            x -= 0.52f;
            purCnt++; //구슬 개수 카운트
        } //여덟번째 줄 생성

        x = 2.08f;
        y = 0.85f;
        for (int i = 0; i < 9; i++)
        {
            Instantiate(purple, new Vector3(x, y, 0), Quaternion.Euler(0f, 0f, 0f));
            x -= 0.52f;
            purCnt++; //구슬 개수 카운트
        } //아홉째 줄 생성

        Instantiate(purple, new Vector3(0f, 1.75f, 0), Quaternion.Euler(0f, 0f, 0f));
        purCnt++;

        Instantiate(quest, new Vector3(0f, 3.55f, 0), Quaternion.Euler(0f, 0f, 0f));
        queCnt++;

        ballCnt[0] = redCnt;
        ballCnt[1] = yelCnt;
        ballCnt[2] = greCnt;
        ballCnt[3] = bluCnt;
        ballCnt[4] = purCnt;
        ballCnt[5] = queCnt; //배열에 구슬 카운트 한 것 입력
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
