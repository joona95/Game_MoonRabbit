﻿using System.Collections;
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
    public bool quest, diebomb, rowbomb, sixbomb, rainbow; //퀘스트 구슬인지 여부
    public bool shootball = false; //발사하는 공인지 여부
    public int count = 0;
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

            
            



            //item사용
            if (this.gameObject.GetComponent<Ball>().rowbomb == true)
            {
                Destroy(this.gameObject);
            }
            else if (this.gameObject.GetComponent<Ball>().sixbomb == true)
            {
                Destroy(this.gameObject);
            }
            else if (this.gameObject.GetComponent<Ball>().rainbow == true)
            {
                //주위 6개 bfs

                Destroy(this.gameObject);
            }
            else//item 사용아닐때
            {
                //10,11,12,13,14:갯수증가+2 빨,노,초,파,보 15,16,17,18,19:갯수감소-2 빨,노,초,파,보 
                //20:돌 21:건드리면죽는폭탄 22:가로줄폭탄 23:6개폭탄 24:무지개
                bool special = false; //특별구슬을 건드렸는지 여부 판별
                if (this.gameObject.GetComponent<Ball>().type == 0) //10개 행
                {
                    //건드리면 죽는 폭탄이 주위 6개에 있는지

                    if (0 <= row - 1 && 0 <= col - 1 && Manager.Map[row - 1][col - 1])
                    {
                        if (Manager.Map[row - 1][col - 1].GetComponent<Ball>().diebomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row - 1][col - 1]);
                            Time.timeScale = 0f;
                        }
                    }
                    if (0 <= row - 1 && col < Manager.Map[row - 1].Length && Manager.Map[row - 1][col])
                    {
                        if (Manager.Map[row - 1][col].GetComponent<Ball>().diebomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row - 1][col]);
                            Time.timeScale = 0f;
                        }
                    }
                    if (0 <= col - 1 && Manager.Map[row][col - 1])
                    {
                        if (Manager.Map[row][col - 1].GetComponent<Ball>().diebomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row][col - 1]);
                            Time.timeScale = 0f;
                        }
                    }
                    if (col + 1 < Manager.Map[row].Length && Manager.Map[row][col + 1])
                    {
                        if (Manager.Map[row][col + 1].GetComponent<Ball>().diebomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row][col + 1]);
                            Time.timeScale = 0f;
                        }
                    }
                    if (row + 1 < Manager.total_row && 0 <= col - 1 && Manager.Map[row + 1][col - 1])
                    {
                        if (Manager.Map[row + 1][col - 1].GetComponent<Ball>().diebomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row + 1][col - 1]);
                            Time.timeScale = 0f;
                        }
                    }
                    if (row + 1 < Manager.total_row && col < Manager.Map[row + 1].Length && Manager.Map[row + 1][col])
                    {
                        if (Manager.Map[row + 1][col].GetComponent<Ball>().diebomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row + 1][col]);
                            Time.timeScale = 0f;
                        }
                    }

                    //가로줄 폭탄이 주위6개에 있는지
                    //6개 폭탄이 주위6개에 있는지

                    if (0 <= row - 1 && 0 <= col - 1 && Manager.Map[row - 1][col - 1])
                    {
                        if (Manager.Map[row - 1][col - 1].GetComponent<Ball>().rowbomb == true || Manager.Map[row - 1][col - 1].GetComponent<Ball>().sixbomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row - 1][col - 1]);
                        }

                    }
                    if (0 <= row - 1 && col < Manager.Map[row - 1].Length && Manager.Map[row - 1][col])
                    {
                        if (Manager.Map[row - 1][col].GetComponent<Ball>().rowbomb == true || Manager.Map[row - 1][col].GetComponent<Ball>().sixbomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row - 1][col]);
                        }

                    }
                    if (0 <= col - 1 && Manager.Map[row][col - 1])
                    {
                        if (Manager.Map[row][col - 1].GetComponent<Ball>().rowbomb == true || Manager.Map[row][col - 1].GetComponent<Ball>().sixbomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row][col - 1]);
                        }

                    }
                    if (col + 1 < Manager.Map[row].Length && Manager.Map[row][col + 1])
                    {
                        if (Manager.Map[row][col + 1].GetComponent<Ball>().rowbomb == true || Manager.Map[row][col + 1].GetComponent<Ball>().sixbomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row][col + 1]);
                        }

                    }
                    if (row + 1 < Manager.total_row && 0 <= col - 1 && Manager.Map[row + 1][col - 1])
                    {
                        if (Manager.Map[row + 1][col - 1].GetComponent<Ball>().rowbomb == true || Manager.Map[row + 1][col - 1].GetComponent<Ball>().sixbomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row + 1][col - 1]);
                        }

                    }
                    if (row + 1 < Manager.total_row && col < Manager.Map[row + 1].Length && Manager.Map[row + 1][col])
                    {
                        if (Manager.Map[row + 1][col].GetComponent<Ball>().rowbomb == true || Manager.Map[row + 1][col].GetComponent<Ball>().sixbomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row + 1][col]);
                        }

                    }
                }
                else //9개 행
                {
                    //건드리면 죽는 폭탄이 주위 6개에 있는지

                    if (0 <= row - 1 && col < Manager.Map[row - 1].Length && Manager.Map[row - 1][col])
                    {
                        if (Manager.Map[row - 1][col].GetComponent<Ball>().diebomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row - 1][col]);
                            Time.timeScale = 0f;
                        }
                    }
                    if (0 <= row - 1 && col + 1 < Manager.Map[row - 1].Length && Manager.Map[row - 1][col + 1])
                    {
                        if (Manager.Map[row - 1][col + 1].GetComponent<Ball>().diebomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row - 1][col + 1]);
                            Time.timeScale = 0f;
                        }
                    }
                    if (0 <= col - 1 && Manager.Map[row][col - 1])
                    {
                        if (Manager.Map[row][col - 1].GetComponent<Ball>().diebomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row][col - 1]);
                            Time.timeScale = 0f;
                        }
                    }
                    if (col + 1 < Manager.Map[row].Length && Manager.Map[row][col + 1])
                    {
                        if (Manager.Map[row][col + 1].GetComponent<Ball>().diebomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row][col + 1]);
                            Time.timeScale = 0f;
                        }
                    }
                    if (row + 1 < Manager.total_row && col < Manager.Map[row + 1].Length && Manager.Map[row + 1][col])
                    {
                        if (Manager.Map[row + 1][col].GetComponent<Ball>().diebomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row + 1][col]);
                            Time.timeScale = 0f;
                        }
                    }
                    if (row + 1 < Manager.total_row && col + 1 < Manager.Map[row + 1].Length && Manager.Map[row + 1][col + 1])
                    {
                        if (Manager.Map[row + 1][col + 1].GetComponent<Ball>().diebomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row + 1][col + 1]);
                            Time.timeScale = 0f;
                        }
                    }


                    //가로줄 폭탄이 주위6개에 있는지
                    //6개 폭탄이 주위6개에 있는지

                    if (0 <= row - 1 && col < Manager.Map[row - 1].Length && Manager.Map[row - 1][col])
                    {
                        if (Manager.Map[row - 1][col].GetComponent<Ball>().rowbomb == true || Manager.Map[row - 1][col].GetComponent<Ball>().sixbomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row - 1][col]);
                        }

                    }
                    if (0 <= row - 1 && col + 1 < Manager.Map[row - 1].Length && Manager.Map[row - 1][col + 1])
                    {
                        if (Manager.Map[row - 1][col + 1].GetComponent<Ball>().rowbomb == true || Manager.Map[row - 1][col + 1].GetComponent<Ball>().sixbomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row - 1][col + 1]);
                        }

                    }
                    if (0 <= col - 1 && Manager.Map[row][col - 1])
                    {
                        if (Manager.Map[row][col - 1].GetComponent<Ball>().rowbomb == true || Manager.Map[row][col - 1].GetComponent<Ball>().sixbomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row][col - 1]);
                        }
                    }
                    if (col + 1 < Manager.Map[row].Length && Manager.Map[row][col + 1])
                    {
                        if (Manager.Map[row][col + 1].GetComponent<Ball>().rowbomb == true || Manager.Map[row][col + 1].GetComponent<Ball>().sixbomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row][col + 1]);
                        }
                    }
                    if (row + 1 < Manager.total_row && col < Manager.Map[row + 1].Length && Manager.Map[row + 1][col])
                    {
                        if (Manager.Map[row + 1][col].GetComponent<Ball>().rowbomb == true || Manager.Map[row + 1][col].GetComponent<Ball>().sixbomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row + 1][col]);
                        }
                    }
                    if (row + 1 < Manager.total_row && col + 1 < Manager.Map[row + 1].Length && Manager.Map[row + 1][col + 1])
                    {
                        if (Manager.Map[row + 1][col + 1].GetComponent<Ball>().rowbomb == true || Manager.Map[row + 1][col + 1].GetComponent<Ball>().sixbomb == true)
                        {
                            special = true;
                            Destroy(Manager.Map[row + 1][col + 1]);
                        }
                    }
                }


                if (special == false) //특별구슬없으면 그냥 주위 색깔 판별로 터뜨리기
                {
                    //visit 초기화
                    for (int i = 0; i < Manager.total_row; i++)
                    {
                        for (int j = 0; j < Manager.Map[i].Length; j++)
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
                                   //int qcount = 0; //같은 색상의 연결된 구슬 중 퀘스트 구슬 갯수
                    while (q.Count != 0)
                    {
                        GameObject obj = q.Dequeue();
                        int r = obj.GetComponent<Ball>().row;
                        int c = obj.GetComponent<Ball>().col;

                        count++;

                        if (obj.GetComponent<Ball>().type == 1)//9개일 때 이웃한 6개
                        {
                            if (0 <= r - 1 && c < Manager.Map[r - 1].Length && Manager.Map[r - 1][c] != null && Manager.Map[r - 1][c].GetComponent<Ball>().visit == false && Manager.Map[r - 1][c].GetComponent<Ball>().tag == this.gameObject.tag)
                            {
                                q.Enqueue(Manager.Map[r - 1][c]);
                                Manager.Map[r - 1][c].GetComponent<Ball>().visit = true;
                            }
                            if (0 <= r - 1 && c + 1 < Manager.Map[r - 1].Length && Manager.Map[r - 1][c + 1] != null && Manager.Map[r - 1][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r - 1][c + 1].GetComponent<Ball>().tag == this.gameObject.tag)
                            {
                                q.Enqueue(Manager.Map[r - 1][c + 1]);
                                Manager.Map[r - 1][c + 1].GetComponent<Ball>().visit = true;
                            }
                            if (0 <= c - 1 && Manager.Map[r][c - 1] != null && Manager.Map[r][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r][c - 1].GetComponent<Ball>().tag == this.gameObject.tag)
                            {
                                q.Enqueue(Manager.Map[r][c - 1]);
                                Manager.Map[r][c - 1].GetComponent<Ball>().visit = true;
                            }
                            if (c + 1 < Manager.Map[r].Length && Manager.Map[r][c + 1] != null && Manager.Map[r][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r][c + 1].GetComponent<Ball>().tag == this.gameObject.tag)
                            {
                                q.Enqueue(Manager.Map[r][c + 1]);
                                Manager.Map[r][c + 1].GetComponent<Ball>().visit = true;
                            }
                            if (r + 1 < Manager.total_row && c < Manager.Map[r + 1].Length && Manager.Map[r + 1][c] != null && Manager.Map[r + 1][c].GetComponent<Ball>().visit == false && Manager.Map[r + 1][c].GetComponent<Ball>().tag == this.gameObject.tag)
                            {
                                q.Enqueue(Manager.Map[r + 1][c]);
                                Manager.Map[r + 1][c].GetComponent<Ball>().visit = true;
                            }
                            if (r + 1 < Manager.total_row && c + 1 < Manager.Map[r + 1].Length && Manager.Map[r + 1][c + 1] != null && Manager.Map[r + 1][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r + 1][c + 1].GetComponent<Ball>().tag == this.gameObject.tag)
                            {
                                q.Enqueue(Manager.Map[r + 1][c + 1]);
                                Manager.Map[r + 1][c + 1].GetComponent<Ball>().visit = true;
                            }
                        }
                        else //10개일 때 이웃한 6개 
                        {
                            if (0 <= r - 1 && 0 <= c - 1 && Manager.Map[r - 1][c - 1] != null && Manager.Map[r - 1][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r - 1][c - 1].GetComponent<Ball>().tag == this.gameObject.tag)
                            {
                                q.Enqueue(Manager.Map[r - 1][c - 1]);
                                Manager.Map[r - 1][c - 1].GetComponent<Ball>().visit = true;
                            }
                            if (0 <= r - 1 && c < Manager.Map[r - 1].Length && Manager.Map[r - 1][c] != null && Manager.Map[r - 1][c].GetComponent<Ball>().visit == false && Manager.Map[r - 1][c].GetComponent<Ball>().tag == this.gameObject.tag)
                            {
                                q.Enqueue(Manager.Map[r - 1][c]);
                                Manager.Map[r - 1][c].GetComponent<Ball>().visit = true;
                            }
                            if (0 <= c - 1 && Manager.Map[r][c - 1] != null && Manager.Map[r][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r][c - 1].GetComponent<Ball>().tag == this.gameObject.tag)
                            {
                                q.Enqueue(Manager.Map[r][c - 1]);
                                Manager.Map[r][c - 1].GetComponent<Ball>().visit = true;
                            }
                            if (c + 1 < Manager.Map[r].Length && Manager.Map[r][c + 1] != null && Manager.Map[r][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r][c + 1].GetComponent<Ball>().tag == this.gameObject.tag)
                            {
                                q.Enqueue(Manager.Map[r][c + 1]);
                                Manager.Map[r][c + 1].GetComponent<Ball>().visit = true;
                            }
                            if (r + 1 < Manager.total_row && 0 <= c - 1 && Manager.Map[r + 1][c - 1] != null && Manager.Map[r + 1][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r + 1][c - 1].GetComponent<Ball>().tag == this.gameObject.tag)
                            {
                                q.Enqueue(Manager.Map[r + 1][c - 1]);
                                Manager.Map[r + 1][c - 1].GetComponent<Ball>().visit = true;
                            }
                            if (r + 1 < Manager.total_row && c < Manager.Map[r + 1].Length && Manager.Map[r + 1][c] != null && Manager.Map[r + 1][c].GetComponent<Ball>().visit == false && Manager.Map[r + 1][c].GetComponent<Ball>().tag == this.gameObject.tag)
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


                    }
                }


            }


            


            //연결 여부 판별
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
        else if (shootball == true && collision.gameObject.tag == "ceil")//천장에 닿았을때
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.gameObject.GetComponent<Ball>().row = 0;

            float max_y = 0;
            for (int i = 0; i < 9; i++)
            {
                if (Manager.Map[0][i])
                {
                    max_y = Manager.Map[0][i].transform.position.y;
                    break;
                }
            }

            float x = Mathf.Round(this.gameObject.transform.position.x*100)/100;
            if (Manager.Map[0].Length == 9)//첫번째 행이 9개일때
            {
                this.gameObject.GetComponent<Ball>().type = 1;
                if (-0.26f <= x && x < 0.26f)
                {
                    if (!Manager.Map[0][4])
                    {
                        this.gameObject.GetComponent<Ball>().col = 4;
                        Manager.Map[0][4] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(0f, max_y, 0f);
                    }
                }
                else if (-0.78f <= x && x < -0.26f)
                {
                    if (!Manager.Map[0][3])
                    {
                        this.gameObject.GetComponent<Ball>().col = 3;
                        Manager.Map[0][3] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(-0.52f, max_y, 0f);
                    }
                }
                else if (-1.30f <= x && x < -0.78f)
                {
                    if (!Manager.Map[0][2])
                    {
                        this.gameObject.GetComponent<Ball>().col = 2;
                        Manager.Map[0][2] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(-1.04f, max_y, 0f);
                    }
                }
                else if (-1.82f <= x && x < -1.30f)
                {
                    if (!Manager.Map[0][1])
                    {
                        this.gameObject.GetComponent<Ball>().col = 1;
                        Manager.Map[0][1] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(-1.56f, max_y, 0f);
                    }
                }
                else if (x < -1.82f)
                {
                    if (!Manager.Map[0][0])
                    {
                        this.gameObject.GetComponent<Ball>().col = 0;
                        Manager.Map[0][0] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(-2.08f, max_y, 0f);
                    }
                }
                else if (0.26f <= x && x < 0.78f)
                {
                    if (!Manager.Map[0][5])
                    {
                        this.gameObject.GetComponent<Ball>().col = 5;
                        Manager.Map[0][5] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(0.52f, max_y, 0f);
                    }
                }
                else if (0.78f <= x && x < 1.30f)
                {
                    if (!Manager.Map[0][6])
                    {
                        this.gameObject.GetComponent<Ball>().col = 6;
                        Manager.Map[0][6] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(1.04f, max_y, 0f);
                    }
                }
                else if (1.30f <= x && x < 1.82f)
                {
                    if (!Manager.Map[0][7])
                    {
                        this.gameObject.GetComponent<Ball>().col = 7;
                        Manager.Map[0][7] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(1.56f, max_y, 0f);
                    }
                }
                else if (1.82f <= x)
                {
                    if (!Manager.Map[0][8])
                    {
                        this.gameObject.GetComponent<Ball>().col = 8;
                        Manager.Map[0][8] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(2.08f, max_y, 0f);
                    }
                }
            }
            else//첫번째 행이 10개일때
            {
                this.gameObject.GetComponent<Ball>().type = 0;

                if (-0.52f <= x && x < 0f)
                {
                    if (!Manager.Map[0][4])
                    {
                        this.gameObject.GetComponent<Ball>().col = 4;
                        Manager.Map[0][4] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(-0.26f, max_y, 0f);
                    }
                }
                else if (-1.04f <= x && x < -0.52f)
                {
                    if (!Manager.Map[0][3])
                    {
                        this.gameObject.GetComponent<Ball>().col = 3;
                        Manager.Map[0][3] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(-0.78f, max_y, 0f);
                    }
                }
                else if (-1.56f <= x && x < -1.04f)
                {
                    if (!Manager.Map[0][2])
                    {
                        this.gameObject.GetComponent<Ball>().col = 2;
                        Manager.Map[0][2] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(-1.30f, max_y, 0f);
                    }
                }
                else if (-2.08f <= x && x < -1.56f)
                {
                    if (!Manager.Map[0][1])
                    {
                        this.gameObject.GetComponent<Ball>().col = 1;
                        Manager.Map[0][1] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(-1.82f, max_y, 0f);
                    }
                }
                else if (x < -2.08f)
                {
                    if (!Manager.Map[0][0])
                    {
                        this.gameObject.GetComponent<Ball>().col = 0;
                        Manager.Map[0][0] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(-2.34f, max_y, 0f);
                    }
                }
                else if (0f <= x && x < 0.52f)
                {
                    if (!Manager.Map[0][5])
                    {
                        this.gameObject.GetComponent<Ball>().col = 5;
                        Manager.Map[0][5] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(0.26f, max_y, 0f);
                    }
                }
                else if (0.52f <= x && x < 1.04f)
                {
                    if (!Manager.Map[0][6])
                    {
                        this.gameObject.GetComponent<Ball>().col = 6;
                        Manager.Map[0][6] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(0.78f, max_y, 0f);
                    }
                }
                else if (1.04f <= x && x < 1.56f)
                {
                    if (!Manager.Map[0][7])
                    {
                        this.gameObject.GetComponent<Ball>().col=7;
                        Manager.Map[0][7] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(1.30f, max_y, 0f);
                    }
                }
                else if (1.56f <= x && x < 2.08f)
                {
                    if (!Manager.Map[0][8])
                    {
                        this.gameObject.GetComponent<Ball>().col = 8;
                        Manager.Map[0][8] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(1.82f, max_y, 0f);
                    }
                }
                else if (2.08f <= x)
                {
                    if (!Manager.Map[0][9])
                    {
                        this.gameObject.GetComponent<Ball>().col = 9;
                        Manager.Map[0][9] = this.gameObject;
                        this.gameObject.transform.position = new Vector3(2.34f, max_y, 0f);
                    }
                }
            }

            shootball = false;
            Shooter.possible = true;
        }
    }



    public void OnDestroy()
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

        if (quest == true)
        {
            Manager.queCnt--;
        }

        if (count != 0)
        {
            Manager.limit_cnt += count;
        }
        
        if (rowbomb == true)
        {
            for(int i = 0; i < Manager.Map[row].Length; i++)
            {
                Destroy(Manager.Map[row][i]);
            }
        }

        if (sixbomb == true)
        {
            if (Manager.Map[row].Length == 9)
            {
                if (0 <= row - 1 && Manager.Map[row - 1][col])
                {
                    Destroy(Manager.Map[row - 1][col]);
                }
                if (0 <= row - 1 && col + 1 < Manager.Map[row - 1].Length && Manager.Map[row - 1][col + 1])
                {
                    Destroy(Manager.Map[row - 1][col + 1]);
                }
                if (0 <= col - 1 && Manager.Map[row][col - 1])
                {
                    Destroy(Manager.Map[row][col - 1]);
                }
                if (col + 1 < Manager.Map[row].Length && Manager.Map[row][col + 1])
                {
                    Destroy(Manager.Map[row][col + 1]);
                }
                if (row + 1 < Manager.total_row && Manager.Map[row + 1][col])
                {
                    Destroy(Manager.Map[row + 1][col]);
                }
                if (row + 1 < Manager.total_row && col + 1 < Manager.Map[row + 1].Length && Manager.Map[row + 1][col + 1])
                {
                    Destroy(Manager.Map[row + 1][col + 1]);
                }
            }
            else
            {
                if (0 <= row - 1 && 0 <= col - 1 && Manager.Map[row - 1][col - 1])
                {
                    Destroy(Manager.Map[row - 1][col - 1]);
                }
                if (0 <= row - 1 && Manager.Map[row - 1][col])
                {
                    Destroy(Manager.Map[row - 1][col]);
                }
                if (0 <= col - 1 && Manager.Map[row][col - 1])
                {
                    Destroy(Manager.Map[row][col - 1]);
                }
                if (col + 1 < Manager.Map[row].Length && Manager.Map[row][col + 1])
                {
                    Destroy(Manager.Map[row][col + 1]);
                }
                if (row + 1 < Manager.total_row && 0 <= col - 1 && Manager.Map[row + 1][col - 1])
                {
                    Destroy(Manager.Map[row + 1][col - 1]);
                }
                if (row + 1 < Manager.total_row && Manager.Map[row + 1][col])
                {
                    Destroy(Manager.Map[row + 1][col]);
                }
            }
        }
    }
}
