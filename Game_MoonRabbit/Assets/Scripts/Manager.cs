using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static public List<GameObject[]> Map; //관리할 맵
    static public int total_row, total_col; //맵 전체 행, 열
    static public List<Dictionary<string, object>> StageInfo;
    static public int current_stage;//현재 스테이지 레벨
    static public int[,] stage;

    public GameObject[] BallType=new GameObject[10];//구슬 색깔별로 종류 저장
    //0,1,2,3,4: 빨,노,초,파,보   5,6,7,8,9: 퀘스트빨,노,초,파,보

    public GameObject shotspawn;//구슬 발사의 출발점을 정하기 위한 것
    public GameObject[] ballPrefabs;//발사할 구슬의 배열

    public int size;//발사할 구슬의 배열의 사이즈
    float purpl, re, blu, yello;//구슬 개수 비율

    static public int limit_cnt = 30; //구슬 제한 갯수
    static public int redCnt=0, yelCnt=0, greCnt=0, bluCnt=0, purCnt=0, queCnt=0; //구슬 개수 카운트 변수
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
            ballPrefabs[0] = (GameObject)Instantiate(BallType[4], new Vector3(0f, -4.5f, 0f), Quaternion.identity);
            purCnt++;
        }// (0~ 20퍼) 보라색
        else if ((a >= purpl) && (a < re))
        {
            ballPrefabs[0] = (GameObject)Instantiate(BallType[0], new Vector3(0f, -4.5f, 0f), Quaternion.identity); 
            redCnt++;
        }//(20~40퍼) 레드
        else if ((a >= re) && (a < blu))
        {
            ballPrefabs[0] = (GameObject)Instantiate(BallType[3], new Vector3(0f, -4.5f, 0f), Quaternion.identity); 
            bluCnt++;
        }//(40~60퍼) 블루
        else if ((a >= blu) && (a < yello))
        {
            ballPrefabs[0] = (GameObject)Instantiate(BallType[1], new Vector3(0f, -4.5f, 0f), Quaternion.identity);
            yelCnt++;
        }//(60~80퍼) 노랑
        else if ((a >= yello) && (a <= 100.0))
        {
            ballPrefabs[0] = (GameObject)Instantiate(BallType[2], new Vector3(0f, -4.5f, 0f), Quaternion.identity);
            greCnt++;
        }//(80퍼 이상 초록)
   

        //두번째 발사할 구슬
        a = Random.Range(0.0f, 100.0f);
        
        if ((a >= 0.0f) && (a < purpl))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[4], new Vector3(-1.5f, -4.5f, 0f), Quaternion.identity); 
            purCnt++;
        }// (0~ 20퍼) 보라색
        else if ((a >= purpl) && (a < re))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[0], new Vector3(-1.5f, -4.5f, 0f), Quaternion.identity); 
            redCnt++;
        }//(20~40퍼) 레드
        else if ((a >= re) && (a < blu))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[3], new Vector3(-1.5f, -4.5f, 0f), Quaternion.identity); 
            bluCnt++;
        }//(40~60퍼) 블루
        else if ((a >= blu) && (a < yello))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[1], new Vector3(-1.5f, -4.5f, 0f), Quaternion.identity); 
            yelCnt++;
        }//(60~80퍼) 노랑
        else if ((a >= yello) && (a <= 100.0))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[2], new Vector3(-1.5f, -4.5f, 0f), Quaternion.identity); 
            greCnt++;
        }//(80퍼 이상 초록)
 
    }

    void Awake()
    {
        /*
        //맵생성
        total_row = 9; //맵의 주어진 전체 행 설정
        total_col = 10; //맵의 주어진 전체 열 설정
        //스테이지 구슬 배치
        int[,] stage = new int[,] { { 0,0,0,0,0,0,0,0,0,-1 },
                                    { 0,0,0,0,0,0,0,0,0,0},
                                    { 3,3,3,0,5,0,3,3,3,-1},
                                    { 3,3,3,3,0,0,3,3,3,3},
                                    { 2,2,2,3,3,3,1,1,1,-1},
                                    { 2,2,2,2,2,1,1,1,1,1},
                                    { 2,2,2,2,4,1,1,1,1,-1},
                                    { 4,4,4,4,4,4,4,4,4,4},
                                    { 4,4,4,4,4,4,4,4,4,-1} };
        */
        StageInfo = MapLoader.StageRead("StageInfo");
        total_row = int.Parse(StageInfo[current_stage]["Row"].ToString());
        total_col = int.Parse(StageInfo[current_stage]["Col"].ToString());
        stage = new int[total_row, total_col];
        MapLoader.MapRead(current_stage.ToString());
        Map= new List<GameObject[]>();

        float x, y=0.85f; //구슬 생성 위치 지정 변수.  제일 밑에서부터 쌓아올라가기
        int t; //행 구분을 위한 변수
        Stack<GameObject[]> temp = new Stack<GameObject[]>();//List Map에 거꾸로 넣어주기
        for(int i = total_row-1; i >=0; i--)
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
                tmp[j]=Instantiate(BallType[stage[i, j]], new Vector3(x, y, 0f), Quaternion.Euler(0f, 0f, 0f)); //구슬 생성
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
                    case 6:
                    case 7:
                    case 8:
                    case 9:                 
                        queCnt++;
                        tmp[j].GetComponent<Ball>().quest = true; //퀘스트 구슬인 경우 각 구슬마다 표시
                        break;
  
                }

                x -= 0.52f;
            }
            temp.Push(tmp); //리스트에 구슬 한 줄의 배열 넣기
            

            y += 0.45f;
        }

        //Map에 GameObject[] 처음 행부터 모든 행 전부 넣어주기
        while (temp.Count != 0)
        {
            Map.Add(temp.Pop());
        }

        


    }


    // Update is called once per frame
    void Update()//구슬 색깔별로 개수 비율 맞춰놓은 겁니다.(발사 구슬 랜덤) 구슬 색깔 비율 변수를 여기 저기 넣어봤는데 update에 넣어둬야 제대로 되더라고요
    {

        total = redCnt + bluCnt + yelCnt + purCnt + greCnt;
        purpl = ((float)purCnt / total) * 100f;
        re = purpl + (((float)redCnt / total) * 100f);
        blu = re + (((float)bluCnt / total) * 100f);
        yello = blu + (((float)yelCnt / total) * 100f);


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
            Time.timeScale = 0f;
        }
        else
        {

            //가장 낮은 위치에 있는 구슬 y값 구하기
            float min_y = 0.85f;
            int map_index = 0;
            map_index = Map[total_row - 1].Length - 1;
            
            for (int i = map_index; i >= 0 ; i--)
            {

                Debug.Log(i);
                if (Map[total_row - 1][i] != null)
                {
                    min_y = Mathf.Round(Map[total_row - 1][i].transform.position.y * 100) / 100;
                    break;
                }
            }


            if (Ball.discon_total == Ball.discon_cnt) //연결되지 않은 구슬들 다 떨어지면 동작
            {
                //가장 높은 위치에 있는 구슬 y 값 구하기
                float max_y = 4.45f;
                for (int i = Map[0].Length - 1; i >= 0; i--)
                {
                    if (Map[0][i] != null)
                    {
                        max_y = Mathf.Round(Map[0][i].transform.position.y * 100) / 100;
                        break;
                    }
                }


                if (min_y < 0.85f)
                {
                    float[,] end_y = new float[total_row, total_col];
                    for (int i = 0; i < total_row; i++)
                    {
                        for (int j = 0; j < Map[i].Length; j++)
                        {
                            if (Map[i][j] != null)
                                end_y[i, j] = Map[i][j].transform.position.y + 0.45f;
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
                                    Map[i][j].transform.position = new Vector3(Map[i][j].transform.position.x, Map[i][j].transform.position.y + 0.05f, Map[i][j].transform.position.z);
                                }
                            }
                        }
                    }


                }
                else if (min_y > 0.85f && max_y > 4.45f)
                {
                    float[,] end_y = new float[total_row, total_col];
                    for (int i = 0; i < total_row; i++)
                    {
                        for (int j = 0; j < Map[i].Length; j++)
                        {
                            if (Map[i][j] != null)
                                end_y[i, j] = Map[i][j].transform.position.y - (max_y - 4.45f);
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
                                    Map[i][j].transform.position = new Vector3(Map[i][j].transform.position.x, Map[i][j].transform.position.y - 0.05f, Map[i][j].transform.position.z);

                                }
                            }
                        }
                    }


                }

            }
        }

    }

    public void bubblepop()//구슬 생성 함수
    {
        //발사할 첫번째 구슬 위치를 대포 위치로 변경
        ballPrefabs[0].transform.position = shotspawn.transform.position;
        ballPrefabs[0].transform.rotation = shotspawn.transform.rotation;

        //두번째 발사 구슬을 첫번째 발사 구슬로 바꾸고 첫번째 구슬 위치로 변경
        ballPrefabs[0] = ballPrefabs[1];
        ballPrefabs[0].transform.position = new Vector3(0f, -4.5f, 0f);
        

     //구슬 배열의 마지막 순서의 색깔을 정해줍니다.

        float a;
        a = Random.Range(0.0f, 100.0f);
        
        if ((a >= 0.0f) && (a < purpl))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[4], new Vector3(-1.5f, -4.5f, 0f), Quaternion.identity);
            purCnt++;
        }// (0~ 20퍼) 보라색
        else if ((a >= purpl) && (a < re))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[0], new Vector3(-1.5f, -4.5f, 0f), Quaternion.identity);
            redCnt++;
        }//(20~40퍼) 레드
        else if ((a >= re) && (a < blu))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[3], new Vector3(-1.5f, -4.5f, 0f), Quaternion.identity);
            bluCnt++;
        }//(40~60퍼) 블루
        else if ((a >= blu) && (a < yello))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[1], new Vector3(-1.5f, -4.5f, 0f), Quaternion.identity);
            yelCnt++;
        }//(60~80퍼) 노랑
        else if ((a >= yello) && (a <= 100.0))
        {
            ballPrefabs[1] = (GameObject)Instantiate(BallType[2], new Vector3(-1.5f, -4.5f, 0f), Quaternion.identity);
            greCnt++;
        }
        
    }

    
}
