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
    public GameObject shotspawn;//구슬 발사의 출발점을 정하기 위한 것
    public GameObject[] ballPrefabs;//발사할 구슬의 배열

    public int size;//발사할 구슬의 배열의 사이즈
    float purpl, re, blu, yello;//구슬 개수 비율

    int[] ballCnt = new int[6]; //구슬 개수 알려주는 배열. red,yellow,green,blue,purple,quest 순서
    float x, y; //구슬 생성 위한 좌표
    static int redCnt=0, yelCnt=0, greCnt=0, bluCnt=0, purCnt=0, queCnt=0; //구슬 개수 카운트 변수
    int total = 0;//전체 구슬 개수

    // Start is called before the first frame update
    void Start()
    {
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
            ballPrefabs[0] = (GameObject)Instantiate(purple, new Vector3(-2f, -2f, 0f), Quaternion.identity); purCnt++;
            purCnt++;
        }// (0~ 20퍼) 보라색
        else if ((a >= purpl) && (a < re))
        {
            ballPrefabs[0] = (GameObject)Instantiate(red, new Vector3(-2f, -2f, 0f), Quaternion.identity); purCnt++;
            redCnt++;
        }//(20~40퍼) 레드
        else if ((a >= re) && (a < blu))
        {
            ballPrefabs[0] = (GameObject)Instantiate(blue, new Vector3(-2f, -2f, 0f), Quaternion.identity); purCnt++;
            bluCnt++;
        }//(40~60퍼) 블루
        else if ((a >= blu) && (a < yello))
        {
            ballPrefabs[0] = (GameObject)Instantiate(yellow, new Vector3(-2f, -2f, 0f), Quaternion.identity); purCnt++;
            yelCnt++;
        }//(60~80퍼) 노랑
        else if ((a >= yello) && (a <= 100.0))
        {
            ballPrefabs[0] = (GameObject)Instantiate(green, new Vector3(-2f, -2f, 0f), Quaternion.identity); purCnt++;
            greCnt++;
        }//(80퍼 이상 초록)
   

        //두번째 발사할 구슬
        a = Random.Range(0.0f, 100.0f);
        
        if ((a >= 0.0f) && (a < purpl))
        {
            ballPrefabs[1] = (GameObject)Instantiate(purple, new Vector3(-2f, -4f, 0f), Quaternion.identity); purCnt++;
            purCnt++;
        }// (0~ 20퍼) 보라색
        else if ((a >= purpl) && (a < re))
        {
            ballPrefabs[1] = (GameObject)Instantiate(red, new Vector3(-2f, -4f, 0f), Quaternion.identity); purCnt++;
            redCnt++;
        }//(20~40퍼) 레드
        else if ((a >= re) && (a < blu))
        {
            ballPrefabs[1] = (GameObject)Instantiate(blue, new Vector3(-2f, -4f, 0f), Quaternion.identity); purCnt++;
            bluCnt++;
        }//(40~60퍼) 블루
        else if ((a >= blu) && (a < yello))
        {
            ballPrefabs[1] = (GameObject)Instantiate(yellow, new Vector3(-2f, -4f, 0f), Quaternion.identity); purCnt++;
            yelCnt++;
        }//(60~80퍼) 노랑
        else if ((a >= yello) && (a <= 100.0))
        {
            ballPrefabs[1] = (GameObject)Instantiate(green, new Vector3(-2f, -4f, 0f), Quaternion.identity); purCnt++;
            greCnt++;
        }//(80퍼 이상 초록)
 
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
    void Update()//구슬 색깔별로 개수 비율 맞춰놓은 겁니다.(발사 구슬 랜덤) 구슬 색깔 비율 변수를 여기 저기 넣어봤는데 update에 넣어둬야 제대로 되더라고요
    {
        total = redCnt + bluCnt + yelCnt + purCnt + greCnt;
        purpl = ((float)purCnt / total) * 100f;
        re = purpl + (((float)redCnt / total) * 100f);
        blu = re + (((float)bluCnt / total) * 100f);
        yello = blu + (((float)yelCnt / total) * 100f);
    }

    public void bubblepop()//구슬 생성 함수
    {
        //발사할 첫번째 구슬 위치를 대포 위치로 변경
        ballPrefabs[0].transform.position = shotspawn.transform.position;
        ballPrefabs[0].transform.rotation = shotspawn.transform.rotation;

        //두번째 발사 구슬을 첫번째 발사 구슬로 바꾸고 첫번째 구슬 위치로 변경
        ballPrefabs[0] = ballPrefabs[1];
        ballPrefabs[0].transform.position = new Vector3(-2f, -2f, 0f);
        

     //구슬 배열의 마지막 순서의 색깔을 정해줍니다.

        float a;
        a = Random.Range(0.0f, 100.0f);
        
        if ((a >= 0.0f) && (a < purpl))
        {
            ballPrefabs[1] = (GameObject)Instantiate(purple, new Vector3(-2f, -4f, 0f), Quaternion.identity);
            purCnt++;
        }// (0~ 20퍼) 보라색
        else if ((a >= purpl) && (a < re))
        {
            ballPrefabs[1] = (GameObject)Instantiate(red, new Vector3(-2f, -4f, 0f), Quaternion.identity);
            redCnt++;
        }//(20~40퍼) 레드
        else if ((a >= re) && (a < blu))
        {
            ballPrefabs[1] = (GameObject)Instantiate(blue, new Vector3(-2f, -4f, 0f), Quaternion.identity);
            bluCnt++;
        }//(40~60퍼) 블루
        else if ((a >= blu) && (a < yello))
        {
            ballPrefabs[1] = (GameObject)Instantiate(yellow, new Vector3(-2f, -4f, 0f), Quaternion.identity);
            yelCnt++;
        }//(60~80퍼) 노랑
        else if ((a >= yello) && (a <= 100.0))
        {
            ballPrefabs[1] = (GameObject)Instantiate(green, new Vector3(-2f, -4f, 0f), Quaternion.identity);
            greCnt++;
        }

    }
}
