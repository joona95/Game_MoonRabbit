using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int type; //해당 구슬이 9개 행인지 10개 행인지 알려주는 변수  0:10개, 1:9개
    public int row, col; //해당 구슬의 맵 상 위치 행, 열
    public string color; //shootball tag 바궈주기 위한 변수
    public bool visit = false; //3개이상 연속인지 여부 판별할 때 이용
    public bool connect = true; //연결여부 판단
    static public int discon_cnt = 0, discon_total = 0; //연결끊긴 구슬의 갯수
    public bool quest; //퀘스트 구슬인지 여부
    public bool shootball = false; //발사하는 공인지 여부
    semmanager sem;

    // Start is called before the first frame update
    void Start()
    {
        sem = FindObjectOfType<semmanager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (this.gameObject.transform.position == GameObject.Find("Shotspawn").transform.position)//발사할 공에 한정하여 발사가 되게끔 하는 함수
        {
            shootball = true; //shootball 발사할 공 여부 변경
            GetComponent<Rigidbody2D>().velocity = transform.right * 5f;//발사
        }

        //연결되지 않은 경우 아래로 떨어지고 일정 위치에서 destroy
        if (connect == false)
        {
            
            this.gameObject.transform.Translate(0, -0.2f, 0, Space.World);
            if (this.gameObject.transform.position.y <= -3f)
            {
                if (this.gameObject.tag == "red")
                    Manager.redCnt--;
                else if (this.gameObject.tag == "yellow")
                    Manager.yelCnt--;
                else if (this.gameObject.tag == "green")
                    Manager.greCnt--;
                else if (this.gameObject.tag == "blue")
                    Manager.bluCnt--;
                else if (this.gameObject.tag == "purple")
                    Manager.purCnt--;

                if (quest == true) //퀘스트구슬이면 퀘스트구슬갯수 감소
                {
                    Manager.queCnt--;
                }

                Destroy(this.gameObject);
                discon_cnt++;

                //연결끊긴것들 다 떨어지면 shooter 다시 동작하게
                if(discon_cnt==discon_total)
                    Shooter.possible = true;
            }
        }

        

        
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (shootball==true && collision.gameObject.tag!="wall" && collision.gameObject.tag!="line"&&collision.gameObject.tag!="ceil") //shootball이 벽이 아닌 공에 닿았을 때
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero; //발사되는 공이 벽이 아닌 다른 공과 닿았을 때 멈춤
            sem.play(2);
            float col_x = collision.gameObject.transform.position.x; //collision의 x 좌표
            float col_y = collision.gameObject.transform.position.y; //collision의 y 좌표
            float this_x = this.gameObject.transform.position.x; //shootball의 x 좌표
            float this_y = this.gameObject.transform.position.y; //shootball의 y 좌표

            if (collision.gameObject.GetComponent<Ball>().type == 0) //0:10개 행
            {
                //collision의 (x, y)좌표와 shootball의 (x,y)좌표 비교했을 때 row, col, position 정해주기 (위왼, 위오, 왼, 오, 아래왼, 아래오)
                if (col_x < this_x) 
                {
                    if (col_y + 0.225 < this_y) //위쪽 오른쪽
                    {
                        this.gameObject.GetComponent<Ball>().row = collision.gameObject.GetComponent<Ball>().row - 1;
                        this.gameObject.GetComponent<Ball>().col = collision.gameObject.GetComponent<Ball>().col;
                        this.gameObject.transform.position = new Vector3(col_x + 0.26f, col_y + 0.45f, 0f);
 
                    }
                    else if(col_y - 0.225 > this_y) //아래쪽 오른쪽 
                    {
                        this.gameObject.GetComponent<Ball>().row = collision.gameObject.GetComponent<Ball>().row + 1;
                        this.gameObject.GetComponent<Ball>().col = collision.gameObject.GetComponent<Ball>().col;
                        this.gameObject.transform.position = new Vector3(col_x + 0.26f, col_y - 0.45f, 0f);
                 
                    }
                    else //col_y - 0.225 <= this_y <= col_y + 0.225  //오른쪽
                    {
                        this.gameObject.GetComponent<Ball>().row = collision.gameObject.GetComponent<Ball>().row;
                        this.gameObject.GetComponent<Ball>().col = collision.gameObject.GetComponent<Ball>().col + 1;
                        this.gameObject.transform.position = new Vector3(col_x + 0.52f, col_y, 0f);
                      
                    }
                }
                else //col_x >= this_x
                {
                    if (col_y + 0.225 < this_y) //위쪽 왼쪽
                    {
                        this.gameObject.GetComponent<Ball>().row = collision.gameObject.GetComponent<Ball>().row - 1;
                        this.gameObject.GetComponent<Ball>().col = collision.gameObject.GetComponent<Ball>().col - 1;
                        this.gameObject.transform.position = new Vector3(col_x - 0.26f, col_y + 0.45f, 0f);
              

                    }
                    else if (col_y - 0.225 > this_y) //아래쪽 왼쪽
                    {
                        this.gameObject.GetComponent<Ball>().row = collision.gameObject.GetComponent<Ball>().row + 1;
                        this.gameObject.GetComponent<Ball>().col = collision.gameObject.GetComponent<Ball>().col - 1;
                        this.gameObject.transform.position = new Vector3(col_x - 0.26f, col_y - 0.45f, 0f);
              

                    }
                    else //col_y - 0.225 <= this_y <= col_y + 0.225  //왼쪽
                    {
                        this.gameObject.GetComponent<Ball>().row = collision.gameObject.GetComponent<Ball>().row;
                        this.gameObject.GetComponent<Ball>().col = collision.gameObject.GetComponent<Ball>().col - 1;
                        this.gameObject.transform.position = new Vector3(col_x - 0.52f, col_y, 0f);
                 
                    }
                }

                

            }
            else //1:9개 행
            {
                if (col_x < this_x)
                {
                    if (col_y + 0.225 < this_y) //위쪽 오른쪽
                    {
                        this.gameObject.GetComponent<Ball>().row = collision.gameObject.GetComponent<Ball>().row - 1;
                        this.gameObject.GetComponent<Ball>().col = collision.gameObject.GetComponent<Ball>().col + 1;
                        this.gameObject.transform.position = new Vector3(col_x + 0.26f, col_y + 0.45f, 0f);
            
                    }
                    else if (col_y - 0.225 > this_y) //아래쪽 오른쪽
                    {
                        this.gameObject.GetComponent<Ball>().row = collision.gameObject.GetComponent<Ball>().row + 1;
                        this.gameObject.GetComponent<Ball>().col = collision.gameObject.GetComponent<Ball>().col + 1;
                        this.gameObject.transform.position = new Vector3(col_x + 0.26f, col_y - 0.45f, 0f);
               
                    }
                    else //col_y - 0.225 <= this_y <= col_y + 0.225  //오른쪽
                    {
                        this.gameObject.GetComponent<Ball>().row = collision.gameObject.GetComponent<Ball>().row;
                        this.gameObject.GetComponent<Ball>().col = collision.gameObject.GetComponent<Ball>().col + 1;
                        this.gameObject.transform.position = new Vector3(col_x + 0.52f, col_y, 0f);
                   
                    }
                }
                else //col_x >= this_x  
                {
                    if (col_y + 0.225 < this_y) //위쪽 왼쪽
                    {
                        this.gameObject.GetComponent<Ball>().row = collision.gameObject.GetComponent<Ball>().row - 1;
                        this.gameObject.GetComponent<Ball>().col = collision.gameObject.GetComponent<Ball>().col;
                        this.gameObject.transform.position = new Vector3(col_x - 0.26f, col_y + 0.45f, 0f);
        
                    }
                    else if (col_y - 0.225 > this_y) //아래쪽 왼쪽
                    {
                        this.gameObject.GetComponent<Ball>().row = collision.gameObject.GetComponent<Ball>().row + 1;
                        this.gameObject.GetComponent<Ball>().col = collision.gameObject.GetComponent<Ball>().col;
                        this.gameObject.transform.position = new Vector3(col_x - 0.26f, col_y - 0.45f, 0f);
    
                    }
                    else //col_y - 0.225 <= this_y <= col_y + 0.225  //왼쪽
                    {
                        this.gameObject.GetComponent<Ball>().row = collision.gameObject.GetComponent<Ball>().row;
                        this.gameObject.GetComponent<Ball>().col = collision.gameObject.GetComponent<Ball>().col - 1;
                        this.gameObject.transform.position = new Vector3(col_x - 0.52f, col_y, 0f);
          
                    }
                }

            }

           

            
            

            if (this.gameObject.GetComponent<Ball>().row >= Manager.total_row) //total_row보다 현재 row가 크면 Map에 새로운 배열을 넣어주어야함. total_row도 증가.
            {
                int t; //현재 row의 갯수
                if (Manager.Map[this.gameObject.GetComponent<Ball>().row - 1].Length == Manager.total_col) //현재 row의 이전 행의 배열 수가 10 -> 현재 9
                {
                    this.gameObject.GetComponent<Ball>().type = 1;
                    t = Manager.total_col - 1;
                }
                else //현재 row의 이전 행의 배열 수가 9 -> 현재 10
                {
                    this.gameObject.GetComponent<Ball>().type = 0;
                    t = Manager.total_col;
                }

                GameObject[] tmp = new GameObject[t];
                Manager.Map.Add(tmp);
                Manager.total_row++;
            }

            Manager.Map[this.gameObject.GetComponent<Ball>().row][this.gameObject.GetComponent<Ball>().col] = this.gameObject; //Map의 해당 row, col 위치에 shootball 저장
            this.gameObject.GetComponent<Ball>().type = Manager.total_col - Manager.Map[this.gameObject.GetComponent<Ball>().row].Length;

            //shootball 발사하는공 여부 변경
            shootball = false;

            //visit 초기화
            for (int i = 0; i < Manager.total_row; i++)
            {
                for(int j = 0; j < Manager.Map[i].Length; j++)
                {
                    if (Manager.Map[i][j] != null)
                        Manager.Map[i][j].GetComponent<Ball>().visit = false;
                }
            }

            //현재 구슬과 동일한 색상의 연속된 구슬이 3개 이상 있는지 판별
            Queue<GameObject> q = new Queue<GameObject>();
            q.Enqueue(this.gameObject);
            q.Peek().GetComponent<Ball>().visit = true;
            int count = 0; //같은 색상의 연결된 구슬 갯수
            int qcount = 0; //같은 색상의 연결된 구슬 중 퀘스트 구슬 갯수
            while (q.Count != 0)
            {
                GameObject obj = q.Dequeue();
                int r = obj.GetComponent<Ball>().row;
                int c = obj.GetComponent<Ball>().col;
               

                if (obj.GetComponent<Ball>().quest == true) //같은 색상의 연결된 구슬 중 퀘스트 구슬인 경우의 수
                {
                    qcount++;
                }

                count++;

                if (obj.GetComponent<Ball>().type == 1)//9개일 때 이웃한 6개
                {
                    if (0 <=r-1&&c<Manager.Map[r-1].Length && Manager.Map[r - 1][c] != null && Manager.Map[r - 1][c].GetComponent<Ball>().visit == false && Manager.Map[r-1][c].GetComponent<Ball>().tag == this.gameObject.tag)
                    {
                        q.Enqueue(Manager.Map[r - 1][c]);
                        Manager.Map[r - 1][c].GetComponent<Ball>().visit = true;
                    }
                    if (0<=r-1&&c+1<Manager.Map[r-1].Length && Manager.Map[r - 1][c+1] != null && Manager.Map[r - 1][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r-1][c+1].GetComponent<Ball>().tag == this.gameObject.tag)
                    {
                        q.Enqueue(Manager.Map[r - 1][c+1]);
                        Manager.Map[r - 1][c+1].GetComponent<Ball>().visit = true;
                    }
                    if (0<=c-1 && Manager.Map[r][c-1] != null && Manager.Map[r][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r][c-1].GetComponent<Ball>().tag == this.gameObject.tag)
                    {
                        q.Enqueue(Manager.Map[r][c-1]);
                        Manager.Map[r][c-1].GetComponent<Ball>().visit = true;
                    }
                    if (c+1<Manager.Map[r].Length && Manager.Map[r][c+1] != null && Manager.Map[r][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r][c+1].GetComponent<Ball>().tag == this.gameObject.tag)
                    {
                        q.Enqueue(Manager.Map[r][c+1]);
                        Manager.Map[r][c+1].GetComponent<Ball>().visit = true;
                    }
                    if (r+1<Manager.total_row&&c<Manager.Map[r+1].Length && Manager.Map[r +1][c] != null && Manager.Map[r + 1][c].GetComponent<Ball>().visit == false && Manager.Map[r+1][c].GetComponent<Ball>().tag == this.gameObject.tag)
                    {
                        q.Enqueue(Manager.Map[r + 1][c]);
                        Manager.Map[r + 1][c].GetComponent<Ball>().visit = true;
                    }
                    if (r+1<Manager.total_row&&c+1<Manager.Map[r+1].Length  && Manager.Map[r + 1][c + 1] != null && Manager.Map[r + 1][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r+1][c+1].GetComponent<Ball>().tag == this.gameObject.tag)
                    {
                        q.Enqueue(Manager.Map[r + 1][c + 1]);
                        Manager.Map[r + 1][c + 1].GetComponent<Ball>().visit = true;
                    }
                }
                else //10개일 때 이웃한 6개 
                {
                    if (0<=r-1&&0<=c-1 && Manager.Map[r - 1][c-1] != null && Manager.Map[r - 1][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r-1][c-1].GetComponent<Ball>().tag == this.gameObject.tag)
                    {
                        q.Enqueue(Manager.Map[r - 1][c-1]);
                        Manager.Map[r - 1][c-1].GetComponent<Ball>().visit = true;
                    }
                    if (0<=r-1 &&c<Manager.Map[r-1].Length&& Manager.Map[r - 1][c] != null && Manager.Map[r - 1][c].GetComponent<Ball>().visit == false && Manager.Map[r-1][c].GetComponent<Ball>().tag == this.gameObject.tag)
                    {
                        q.Enqueue(Manager.Map[r - 1][c]);
                        Manager.Map[r - 1][c].GetComponent<Ball>().visit = true;
                    }
                    if (0<=c-1  && Manager.Map[r][c-1] != null && Manager.Map[r][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r][c-1].GetComponent<Ball>().tag == this.gameObject.tag)
                    {
                        q.Enqueue(Manager.Map[r][c-1]);
                        Manager.Map[r][c-1].GetComponent<Ball>().visit = true;
                    }
                    if (c+1<Manager.Map[r].Length && Manager.Map[r][c+1] != null && Manager.Map[r][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r][c+1].GetComponent<Ball>().tag == this.gameObject.tag)
                    {
                        q.Enqueue(Manager.Map[r][c+1]);
                        Manager.Map[r][c+1].GetComponent<Ball>().visit = true;
                    }
                    if (r+1<Manager.total_row&&0<=c-1&& Manager.Map[r + 1][c-1] != null && Manager.Map[r + 1][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r+1][c-1].GetComponent<Ball>().tag == this.gameObject.tag)
                    {
                        q.Enqueue(Manager.Map[r + 1][c-1]);
                        Manager.Map[r + 1][c-1].GetComponent<Ball>().visit = true;
                    }
                    if (r+1<Manager.total_row&&c<Manager.Map[r+1].Length && Manager.Map[r + 1][c] != null && Manager.Map[r + 1][c].GetComponent<Ball>().visit == false && Manager.Map[r+1][c].GetComponent<Ball>().tag == this.gameObject.tag)
                    {
                        q.Enqueue(Manager.Map[r + 1][c]);
                        Manager.Map[r + 1][c].GetComponent<Ball>().visit = true;
                    }
                }
            }


            //3개 이상인 경우 destroy
            if (count >= 3)
            {
                for (int i = 0; i < Manager.total_row; i++)
                {
                    for (int j = 0; j < Manager.Map[i].Length; j++)
                    {
                        if (Manager.Map[i][j] != null && Manager.Map[i][j].GetComponent<Ball>().visit == true)
                        {
                            sem.play(1);
                            Destroy(Manager.Map[i][j]);
                            Manager.Map[i][j] = null;                     
                        }
                    }
                }

                
                //색상갯수 감소
                if (this.gameObject.tag == "red")
                    Manager.redCnt -= count;
                else if (this.gameObject.tag == "yellow")
                    Manager.yelCnt -= count;
                else if (this.gameObject.tag == "green")
                    Manager.greCnt -= count;
                else if (this.gameObject.tag == "blue")
                    Manager.bluCnt -= count;
                else if (this.gameObject.tag == "purple")
                    Manager.purCnt -= count;

                Manager.queCnt -= qcount; //퀘스트 구슬 갯수 감소
                
            }
            


            discon_total = 0; discon_cnt = 0; //연결되지 않은 구슬갯수 초기화
            
            for(int i = 0; i < Manager.total_row; i++)
            {
                for(int j = 0; j < Manager.Map[i].Length; j++)
                {
                    if (Manager.Map[i][j] != null)
                    {
                        //visit 초기화
                        for (int k = 0; k < Manager.total_row; k++)
                        {
                            for (int l = 0; l < Manager.Map[k].Length; l++)
                            {
                                if (Manager.Map[k][l] != null)
                                    Manager.Map[k][l].GetComponent<Ball>().visit = false;
                            }
                        }

                        //각 공마다 연결돼 있는지 여부 판단
                        Stack<GameObject> s = new Stack<GameObject>();
                        s.Push(Manager.Map[i][j]);
                        Manager.Map[i][j].GetComponent<Ball>().visit = true;
                        int min_r = Manager.total_row;
                        while (s.Count != 0)
                        {
                            GameObject obj = s.Pop();
                            int r = obj.GetComponent<Ball>().row;
                            int c = obj.GetComponent<Ball>().col;

                            if (r < min_r)
                                min_r = r;

                            if (obj.GetComponent<Ball>().type == 1) //9개
                            {
                                if (r + 1 < Manager.total_row && c < Manager.Map[r + 1].Length && Manager.Map[r + 1][c] != null && Manager.Map[r + 1][c].GetComponent<Ball>().visit == false)
                                {
                                    s.Push(Manager.Map[r + 1][c]);
                                    Manager.Map[r + 1][c].GetComponent<Ball>().visit = true;
                                }
                                if (r + 1 < Manager.total_row && c + 1 < Manager.Map[r + 1].Length && Manager.Map[r + 1][c + 1] != null && Manager.Map[r + 1][c + 1].GetComponent<Ball>().visit == false)
                                {
                                    s.Push(Manager.Map[r + 1][c + 1]);
                                    Manager.Map[r + 1][c + 1].GetComponent<Ball>().visit = true;
                                }
                                if (0 <= c - 1 && Manager.Map[r][c - 1] != null && Manager.Map[r][c - 1].GetComponent<Ball>().visit == false)
                                {
                                    s.Push(Manager.Map[r][c - 1]);
                                   Manager.Map[r][c - 1].GetComponent<Ball>().visit = true;
                                }
                                if (c + 1 < Manager.Map[r].Length && Manager.Map[r][c + 1] != null && Manager.Map[r][c + 1].GetComponent<Ball>().visit == false)
                                {
                                    s.Push(Manager.Map[r][c + 1]);
                                    Manager.Map[r][c + 1].GetComponent<Ball>().visit = true;
                                }
                                if (0 <= r - 1 && c < Manager.Map[r - 1].Length && Manager.Map[r - 1][c] != null && Manager.Map[r - 1][c].GetComponent<Ball>().visit == false)
                                {
                                    s.Push(Manager.Map[r - 1][c]);
                                    Manager.Map[r - 1][c].GetComponent<Ball>().visit = true;
                                }
                                if (0 <= r - 1 && c + 1 < Manager.Map[r - 1].Length && Manager.Map[r - 1][c + 1] != null && Manager.Map[r - 1][c + 1].GetComponent<Ball>().visit == false)
                                {
                                    s.Push(Manager.Map[r - 1][c + 1]);
                                    Manager.Map[r - 1][c + 1].GetComponent<Ball>().visit = true;
                                }

                            }
                            else //10개
                            {
                                if (r + 1 < Manager.total_row && 0 <= c - 1 && Manager.Map[r + 1][c - 1] != null && Manager.Map[r + 1][c - 1].GetComponent<Ball>().visit == false)
                                {
                                    s.Push(Manager.Map[r + 1][c - 1]);
                                    Manager.Map[r + 1][c - 1].GetComponent<Ball>().visit = true;
                                }
                                if (r + 1 < Manager.total_row && c < Manager.Map[r + 1].Length && Manager.Map[r + 1][c] != null && Manager.Map[r + 1][c].GetComponent<Ball>().visit == false)
                                {
                                    s.Push(Manager.Map[r + 1][c]);
                                    Manager.Map[r + 1][c].GetComponent<Ball>().visit = true;
                                }
                                if (0 <= c - 1 && Manager.Map[r][c - 1] != null && Manager.Map[r][c - 1].GetComponent<Ball>().visit == false)
                                {
                                    s.Push(Manager.Map[r][c - 1]);
                                    Manager.Map[r][c - 1].GetComponent<Ball>().visit = true;
                                }
                                if (c + 1 < Manager.Map[r].Length && Manager.Map[r][c + 1] != null && Manager.Map[r][c + 1].GetComponent<Ball>().visit == false)
                                {
                                    s.Push(Manager.Map[r][c + 1]);
                                    Manager.Map[r][c + 1].GetComponent<Ball>().visit = true;
                                }
                                if (0 <= r - 1 && 0 <= c - 1 && Manager.Map[r - 1][c - 1] != null && Manager.Map[r - 1][c - 1].GetComponent<Ball>().visit == false)
                                {
                                    s.Push(Manager.Map[r - 1][c - 1]);
                                    Manager.Map[r - 1][c - 1].GetComponent<Ball>().visit = true;
                                }
                                if (0 <= r - 1 && c < Manager.Map[r - 1].Length && Manager.Map[r - 1][c] != null && Manager.Map[r - 1][c].GetComponent<Ball>().visit == false)
                                {
                                    s.Push(Manager.Map[r - 1][c]);
                                    Manager.Map[r - 1][c].GetComponent<Ball>().visit = true;
                                }

                            }
                        }

                        if (min_r != 0)
                        {
                    
                            Manager.Map[i][j].GetComponent<Ball>().connect = false; //connect여부 표시
                            discon_total++; //연결되지 않은 구슬 갯수
                        }
                                             
                    }
                }
            }

            //연결끊긴것들이 없으면 shooter 다시 동작하게
            if (discon_total == 0)
                Shooter.possible = true;

        }
        else if (shootball == true && collision.gameObject.tag == "ceil")
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

 
}
