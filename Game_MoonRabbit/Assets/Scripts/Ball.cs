using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int type; //해당 구슬이 9개 행인지 10개 행인지 알려주는 변수  0:10개, 1:9개
    public int row, col; //해당 구슬의 맵 상 위치 행, 열
    public string color; //shootball tag 바궈주기 위한 변수
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position == GameObject.Find("Shotspawn").transform.position)//발사할 공에 한정하여 발사가 되게끔 하는 함수
        {
            GetComponent<Rigidbody2D>().velocity = transform.right * 5f;//발사
            this.gameObject.GetComponent<Ball>().color = this.gameObject.tag; //원래 tag 저장
            this.gameObject.tag = "shootball"; //발사할 공 tag 변경
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.tag == "shootball" && collision.gameObject.tag!="wall") //shootball이 벽이 아닌 공에 닿았을 때
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero; //발사되는 공이 벽이 아닌 다른 공과 닿았을 때 멈춤

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
                        this.gameObject.GetComponent<Ball>().col = collision.gameObject.GetComponent<Ball>().col+1;
                        this.gameObject.transform.position = new Vector3(col_x + 0.52f, col_y , 0f);
                    }
                }
                else //col_x >= this_x
                {
                    if (col_y + 0.225 < this_y) //위쪽 왼쪽
                    {
                        this.gameObject.GetComponent<Ball>().row = collision.gameObject.GetComponent<Ball>().row - 1;
                        this.gameObject.GetComponent<Ball>().col = collision.gameObject.GetComponent<Ball>().col -1;
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
            

            int t; //현재 row의 갯수
            if (Manager.Map[this.gameObject.GetComponent<Ball>().row - 1].Length == Manager.total_col) //현재 row의 이전 행의 배열 수가 10 -> 현재 9
            {
                this.gameObject.GetComponent<Ball>().type = 1;
                t = Manager.total_col-1;
            }
            else //현재 row의 이전 행의 배열 수가 9 -> 현재 10
            {
                this.gameObject.GetComponent<Ball>().type = 0;
                t = Manager.total_col;
            }


            if (this.gameObject.GetComponent<Ball>().row >= Manager.total_row) //total_row보다 현재 row가 크면 Map에 새로운 배열을 넣어주어야함. total_row도 증가.
            {
                GameObject[] tmp = new GameObject[t];
                Manager.Map.Add(tmp);
                Manager.total_row++;
            }

            Manager.Map[this.gameObject.GetComponent<Ball>().row][this.gameObject.GetComponent<Ball>().col] = this.gameObject; //Map의 해당 row, col 위치에 shootball 저장


            //tag를 shootball에서 다시 색깔이름으로 바꿔주기
            this.gameObject.tag = this.gameObject.GetComponent<Ball>().color;
        }
    }
}
