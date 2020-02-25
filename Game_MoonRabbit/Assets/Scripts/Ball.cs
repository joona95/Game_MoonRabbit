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
    public bool quest, diebomb, rowbomb, sixbomb, rainbow; //퀘스트 구슬인지 여부
    public bool shootball = false; //발사하는 공인지 여부
    public bool anim = false;
    public int count = 0;
    public bool end = false;
    float x = 0f, y = 0f; //발사하는 공이 날아가는 벡터 구하는 변수
    Vector3 ballway;
    semmanager sem;
    public static int anim_cnt = 0;
    public static int destroyCnt = 0;

    int ChType = Character.ChType; //캐릭터 종류 판별
    int replaynum; //ChType==2이고 diebomb 건드렸을 때 부활 여부 판별할 때 이용

    SpriteRenderer starlineSprite; //스타라인 투명도 조절 용도
    public Color starlineColor;
    public GameObject shooter;


    // Start is called before the first frame update
    void Start()
    {
        sem = FindObjectOfType<semmanager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(discon_cnt + "+" + discon_total + "+" + anim_cnt);

        if (this.gameObject.transform.position == GameObject.Find("Shotspawn").transform.position&&shootball==true)//발사할 공에 한정하여 발사가 되게끔 하는 함수
        {
            x = Mathf.Sin(Shooter.radian);
            y = Mathf.Cos(Shooter.radian);
            ballway = new Vector3(-x, y, 0f);
            //shootball = true; //shootball 발사할 공 여부 변경
            //GetComponent<Rigidbody2D>().velocity = transform.right * 5f;//발사
            GetComponent<Rigidbody2D>().velocity = ballway.normalized * 5f;
        }
        
        //연결되지 않은 경우 아래로 떨어지고 일정 위치에서 destroy
        if (connect == false)
        {
            Shooter.possible = false;
            Shooter.starlinepossible = false;
            
            this.gameObject.transform.Translate(0, -0.2f, 0, Space.World);
            if (this.gameObject.transform.position.y <= -3f)
            {

                destroyCnt++;
                Destroy(this.gameObject);
                discon_cnt++;

                //Debug.Log("connect:" + discon_cnt + "," + discon_total);
                //연결끊긴것들 다 떨어지면 shooter 다시 동작하게
                
                if (discon_cnt == discon_total)
                {
                    //Shooter.possible = true;
                    //Shooter.starlinepossible = true;
                    Manager.ing = false;
                }
                    
            }

            //Debug.Log("connect:" + discon_cnt + "," + discon_total+"("+row+","+col+")");
        }

        /*
        if (Manager.limit_cnt == 0&&Shooter.possible==true)
        {
            Manager.fail = true;

        }*/
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(collision.gameObject.tag == "starline" && Shooter.possible)
        {
            starlineSprite = collision.gameObject.GetComponent<SpriteRenderer>();
            starlineColor = starlineSprite.color;
            starlineColor.a = 0f;
            starlineSprite.color = starlineColor;
        }

        if (shootball==true && collision.gameObject.tag!="wall" && collision.gameObject.tag!="line" && collision.gameObject.tag != "starline") //shootball이 벽이 아닌 공에 닿았을 때
        {
            //Manager.ing = true;

            if (collision.gameObject.tag != "ceil" && collision.gameObject.GetComponent<Ball>().connect == true && collision.gameObject.GetComponent<Ball>().anim == false)
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
                        else if (col_y - 0.225 > this_y) //아래쪽 오른쪽 
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

                Debug.Log("shoot:"+row + "," + col);
                Manager.Map[this.gameObject.GetComponent<Ball>().row][this.gameObject.GetComponent<Ball>().col] = this.gameObject; //Map의 해당 row, col 위치에 shootball 저장
                this.gameObject.GetComponent<Ball>().type = Manager.total_col - Manager.Map[this.gameObject.GetComponent<Ball>().row].Length;


                //shootball 발사하는공 여부 변경
                shootball = false;

            }
            else if (collision.gameObject.tag == "ceil")//천장에 닿았을때
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                this.gameObject.GetComponent<Ball>().row = 0;

                float max_y = 0;
                for (int i = 0; i < Manager.Map[0].Length; i++)
                {
                    if (Manager.Map[0][i])
                    {
                        max_y = Manager.Map[0][i].transform.position.y;
                        break;
                    }
                }

                float x = Mathf.Round(this.gameObject.transform.position.x * 100) / 100;
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
                            this.gameObject.GetComponent<Ball>().col = 7;
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
                //Shooter.possible = true;
                //Shooter.starlinepossible = true;
            }
            sem.play(2);





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
                        GameObject tmp = Manager.Map[row - 1][col - 1];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");                      
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row - 1][col - 1] = null;
                        //Time.timeScale = 0f;
                        //Manager.fail = true;
                        //Manager.die = true;
                        Shooter.possible = false;
                        Shooter.starlinepossible = false;
                        GameObject.Find("대포").GetComponent<Shooter>().enabled = false;

                    }
                }
                if (0 <= row - 1 && col < Manager.Map[row - 1].Length && Manager.Map[row - 1][col] && Manager.Map[row-1][col].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row-1][col].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row - 1][col].GetComponent<Ball>().diebomb == true)
                    {
                  
                        GameObject tmp = Manager.Map[row - 1][col];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row - 1][col] = null;
                        //Time.timeScale = 0f;
                        //Manager.fail = true;
                        //Manager.die = true;
                        Shooter.possible = false;
                        Shooter.starlinepossible = false;
                        GameObject.Find("대포").GetComponent<Shooter>().enabled = false;

                    }
                }
                if (0 <= col - 1 && Manager.Map[row][col - 1] && Manager.Map[row][col-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row][col-1].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row][col - 1].GetComponent<Ball>().diebomb == true)
                    {
                        GameObject tmp = Manager.Map[row][col - 1];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row][col - 1] = null;
                        //Time.timeScale = 0f;
                        //Manager.fail = true;
                        //Manager.die = true;
                        Shooter.possible = false;
                        Shooter.starlinepossible = false;
                        GameObject.Find("대포").GetComponent<Shooter>().enabled = false;

                    }
                }
                if (col + 1 < Manager.Map[row].Length && Manager.Map[row][col + 1] && Manager.Map[row][col+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row][col+1].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row][col + 1].GetComponent<Ball>().diebomb == true)
                    {
                        GameObject tmp = Manager.Map[row][col + 1];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row][col + 1] = null;
                        //Time.timeScale = 0f;
                        //Manager.fail = true;
                        //Manager.die = true;
                        Shooter.possible = false;
                        Shooter.starlinepossible = false;
                        GameObject.Find("대포").GetComponent<Shooter>().enabled = false;

                    }
                }
                if (row + 1 < Manager.total_row && 0 <= col - 1 && Manager.Map[row + 1][col - 1] && Manager.Map[row + 1][col-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row + 1][col-1].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row + 1][col - 1].GetComponent<Ball>().diebomb == true)
                    {
                        GameObject tmp = Manager.Map[row + 1][col - 1];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row + 1][col - 1] = null;
                        //Time.timeScale = 0f;
                        //Manager.fail = true;
                        //Manager.die = true;
                        Shooter.possible = false;
                        Shooter.starlinepossible = false;
                        GameObject.Find("대포").GetComponent<Shooter>().enabled = false;

                    }
                }
                if (row + 1 < Manager.total_row && col < Manager.Map[row + 1].Length && Manager.Map[row + 1][col] && Manager.Map[row + 1][col].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row + 1][col].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row + 1][col].GetComponent<Ball>().diebomb == true)
                    {
                        GameObject tmp = Manager.Map[row + 1][col];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row + 1][col] = null;
                        //Time.timeScale = 0f;
                        //Manager.fail = true;
                        //Manager.die = true;
                        Shooter.possible = false;
                        Shooter.starlinepossible = false;
                        GameObject.Find("대포").GetComponent<Shooter>().enabled = false;

                    }
                }

                //가로줄 폭탄이 주위6개에 있는지
                //6개 폭탄이 주위6개에 있는지

                if (0 <= row - 1 && 0 <= col - 1 && Manager.Map[row - 1][col - 1] && Manager.Map[row - 1][col-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row - 1][col-1].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row - 1][col - 1].GetComponent<Ball>().rowbomb == true || Manager.Map[row - 1][col - 1].GetComponent<Ball>().sixbomb == true)
                    {
                        GameObject tmp = Manager.Map[row - 1][col - 1];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row - 1][col - 1] = null;
                    }

                }
                if (0 <= row - 1 && col < Manager.Map[row - 1].Length && Manager.Map[row - 1][col] && Manager.Map[row - 1][col].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row - 1][col].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row - 1][col].GetComponent<Ball>().rowbomb == true || Manager.Map[row - 1][col].GetComponent<Ball>().sixbomb == true)
                    {
                        GameObject tmp = Manager.Map[row - 1][col];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row - 1][col] = null;
                    }

                }
                if (0 <= col - 1 && Manager.Map[row][col - 1] && Manager.Map[row][col-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row][col-1].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row][col - 1].GetComponent<Ball>().rowbomb == true || Manager.Map[row][col - 1].GetComponent<Ball>().sixbomb == true)
                    {
                        GameObject tmp = Manager.Map[row][col - 1];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row][col - 1] = null;
                    }

                }
                if (col + 1 < Manager.Map[row].Length && Manager.Map[row][col + 1] && Manager.Map[row][col+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row][col+1].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row][col + 1].GetComponent<Ball>().rowbomb == true || Manager.Map[row][col + 1].GetComponent<Ball>().sixbomb == true)
                    {
                        GameObject tmp = Manager.Map[row][col + 1];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row][col + 1] = null;
                    }

                }
                if (row + 1 < Manager.total_row && 0 <= col - 1 && Manager.Map[row + 1][col - 1] && Manager.Map[row + 1][col-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row + 1][col-1].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row + 1][col - 1].GetComponent<Ball>().rowbomb == true || Manager.Map[row + 1][col - 1].GetComponent<Ball>().sixbomb == true)
                    {
                        GameObject tmp = Manager.Map[row + 1][col - 1];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row + 1][col - 1] = null;
                    }

                }
                if (row + 1 < Manager.total_row && col < Manager.Map[row + 1].Length && Manager.Map[row + 1][col] && Manager.Map[row + 1][col].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row + 1][col].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row + 1][col].GetComponent<Ball>().rowbomb == true || Manager.Map[row + 1][col].GetComponent<Ball>().sixbomb == true)
                    {
                        GameObject tmp = Manager.Map[row + 1][col];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row + 1][col] = null;
                    }

                }
            }
            else //9개 행
            {
                //건드리면 죽는 폭탄이 주위 6개에 있는지

                if (0 <= row - 1 && col < Manager.Map[row - 1].Length && Manager.Map[row - 1][col] && Manager.Map[row - 1][col].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row - 1][col].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row - 1][col].GetComponent<Ball>().diebomb == true)
                    {
                        GameObject tmp = Manager.Map[row - 1][col];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row - 1][col] = null;
                        //Time.timeScale = 0f;
                        //Manager.fail = true;
                        //Manager.die = true;
                        Shooter.possible = false;
                        Shooter.starlinepossible = false;
                        GameObject.Find("대포").GetComponent<Shooter>().enabled = false;
                        replaynum = Random.Range(1, 5);
                        if (ChType == 2 && replaynum == 1) //일단 1/4 확률로 부활
                        {
                            Time.timeScale = 1f;
                            GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
                        }
                    }
                }
                if (0 <= row - 1 && col + 1 < Manager.Map[row - 1].Length && Manager.Map[row - 1][col + 1] && Manager.Map[row - 1][col+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row - 1][col+1].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row - 1][col + 1].GetComponent<Ball>().diebomb == true)
                    {
                        GameObject tmp = Manager.Map[row - 1][col + 1];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row - 1][col + 1] = null;
                        //Time.timeScale = 0f;
                        //Manager.fail = true;
                        //Manager.die = true;
                        Shooter.possible = false;
                        Shooter.starlinepossible = false;
                        GameObject.Find("대포").GetComponent<Shooter>().enabled = false;
                        replaynum = Random.Range(1, 5);
                        if (ChType == 2 && replaynum == 1) //일단 1/4 확률로 부활
                        {
                            Time.timeScale = 1f;
                            GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
                        }
                    }
                }
                if (0 <= col - 1 && Manager.Map[row][col - 1] && Manager.Map[row][col-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row][col-1].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row][col - 1].GetComponent<Ball>().diebomb == true)
                    {
                        GameObject tmp = Manager.Map[row][col - 1];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row][col - 1] = null;
                        //Time.timeScale = 0f;
                        //Manager.fail = true;
                        //Manager.die = true;
                        Shooter.possible = false;
                        Shooter.starlinepossible = false;
                        GameObject.Find("대포").GetComponent<Shooter>().enabled = false;
                        replaynum = Random.Range(1, 5);
                        if (ChType == 2 && replaynum == 1) //일단 1/4 확률로 부활
                        {
                            Time.timeScale = 1f;
                            GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
                        }
                    }
                }
                if (col + 1 < Manager.Map[row].Length && Manager.Map[row][col + 1] && Manager.Map[row][col+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row][col+1].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row][col + 1].GetComponent<Ball>().diebomb == true)
                    {
                        GameObject tmp = Manager.Map[row][col + 1];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row][col + 1] = null;
                        // Time.timeScale = 0f;
                        //Manager.fail = true;
                        //Manager.die = true;
                        Shooter.possible = false;
                        Shooter.starlinepossible = false;
                        GameObject.Find("대포").GetComponent<Shooter>().enabled = false;
                        replaynum = Random.Range(1, 5);
                        if (ChType == 2 && replaynum == 1) //일단 1/4 확률로 부활
                        {
                            Time.timeScale = 1f;
                            GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
                        }
                    }
                }
                if (row + 1 < Manager.total_row && col < Manager.Map[row + 1].Length && Manager.Map[row + 1][col] && Manager.Map[row + 1][col].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row + 1][col].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row + 1][col].GetComponent<Ball>().diebomb == true)
                    {
                        GameObject tmp = Manager.Map[row + 1][col];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row + 1][col] = null;
                        //Time.timeScale = 0f;
                        //Manager.fail = true;
                        //Manager.die = true;
                        Shooter.possible = false;
                        Shooter.starlinepossible = false;
                        GameObject.Find("대포").GetComponent<Shooter>().enabled = false;
                        replaynum = Random.Range(1, 5);
                        if (ChType == 2 && replaynum == 1) //일단 1/4 확률로 부활
                        {
                            Time.timeScale = 1f;
                            GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
                        }
                    }
                }
                if (row + 1 < Manager.total_row && col + 1 < Manager.Map[row + 1].Length && Manager.Map[row + 1][col + 1] && Manager.Map[row + 1][col+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row + 1][col+1].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row + 1][col + 1].GetComponent<Ball>().diebomb == true)
                    {
                        GameObject tmp = Manager.Map[row + 1][col + 1];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row + 1][col + 1] = null;
                        //Time.timeScale = 0f;
                        //Manager.fail = true;
                        //Manager.die = true;
                        Shooter.possible = false;
                        Shooter.starlinepossible = false;
                        GameObject.Find("대포").GetComponent<Shooter>().enabled = false;
                        replaynum = Random.Range(1, 5);
                        if (ChType == 2 && replaynum == 1) //일단 1/4 확률로 부활
                        {
                            Time.timeScale = 1f;
                            GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
                        }
                    }
                }


                //가로줄 폭탄이 주위6개에 있는지
                //6개 폭탄이 주위6개에 있는지

                if (0 <= row - 1 && col < Manager.Map[row - 1].Length && Manager.Map[row - 1][col] && Manager.Map[row - 1][col].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row - 1][col].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row - 1][col].GetComponent<Ball>().rowbomb == true || Manager.Map[row - 1][col].GetComponent<Ball>().sixbomb == true)
                    {
                        GameObject tmp = Manager.Map[row - 1][col];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row - 1][col] = null;
                    }

                }
                if (0 <= row - 1 && col + 1 < Manager.Map[row - 1].Length && Manager.Map[row - 1][col + 1] && Manager.Map[row - 1][col+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row - 1][col+1].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row - 1][col + 1].GetComponent<Ball>().rowbomb == true || Manager.Map[row - 1][col + 1].GetComponent<Ball>().sixbomb == true)
                    {
                        GameObject tmp = Manager.Map[row - 1][col + 1];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row - 1][col + 1] = null;
                    }

                }
                if (0 <= col - 1 && Manager.Map[row][col - 1] && Manager.Map[row][col-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row][col-1].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row][col - 1].GetComponent<Ball>().rowbomb == true || Manager.Map[row][col - 1].GetComponent<Ball>().sixbomb == true)
                    {
                        GameObject tmp = Manager.Map[row][col - 1];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row][col - 1] = null;
                    }
                }
                if (col + 1 < Manager.Map[row].Length && Manager.Map[row][col + 1] && Manager.Map[row][col+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row][col+1].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row][col + 1].GetComponent<Ball>().rowbomb == true || Manager.Map[row][col + 1].GetComponent<Ball>().sixbomb == true)
                    {
                        GameObject tmp = Manager.Map[row][col + 1];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row][col + 1] = null;
                    }
                }
                if (row + 1 < Manager.total_row && col < Manager.Map[row + 1].Length && Manager.Map[row + 1][col] && Manager.Map[row + 1][col].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row + 1][col].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row + 1][col].GetComponent<Ball>().rowbomb == true || Manager.Map[row + 1][col].GetComponent<Ball>().sixbomb == true)
                    {
                        GameObject tmp = Manager.Map[row + 1][col];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row + 1][col] = null;
                    }
                }
                if (row + 1 < Manager.total_row && col + 1 < Manager.Map[row + 1].Length && Manager.Map[row + 1][col + 1] && Manager.Map[row + 1][col+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row + 1][col+1].gameObject.GetComponent<Ball>().anim == false)
                {
                    if (Manager.Map[row + 1][col + 1].GetComponent<Ball>().rowbomb == true || Manager.Map[row + 1][col + 1].GetComponent<Ball>().sixbomb == true)
                    {
                        GameObject tmp = Manager.Map[row + 1][col + 1];
                        
                        tmp.GetComponent<Animator>().SetTrigger("destroy");
                        special = true;
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row + 1][col + 1] = null;
                    }
                }
            }


            //item사용
            if (this.gameObject.GetComponent<Ball>().rowbomb == true)
            {
                for (int i = 0; i < Manager.Map[row].Length; i++)
                {
                    if (Manager.Map[row][i] && Manager.Map[row][i].tag != "stone"&& Manager.Map[row][i].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row][i].gameObject.GetComponent<Ball>().anim == false)
                    {
                        GameObject tmp = Manager.Map[row][i];
                        
                        if ((tmp.GetComponent<Ball>().rowbomb==true) || (tmp.GetComponent<Ball>().sixbomb==true) || (tmp.GetComponent<Ball>().diebomb == true))
                        {
                            tmp.GetComponent<Animator>().SetTrigger("destroy");
                        }
                        else
                        {
                            tmp.GetComponent<Animator>().SetTrigger("item");
                        }
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row][i] = null;

                    }
                }
            }
            else if (this.gameObject.GetComponent<Ball>().sixbomb == true)
            {
                if (Manager.Map[row].Length == 9)
                {
                    if (0 <= row - 1 && Manager.Map[row - 1][col] && Manager.Map[row - 1][col].tag != "stone" && Manager.Map[row - 1][col].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row - 1][col].gameObject.GetComponent<Ball>().anim == false)
                    {
                        GameObject tmp = Manager.Map[row-1][col];
                        
                        if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                        {
                            tmp.GetComponent<Animator>().SetTrigger("destroy");
                        }
                        else
                        {
                            tmp.GetComponent<Animator>().SetTrigger("item");
                        }
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row - 1][col] = null;

                    }
                    if (0 <= row - 1 && col + 1 < Manager.Map[row - 1].Length && Manager.Map[row - 1][col + 1] && Manager.Map[row - 1][col + 1].tag != "stone" && Manager.Map[row - 1][col+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row - 1][col+1].gameObject.GetComponent<Ball>().anim == false)
                    {
                        GameObject tmp = Manager.Map[row - 1][col+1];
                        
                        if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                        {
                            tmp.GetComponent<Animator>().SetTrigger("destroy");
                        }
                        else
                        {
                            tmp.GetComponent<Animator>().SetTrigger("item");
                        }
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row - 1][col + 1] = null;
                    }
                    if (0 <= col - 1 && Manager.Map[row][col - 1] && Manager.Map[row][col - 1].tag != "stone" && Manager.Map[row][col-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row][col-1].gameObject.GetComponent<Ball>().anim == false)
                    {
                        GameObject tmp = Manager.Map[row][col-1];
                        
                        if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                        {
                            tmp.GetComponent<Animator>().SetTrigger("destroy");
                        }
                        else
                        {
                            tmp.GetComponent<Animator>().SetTrigger("item");
                        }
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row][col - 1] = null;
                    }
                    if (col + 1 < Manager.Map[row].Length && Manager.Map[row][col + 1] && Manager.Map[row][col + 1].tag!="stone" && Manager.Map[row][col+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row][col+1].gameObject.GetComponent<Ball>().anim == false)
                    {
                        GameObject tmp = Manager.Map[row][col+1];
                        
                        if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                        {
                            tmp.GetComponent<Animator>().SetTrigger("destroy");
                        }
                        else
                        {
                            tmp.GetComponent<Animator>().SetTrigger("item");
                        }
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row][col + 1] = null;
                    }
                    if (row + 1 < Manager.total_row && Manager.Map[row + 1][col]&&Manager.Map[row+1][col].tag!="stone" && Manager.Map[row + 1][col].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row + 1][col].gameObject.GetComponent<Ball>().anim == false)
                    {
                        GameObject tmp = Manager.Map[row + 1][col];
                        
                        if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                        {
                            tmp.GetComponent<Animator>().SetTrigger("destroy");
                        }
                        else
                        {
                            tmp.GetComponent<Animator>().SetTrigger("item");
                        }
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row + 1][col] = null;
                    }
                    if (row + 1 < Manager.total_row && col + 1 < Manager.Map[row + 1].Length && Manager.Map[row + 1][col + 1]&&Manager.Map[row+1][col+1].tag!="stone" && Manager.Map[row + 1][col+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row + 1][col+1].gameObject.GetComponent<Ball>().anim == false)
                    {
                        GameObject tmp = Manager.Map[row + 1][col+1];
                        
                        if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                        {
                            tmp.GetComponent<Animator>().SetTrigger("destroy");
                        }
                        else
                        {
                            tmp.GetComponent<Animator>().SetTrigger("item");
                        }
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row + 1][col + 1] = null;
                    }

                  
                }
                else
                {
                    if (0 <= row - 1 && 0 <= col - 1 && Manager.Map[row - 1][col - 1]&&Manager.Map[row-1][col-1].tag!="stone" && Manager.Map[row - 1][col-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row - 1][col-1].gameObject.GetComponent<Ball>().anim == false)
                    {
                        GameObject tmp = Manager.Map[row - 1][col-1];
                        
                        if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                        {
                            tmp.GetComponent<Animator>().SetTrigger("destroy");
                        }
                        else
                        {
                            tmp.GetComponent<Animator>().SetTrigger("item");
                        }
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row - 1][col - 1] = null;
                    }
                    if (0 <= row - 1 && Manager.Map[row - 1][col]&&Manager.Map[row-1][col].tag!="stone" && Manager.Map[row - 1][col].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row - 1][col].gameObject.GetComponent<Ball>().anim == false)
                    {
                        GameObject tmp = Manager.Map[row - 1][col];
                        
                        if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                        {
                            tmp.GetComponent<Animator>().SetTrigger("destroy");
                        }
                        else
                        {
                            tmp.GetComponent<Animator>().SetTrigger("item");
                        }
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row - 1][col] = null;
                    }
                    if (0 <= col - 1 && Manager.Map[row][col - 1]&&Manager.Map[row][col-1].tag!="stone" && Manager.Map[row][col-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row][col-1].gameObject.GetComponent<Ball>().anim == false)
                    {
                        GameObject tmp = Manager.Map[row][col-1];
                        
                        if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                        {
                            tmp.GetComponent<Animator>().SetTrigger("destroy");
                        }
                        else
                        {
                            tmp.GetComponent<Animator>().SetTrigger("item");
                        }
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row][col - 1] = null;
                    }
                    if (col + 1 < Manager.Map[row].Length && Manager.Map[row][col + 1]&&Manager.Map[row][col+1].tag!="stone" && Manager.Map[row][col+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row][col+1].gameObject.GetComponent<Ball>().anim == false)
                    {
                        GameObject tmp = Manager.Map[row][col+1];
                        
                        if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                        {
                            tmp.GetComponent<Animator>().SetTrigger("destroy");
                        }
                        else
                        {
                            tmp.GetComponent<Animator>().SetTrigger("item");
                        }
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row][col + 1] = null;
                    }
                    if (row + 1 < Manager.total_row && 0 <= col - 1 && Manager.Map[row + 1][col - 1]&&Manager.Map[row+1][col-1].tag!="stone" && Manager.Map[row + 1][col-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row + 1][col-1].gameObject.GetComponent<Ball>().anim == false)
                    {
                        GameObject tmp = Manager.Map[row + 1][col-1];
                        
                        if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                        {
                            tmp.GetComponent<Animator>().SetTrigger("destroy");
                        }
                        else
                        {
                            tmp.GetComponent<Animator>().SetTrigger("item");
                        }
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row + 1][col - 1] = null;
                    }
                    if (row + 1 < Manager.total_row && Manager.Map[row + 1][col]&&Manager.Map[row+1][col].tag!="stone" && Manager.Map[row + 1][col].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row + 1][col].gameObject.GetComponent<Ball>().anim == false)
                    {
                        GameObject tmp = Manager.Map[row + 1][col];
                        
                        if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                        {
                            tmp.GetComponent<Animator>().SetTrigger("destroy");
                        }
                        else
                        {
                            tmp.GetComponent<Animator>().SetTrigger("item");
                        }
                        destroyCnt++;
                        Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        Manager.Map[row + 1][col] = null;
                    }
                }

                int t_r = this.gameObject.GetComponent<Ball>().row;
                int t_c = this.gameObject.GetComponent<Ball>().col;
                this.gameObject.GetComponent<Animator>().SetTrigger("destroy");
                destroyCnt++;
                Destroy(this.gameObject, this.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                Manager.Map[t_r][t_c] = null;
            }
            else if (this.gameObject.GetComponent<Ball>().rainbow == true)
            {

                int t_r = this.gameObject.GetComponent<Ball>().row;
                int t_c = this.gameObject.GetComponent<Ball>().col;

                //visit 초기화
                for (int i = 0; i < Manager.total_row; i++)
                {
                    for (int j = 0; j < Manager.Map[i].Length; j++)
                    {
                        if (Manager.Map[i][j] != null)
                            Manager.Map[i][j].GetComponent<Ball>().visit = false;
                    }
                }

                //주위 6개 bfs
                for (int z = 0; z < 6; z++)
                {

                    //현재 구슬과 동일한 색상의 연속된 구슬이 3개 이상 있는지 판별
                    Queue<GameObject> q = new Queue<GameObject>();
                    Queue<GameObject> re = new Queue<GameObject>();

                    switch (z)
                    {
                        case 0:
                            if (this.gameObject.GetComponent<Ball>().type == 0)
                            {
                                if (0 <= t_r - 1 && 0 <= t_c - 1 && Manager.Map[t_r - 1][t_c - 1] &&Manager.Map[t_r-1][t_c-1].GetComponent<Ball>().visit==false&&Manager.Map[t_r-1][t_c-1].tag!="stone"&& Manager.Map[t_r - 1][t_c - 1].GetComponent<Ball>().sixbomb==false&& Manager.Map[t_r - 1][t_c - 1].GetComponent<Ball>().diebomb==false&& Manager.Map[t_r - 1][t_c - 1].GetComponent<Ball>().rowbomb==false && Manager.Map[t_r - 1][t_c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[t_r - 1][t_c-1].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    q.Enqueue(Manager.Map[t_r - 1][t_c - 1]);
                                }
                            }
                            else
                            {
                                if (0 <= t_r - 1 && t_c < Manager.Map[t_r-1].Length && Manager.Map[t_r - 1][t_c] && Manager.Map[t_r - 1][t_c].GetComponent<Ball>().visit == false&&Manager.Map[t_r - 1][t_c].tag != "stone" && Manager.Map[t_r - 1][t_c].GetComponent<Ball>().sixbomb == false && Manager.Map[t_r - 1][t_c].GetComponent<Ball>().diebomb == false && Manager.Map[t_r - 1][t_c].GetComponent<Ball>().rowbomb == false && Manager.Map[t_r - 1][t_c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[t_r - 1][t_c].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    q.Enqueue(Manager.Map[t_r - 1][t_c]);
                                }
                            }
                            break;
                        case 1:
                            if (this.gameObject.GetComponent<Ball>().type == 0)
                            {
                                if (0 <= t_r - 1 && t_c < Manager.Map[t_r-1].Length && Manager.Map[t_r - 1][t_c] && Manager.Map[t_r - 1][t_c].GetComponent<Ball>().visit == false&& Manager.Map[t_r - 1][t_c].tag != "stone" && Manager.Map[t_r - 1][t_c ].GetComponent<Ball>().sixbomb == false && Manager.Map[t_r - 1][t_c].GetComponent<Ball>().diebomb == false && Manager.Map[t_r - 1][t_c].GetComponent<Ball>().rowbomb == false && Manager.Map[t_r - 1][t_c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[t_r - 1][t_c].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    q.Enqueue(Manager.Map[t_r - 1][t_c]);
                                }
                            }
                            else
                            {
                                if (0 <= t_r - 1 && t_c + 1 < Manager.Map[t_r-1].Length && Manager.Map[t_r - 1][t_c + 1] && Manager.Map[t_r - 1][t_c + 1].GetComponent<Ball>().visit == false&&Manager.Map[t_r - 1][t_c + 1].tag != "stone" && Manager.Map[t_r - 1][t_c + 1].GetComponent<Ball>().sixbomb == false && Manager.Map[t_r - 1][t_c + 1].GetComponent<Ball>().diebomb == false && Manager.Map[t_r - 1][t_c + 1].GetComponent<Ball>().rowbomb == false && Manager.Map[t_r - 1][t_c+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[t_r - 1][t_c+1].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    q.Enqueue(Manager.Map[t_r - 1][t_c + 1]);
                                }
                            }
                            break;
                        case 2:
                            if (0 <= t_c - 1 && Manager.Map[t_r][t_c - 1] && Manager.Map[t_r][t_c - 1].GetComponent<Ball>().visit == false&&Manager.Map[t_r][t_c - 1].tag != "stone" && Manager.Map[t_r ][t_c - 1].GetComponent<Ball>().sixbomb == false && Manager.Map[t_r][t_c - 1].GetComponent<Ball>().diebomb == false && Manager.Map[t_r][t_c - 1].GetComponent<Ball>().rowbomb == false && Manager.Map[t_r][t_c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[t_r][t_c-1].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[t_r][t_c - 1]);
                            }
                            break;
                        case 3:

                            if (t_c + 1 < Manager.Map[t_r].Length && Manager.Map[t_r][t_c + 1] && Manager.Map[t_r][t_c + 1].GetComponent<Ball>().visit == false && Manager.Map[t_r][t_c + 1].tag != "stone" && Manager.Map[t_r][t_c + 1].GetComponent<Ball>().sixbomb == false && Manager.Map[t_r][t_c + 1].GetComponent<Ball>().diebomb == false && Manager.Map[t_r][t_c + 1].GetComponent<Ball>().rowbomb == false && Manager.Map[t_r][t_c+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[t_r][t_c+1].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[t_r][t_c + 1]);
                            }

                            break;
                        case 4:
                            if (this.gameObject.GetComponent<Ball>().type == 0)
                            {
                                if (t_r + 1 < Manager.total_row && 0 <= t_c - 1 && Manager.Map[t_r + 1][t_c - 1] && Manager.Map[t_r + 1][t_c - 1].GetComponent<Ball>().visit == false&& Manager.Map[t_r + 1][t_c - 1].tag != "stone" && Manager.Map[t_r + 1][t_c - 1].GetComponent<Ball>().sixbomb == false && Manager.Map[t_r + 1][t_c - 1].GetComponent<Ball>().diebomb == false && Manager.Map[t_r + 1][t_c - 1].GetComponent<Ball>().rowbomb == false && Manager.Map[t_r+1][t_c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[t_r+1][t_c-1].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    q.Enqueue(Manager.Map[t_r + 1][t_c - 1]);
                                }
                            }
                            else
                            {
                                if (t_r + 1 < Manager.total_row && t_c < Manager.Map[t_r+1].Length && Manager.Map[t_r + 1][t_c] && Manager.Map[t_r + 1][t_c].GetComponent<Ball>().visit == false&& Manager.Map[t_r + 1][t_c ].tag != "stone" && Manager.Map[t_r + 1][t_c].GetComponent<Ball>().sixbomb == false && Manager.Map[t_r + 1][t_c].GetComponent<Ball>().diebomb == false && Manager.Map[t_r + 1][t_c].GetComponent<Ball>().rowbomb == false && Manager.Map[t_r+1][t_c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[t_r+1][t_c].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    q.Enqueue(Manager.Map[t_r + 1][t_c]);
                                }
                            }
                            break;
                        case 5:
                            if (this.gameObject.GetComponent<Ball>().type == 0)
                            {
                                if (t_r + 1 < Manager.total_row && t_c < Manager.Map[t_r+1].Length && Manager.Map[t_r + 1][t_c] && Manager.Map[t_r + 1][t_c].GetComponent<Ball>().visit == false&& Manager.Map[t_r + 1][t_c].tag != "stone" && Manager.Map[t_r + 1][t_c].GetComponent<Ball>().sixbomb == false && Manager.Map[t_r + 1][t_c].GetComponent<Ball>().diebomb == false && Manager.Map[t_r + 1][t_c].GetComponent<Ball>().rowbomb == false && Manager.Map[t_r+1][t_c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[t_r+1][t_c].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    q.Enqueue(Manager.Map[t_r + 1][t_c]);
                                }
                            }
                            else
                            {
                                if (t_r + 1 < Manager.total_row && t_c + 1 < Manager.Map[t_r+1].Length && Manager.Map[t_r + 1][t_c + 1] && Manager.Map[t_r +1][t_c + 1].GetComponent<Ball>().visit == false&& Manager.Map[t_r + 1][t_c + 1].tag != "stone" && Manager.Map[t_r + 1][t_c + 1].GetComponent<Ball>().sixbomb == false && Manager.Map[t_r + 1][t_c + 1].GetComponent<Ball>().diebomb == false && Manager.Map[t_r + 1][t_c + 1].GetComponent<Ball>().rowbomb == false && Manager.Map[t_r+1][t_c+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[t_r+1][t_c+1].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    q.Enqueue(Manager.Map[t_r + 1][t_c + 1]);
                                }
                            }
                            break;
                    }

                    string color=null;
                    if (q.Count != 0)
                    {
                        q.Peek().GetComponent<Ball>().visit = true;
                        //Debug.Log(z + ":" + q.Peek().GetComponent<Ball>().row + "," + q.Peek().GetComponent<Ball>().col);
                        color = q.Peek().tag;
                    }
                    int count = 0; //같은 색상의 연결된 구슬 갯수
                                   //int qcount = 0; //같은 색상의 연결된 구슬 중 퀘스트 구슬 갯수
                    while (q.Count != 0)
                    {
                        re.Enqueue(q.Peek());
                        GameObject obj = q.Dequeue();
                        int r = obj.GetComponent<Ball>().row;
                        int c = obj.GetComponent<Ball>().col;
                        Debug.Log(r + "," + c);

                        count++;

                        if (obj.GetComponent<Ball>().type == 1)//9개일 때 이웃한 6개
                        {
                            if (0 <= r - 1 && c < Manager.Map[r - 1].Length && Manager.Map[r - 1][c] != null && Manager.Map[r - 1][c].GetComponent<Ball>().visit == false && (Manager.Map[r - 1][c].GetComponent<Ball>().tag == color || Manager.Map[r - 1][c].GetComponent<Ball>().rainbow == true) && Manager.Map[r - 1][c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r - 1][c].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r - 1][c]);
                                Manager.Map[r - 1][c].GetComponent<Ball>().visit = true;
                                
                            }
                            if (0 <= r - 1 && c + 1 < Manager.Map[r - 1].Length && Manager.Map[r - 1][c + 1] != null && Manager.Map[r - 1][c + 1].GetComponent<Ball>().visit == false && (Manager.Map[r - 1][c + 1].GetComponent<Ball>().tag == color || Manager.Map[r - 1][c + 1].GetComponent<Ball>().rainbow == true) && Manager.Map[r - 1][c+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r - 1][c+1].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r - 1][c + 1]);
                                Manager.Map[r - 1][c + 1].GetComponent<Ball>().visit = true;
              
                            }
                            if (0 <= c - 1 && Manager.Map[r][c - 1] != null && Manager.Map[r][c - 1].GetComponent<Ball>().visit == false && (Manager.Map[r][c - 1].GetComponent<Ball>().tag == color || Manager.Map[r][c - 1].GetComponent<Ball>().rainbow == true) && Manager.Map[r][c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r][c-1].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r][c - 1]);
                                Manager.Map[r][c - 1].GetComponent<Ball>().visit = true;
                                
                            }
                            if (c + 1 < Manager.Map[r].Length && Manager.Map[r][c + 1] != null && Manager.Map[r][c + 1].GetComponent<Ball>().visit == false && (Manager.Map[r][c + 1].GetComponent<Ball>().tag == color || Manager.Map[r][c + 1].GetComponent<Ball>().rainbow == true) && Manager.Map[r][c+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r][c+1].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r][c + 1]);
                                Manager.Map[r][c + 1].GetComponent<Ball>().visit = true;

                            }
                            if (r + 1 < Manager.total_row && c < Manager.Map[r + 1].Length && Manager.Map[r + 1][c] != null && Manager.Map[r + 1][c].GetComponent<Ball>().visit == false && (Manager.Map[r + 1][c].GetComponent<Ball>().tag == color || Manager.Map[r + 1][c].GetComponent<Ball>().rainbow == true) && Manager.Map[r+1][c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r+1][c].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r + 1][c]);
                                Manager.Map[r + 1][c].GetComponent<Ball>().visit = true;

                            }
                            if (r + 1 < Manager.total_row && c + 1 < Manager.Map[r + 1].Length && Manager.Map[r + 1][c + 1] != null && Manager.Map[r + 1][c + 1].GetComponent<Ball>().visit == false && (Manager.Map[r + 1][c + 1].GetComponent<Ball>().tag == color || Manager.Map[r + 1][c + 1].GetComponent<Ball>().rainbow == true) && Manager.Map[r+1][c+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r+1][c+1].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r + 1][c + 1]);
                                Manager.Map[r + 1][c + 1].GetComponent<Ball>().visit = true;
 
                            }
                        }
                        else //10개일 때 이웃한 6개 
                        {
                            if (0 <= r - 1 && 0 <= c - 1 && Manager.Map[r - 1][c - 1] != null && Manager.Map[r - 1][c - 1].GetComponent<Ball>().visit == false && (Manager.Map[r - 1][c - 1].GetComponent<Ball>().tag == color || Manager.Map[r -1][c - 1].GetComponent<Ball>().rainbow == true) && Manager.Map[r - 1][c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r- 1][c-1].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r - 1][c - 1]);
                                Manager.Map[r - 1][c - 1].GetComponent<Ball>().visit = true;

                            }
                            if (0 <= r - 1 && c < Manager.Map[r - 1].Length && Manager.Map[r - 1][c] != null && Manager.Map[r - 1][c].GetComponent<Ball>().visit == false && (Manager.Map[r - 1][c].GetComponent<Ball>().tag == color || Manager.Map[r - 1][c].GetComponent<Ball>().rainbow == true) && Manager.Map[r - 1][c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r- 1][c].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r - 1][c]);

                                Manager.Map[r - 1][c].GetComponent<Ball>().visit = true;
                            }
                            if (0 <= c - 1 && Manager.Map[r][c - 1] != null && Manager.Map[r][c - 1].GetComponent<Ball>().visit == false && (Manager.Map[r][c - 1].GetComponent<Ball>().tag == color||Manager.Map[r][c-1].GetComponent<Ball>().rainbow==true) && Manager.Map[r][c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r][c-1].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r][c - 1]);
                                Manager.Map[r][c - 1].GetComponent<Ball>().visit = true;
                            }
                            if (c + 1 < Manager.Map[r].Length && Manager.Map[r][c + 1] != null && Manager.Map[r][c + 1].GetComponent<Ball>().visit == false && (Manager.Map[r][c + 1].GetComponent<Ball>().tag == color||Manager.Map[r][c+1].GetComponent<Ball>().rainbow==true) && Manager.Map[r][c+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r][c+1].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r][c + 1]);
                                Manager.Map[r][c + 1].GetComponent<Ball>().visit = true;
                            }
                            if (r + 1 < Manager.total_row && 0 <= c - 1 && Manager.Map[r + 1][c - 1] != null && Manager.Map[r + 1][c - 1].GetComponent<Ball>().visit == false && (Manager.Map[r + 1][c - 1].GetComponent<Ball>().tag == color||Manager.Map[r+1][c-1].GetComponent<Ball>().rainbow==true) && Manager.Map[r+1][c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r+1][c-1].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r + 1][c - 1]);
                                Manager.Map[r + 1][c - 1].GetComponent<Ball>().visit = true;
                            }
                            if (r + 1 < Manager.total_row && c < Manager.Map[r + 1].Length && Manager.Map[r + 1][c] != null && Manager.Map[r + 1][c].GetComponent<Ball>().visit == false && (Manager.Map[r + 1][c].GetComponent<Ball>().tag == color||Manager.Map[r+1][c].GetComponent<Ball>().rainbow==true) && Manager.Map[r+1][c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r+1][c].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r + 1][c]);
                                Manager.Map[r + 1][c].GetComponent<Ball>().visit = true;
                            }
                        }
                    }

                    Debug.Log(count);

                    //3개 이상인 경우 visit그대로 아닌경우 visit지우기
                    /*
                    if (count >= 3)
                    {
                        for (int i = 0; i < Manager.total_row; i++)
                        {
                            for (int j = 0; j < Manager.Map[i].Length; j++)
                            {
                                if (Manager.Map[i][j] != null && Manager.Map[i][j].GetComponent<Ball>().visit == true)
                                {
                                    Destroy(Manager.Map[i][j]);
                                    Manager.Map[i][j] = null;
                                }
                            }
                        }
                    }*/
                    
                    while (re.Count != 0)
                    {
                        if (count < 3)
                        {
                            Manager.Map[re.Peek().GetComponent<Ball>().row][re.Peek().GetComponent<Ball>().col].GetComponent<Ball>().visit = false;
                        }

                        if (Manager.Map[re.Peek().GetComponent<Ball>().row][re.Peek().GetComponent<Ball>().col].GetComponent<Ball>().rainbow == true)
                        {
                            Manager.Map[re.Peek().GetComponent<Ball>().row][re.Peek().GetComponent<Ball>().col].GetComponent<Ball>().visit = false;
                        }
                        re.Dequeue();
                    }
                    
                }

                
                //visit한 곳 다 destroy
                for (int i = 0; i < Manager.total_row; i++)
                {
                    for (int j = 0; j < Manager.Map[i].Length; j++)
                    {
                        if (Manager.Map[i][j] != null && Manager.Map[i][j].GetComponent<Ball>().visit == true)
                        {
                            GameObject tmp = Manager.Map[i][j];
                            
                            tmp.GetComponent<Animator>().SetTrigger("rainbow");
                            destroyCnt++;
                            Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                            Manager.Map[i][j] = null;
                        }
                    }
                }

                sem.play(3);
                //GameObject tmp = Manager.Map[t_r][t_c];
                
                this.gameObject.GetComponent<Animator>().SetTrigger("destroy");
                destroyCnt++;
                Destroy(this.gameObject, this.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                Manager.Map[t_r][t_c] = null;
            }
            else//item 사용아닐때
            {
               


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
                            if (0 <= r - 1 && c < Manager.Map[r - 1].Length && Manager.Map[r - 1][c] != null && Manager.Map[r - 1][c].GetComponent<Ball>().visit == false && Manager.Map[r - 1][c].GetComponent<Ball>().tag == this.gameObject.tag && Manager.Map[r - 1][c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r - 1][c].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r - 1][c]);
                                Manager.Map[r - 1][c].GetComponent<Ball>().visit = true;
                            }
                            if (0 <= r - 1 && c + 1 < Manager.Map[r - 1].Length && Manager.Map[r - 1][c + 1] != null && Manager.Map[r - 1][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r - 1][c + 1].GetComponent<Ball>().tag == this.gameObject.tag && Manager.Map[r - 1][c+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r - 1][c+1].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r - 1][c + 1]);
                                Manager.Map[r - 1][c + 1].GetComponent<Ball>().visit = true;
                            }
                            if (0 <= c - 1 && Manager.Map[r][c - 1] != null && Manager.Map[r][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r][c - 1].GetComponent<Ball>().tag == this.gameObject.tag && Manager.Map[r][c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r][c-1].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r][c - 1]);
                                Manager.Map[r][c - 1].GetComponent<Ball>().visit = true;
                            }
                            if (c + 1 < Manager.Map[r].Length && Manager.Map[r][c + 1] != null && Manager.Map[r][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r][c + 1].GetComponent<Ball>().tag == this.gameObject.tag && Manager.Map[r][c+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r][c+1].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r][c + 1]);
                                Manager.Map[r][c + 1].GetComponent<Ball>().visit = true;
                            }
                            if (r + 1 < Manager.total_row && c < Manager.Map[r + 1].Length && Manager.Map[r + 1][c] != null && Manager.Map[r + 1][c].GetComponent<Ball>().visit == false && Manager.Map[r + 1][c].GetComponent<Ball>().tag == this.gameObject.tag && Manager.Map[r+1][c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r+1][c].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r + 1][c]);
                                Manager.Map[r + 1][c].GetComponent<Ball>().visit = true;
                            }
                            if (r + 1 < Manager.total_row && c + 1 < Manager.Map[r + 1].Length && Manager.Map[r + 1][c + 1] != null && Manager.Map[r + 1][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r + 1][c + 1].GetComponent<Ball>().tag == this.gameObject.tag && Manager.Map[r+1][c+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r+1][c+1].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r + 1][c + 1]);
                                Manager.Map[r + 1][c + 1].GetComponent<Ball>().visit = true;
                            }
                        }
                        else //10개일 때 이웃한 6개 
                        {
                            if (0 <= r - 1 && 0 <= c - 1 && Manager.Map[r - 1][c - 1] != null && Manager.Map[r - 1][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r - 1][c - 1].GetComponent<Ball>().tag == this.gameObject.tag && Manager.Map[r- 1][c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r - 1][c-1].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r - 1][c - 1]);
                                Manager.Map[r - 1][c - 1].GetComponent<Ball>().visit = true;
                            }
                            if (0 <= r - 1 && c < Manager.Map[r - 1].Length && Manager.Map[r - 1][c] != null && Manager.Map[r - 1][c].GetComponent<Ball>().visit == false && Manager.Map[r - 1][c].GetComponent<Ball>().tag == this.gameObject.tag && Manager.Map[r - 1][c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r - 1][c].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r - 1][c]);
                                Manager.Map[r - 1][c].GetComponent<Ball>().visit = true;
                            }
                            if (0 <= c - 1 && Manager.Map[r][c - 1] != null && Manager.Map[r][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r][c - 1].GetComponent<Ball>().tag == this.gameObject.tag && Manager.Map[r][c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r][c-1].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r][c - 1]);
                                Manager.Map[r][c - 1].GetComponent<Ball>().visit = true;
                            }
                            if (c + 1 < Manager.Map[r].Length && Manager.Map[r][c + 1] != null && Manager.Map[r][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r][c + 1].GetComponent<Ball>().tag == this.gameObject.tag)
                            {
                                q.Enqueue(Manager.Map[r][c + 1]);
                                Manager.Map[r][c + 1].GetComponent<Ball>().visit = true;
                            }
                            if (r + 1 < Manager.total_row && 0 <= c - 1 && Manager.Map[r + 1][c - 1] != null && Manager.Map[r + 1][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r + 1][c - 1].GetComponent<Ball>().tag == this.gameObject.tag && Manager.Map[r+1][c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r+1][c-1].gameObject.GetComponent<Ball>().anim == false)
                            {
                                q.Enqueue(Manager.Map[r + 1][c - 1]);
                                Manager.Map[r + 1][c - 1].GetComponent<Ball>().visit = true;
                            }
                            if (r + 1 < Manager.total_row && c < Manager.Map[r + 1].Length && Manager.Map[r + 1][c] != null && Manager.Map[r + 1][c].GetComponent<Ball>().visit == false && Manager.Map[r + 1][c].GetComponent<Ball>().tag == this.gameObject.tag && Manager.Map[r+1][c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r+1][c].gameObject.GetComponent<Ball>().anim == false)
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
                                    GameObject tmp = Manager.Map[i][j];
                                    Manager.Map[i][j] = null;
                                    if (tmp.GetComponent<Ball>().quest == false)
                                    {
                                        tmp.GetComponent<Animator>().SetTrigger("bomb");
                                    }
                                    else
                                    {
                                        if (tmp.tag == "red")
                                        {
                                            tmp.GetComponent<Animator>().SetTrigger("red_quest");
                                        }
                                        else if (tmp.tag == "yellow")
                                        {
                                            tmp.GetComponent<Animator>().SetTrigger("yellow_quest");
                                        }
                                        else if (tmp.tag == "green")
                                        {
                                            tmp.GetComponent<Animator>().SetTrigger("green_quest");
                                        }
                                        else if (tmp.tag == "blue")
                                        {
                                            tmp.GetComponent<Animator>().SetTrigger("blue_quest");
                                        }
                                        else if (tmp.tag == "purple")
                                        {
                                            tmp.GetComponent<Animator>().SetTrigger("purple_quest");
                                        }

                                        
                                    }

                                    destroyCnt++;
                                    Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                                    
                                }
                            }
                        }


                    }
                }


            }




            //연결 여부 판별
            discon_total = 0; discon_cnt = 0; //연결되지 않은 구슬갯수 초기화

            for (int i = 0; i < Manager.total_row; i++)
            {
                for (int j = 0; j < Manager.Map[i].Length; j++)
                {
                    if (Manager.Map[i][j] != null && Manager.Map[i][j].gameObject.GetComponent<Ball>().connect == true && Manager.Map[i][j].gameObject.GetComponent<Ball>().anim == false)
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
                                if (r + 1 < Manager.total_row && c < Manager.Map[r + 1].Length && Manager.Map[r + 1][c] != null && Manager.Map[r + 1][c].GetComponent<Ball>().visit == false && Manager.Map[r+1][c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r+1][c].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    s.Push(Manager.Map[r + 1][c]);
                                    Manager.Map[r + 1][c].GetComponent<Ball>().visit = true;
                                }
                                if (r + 1 < Manager.total_row && c + 1 < Manager.Map[r + 1].Length && Manager.Map[r + 1][c + 1] != null && Manager.Map[r + 1][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r+1][c+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r+1][c+1].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    s.Push(Manager.Map[r + 1][c + 1]);
                                    Manager.Map[r + 1][c + 1].GetComponent<Ball>().visit = true;
                                }
                                if (0 <= c - 1 && Manager.Map[r][c - 1] != null && Manager.Map[r][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r][c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r][c-1].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    s.Push(Manager.Map[r][c - 1]);
                                    Manager.Map[r][c - 1].GetComponent<Ball>().visit = true;
                                }
                                if (c + 1 < Manager.Map[r].Length && Manager.Map[r][c + 1] != null && Manager.Map[r][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r][c+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r][c+1].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    s.Push(Manager.Map[r][c + 1]);
                                    Manager.Map[r][c + 1].GetComponent<Ball>().visit = true;
                                }
                                if (0 <= r - 1 && c < Manager.Map[r - 1].Length && Manager.Map[r - 1][c] != null && Manager.Map[r - 1][c].GetComponent<Ball>().visit == false && Manager.Map[r - 1][c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r- 1][c].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    s.Push(Manager.Map[r - 1][c]);
                                    Manager.Map[r - 1][c].GetComponent<Ball>().visit = true;
                                }
                                if (0 <= r - 1 && c + 1 < Manager.Map[r - 1].Length && Manager.Map[r - 1][c + 1] != null && Manager.Map[r - 1][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r - 1][c+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r - 1][c+1].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    s.Push(Manager.Map[r - 1][c + 1]);
                                    Manager.Map[r - 1][c + 1].GetComponent<Ball>().visit = true;
                                }

                            }
                            else //10개
                            {
                                if (r + 1 < Manager.total_row && 0 <= c - 1 && Manager.Map[r + 1][c - 1] != null && Manager.Map[r + 1][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r+1][c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r+1][c-1].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    s.Push(Manager.Map[r + 1][c - 1]);
                                    Manager.Map[r + 1][c - 1].GetComponent<Ball>().visit = true;
                                }
                                if (r + 1 < Manager.total_row && c < Manager.Map[r + 1].Length && Manager.Map[r + 1][c] != null && Manager.Map[r + 1][c].GetComponent<Ball>().visit == false && Manager.Map[r+1][c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r+1][c].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    s.Push(Manager.Map[r + 1][c]);
                                    Manager.Map[r + 1][c].GetComponent<Ball>().visit = true;
                                }
                                if (0 <= c - 1 && Manager.Map[r][c - 1] != null && Manager.Map[r][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r][c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r][c-1].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    s.Push(Manager.Map[r][c - 1]);
                                    Manager.Map[r][c - 1].GetComponent<Ball>().visit = true;
                                }
                                if (c + 1 < Manager.Map[r].Length && Manager.Map[r][c + 1] != null && Manager.Map[r][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r][c+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r][c+1].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    s.Push(Manager.Map[r][c + 1]);
                                    Manager.Map[r][c + 1].GetComponent<Ball>().visit = true;
                                }
                                if (0 <= r - 1 && 0 <= c - 1 && Manager.Map[r - 1][c - 1] != null && Manager.Map[r - 1][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r - 1][c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r - 1][c-1].gameObject.GetComponent<Ball>().anim == false)
                                {
                                    s.Push(Manager.Map[r - 1][c - 1]);
                                    Manager.Map[r - 1][c - 1].GetComponent<Ball>().visit = true;
                                }
                                if (0 <= r - 1 && c < Manager.Map[r - 1].Length && Manager.Map[r - 1][c] != null && Manager.Map[r - 1][c].GetComponent<Ball>().visit == false && Manager.Map[r - 1][c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r - 1][c].gameObject.GetComponent<Ball>().anim == false)
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
            {
                //Shooter.possible = true;
                //Shooter.starlinepossible = true;
                Manager.ing = false;

                /*
                if (Manager.limit_cnt == 0)
                {
                    Manager.fail = true;

                }*/
            }
                




        }
        
    }



    public void OnDestroy()
    {
        if (end == false)
        {
            Manager.ing = true;

            Debug.Log("in:" + destroyCnt);
            
            if (this.gameObject.tag == "red")
            {
                Manager.redCnt--;
                if (quest == true)
                {
                    Manager.red_quest++;
                    Manager.queCnt--;
                    Debug.Log("quest:" + Manager.queCnt);
                }
            }
            else if (this.gameObject.tag == "yellow")
            {
                Manager.yelCnt--;
                if (quest == true)
                {
                    Manager.yel_quest++;
                    Manager.queCnt--;
                    Debug.Log("quest:" + Manager.queCnt);
                }
            }
            else if (this.gameObject.tag == "green")
            {
                Manager.greCnt--;
                if (quest == true)
                {
                    Manager.gre_quest++;
                    Manager.queCnt--;
                    Debug.Log("quest:" + Manager.queCnt);
                }
            }
            else if (this.gameObject.tag == "blue")
            {
                Manager.bluCnt--;
                if (quest == true)
                {
                    Manager.blu_quest++;
                    Manager.queCnt--;
                    Debug.Log("quest:" + Manager.queCnt);
                }
            }
            else if (this.gameObject.tag == "purple")
            {
                Manager.purCnt--;
                if (quest == true)
                {
                    Manager.pur_quest++;
                    Manager.queCnt--;
                    Debug.Log("quest:" + Manager.queCnt);
                }
            }

            
            /*
            if (count != 0 && this.gameObject.transform.position.y >= 0.4f)
            {
                Manager.limit_cnt += count;
                
                if (LimitCnt_Text.limit)
                {
                    LimitCnt_Text.limit.GetComponent<Animator>().SetTrigger("size");
                }
                

            }*/

            if (diebomb == true && this.gameObject.transform.position.y >= 0.4f)
            {
                Debug.Log("die**");
                //Time.timeScale = 0f; 
                //Shooter.possible = false;
                if (GameObject.Find("대포"))
                {
                    GameObject.Find("대포").GetComponent<Shooter>().enabled = false;
                    Shooter.possible = false;
                    Shooter.starlinepossible = false;

                }

                replaynum = Random.Range(1, 5);
                if (ChType == 2 && replaynum == 1) //일단 1/4 확률로 부활
                {
                    Time.timeScale = 1f;
                    GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
                    Shooter.possible = true;
                    Shooter.starlinepossible = true;
                }
                else
                {
                    //Manager.fail = true;                   
                    //Manager.die = true;

                }
            }

            if (rowbomb == true&&this.gameObject.transform.position.y>=0.4f)
            {
                if (row <= Manager.total_row)
                {
                    for (int i = 0; i < Manager.Map[row].Length; i++)
                    {
                        if (Manager.Map[row][i] && Manager.Map[row][i].tag != "stone" && Manager.Map[row][i].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row][i].gameObject.GetComponent<Ball>().anim == false)
                        {
                            GameObject tmp = Manager.Map[row][i];

                            if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                            {
                                tmp.GetComponent<Animator>().SetTrigger("destroy");
                            }
                            else
                            {
                                tmp.GetComponent<Animator>().SetTrigger("item");
                            }
                            destroyCnt++;
                            Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                            Manager.Map[row][i] = null;
                        }
                    }
                    sem.play(4);
                }
            }

            if (sixbomb == true&&this.gameObject.transform.position.y>=0.4f)
            {
                if (row <= Manager.total_row)
                {
                    if (Manager.Map[row].Length == 9)
                    {
                        if (0 <= row - 1 && Manager.Map[row - 1][col] && Manager.Map[row - 1][col].tag != "stone" && Manager.Map[row - 1][col].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row - 1][col].gameObject.GetComponent<Ball>().anim == false)
                        {
                            GameObject tmp = Manager.Map[row - 1][col];

                            if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                            {
                                tmp.GetComponent<Animator>().SetTrigger("destroy");
                            }
                            else
                            {
                                tmp.GetComponent<Animator>().SetTrigger("item");
                            }
                            destroyCnt++;
                            Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                            Manager.Map[row - 1][col] = null;
                        }
                        if (0 <= row - 1 && col + 1 < Manager.Map[row - 1].Length && Manager.Map[row - 1][col + 1] && Manager.Map[row - 1][col + 1].tag != "stone" && Manager.Map[row - 1][col + 1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row - 1][col + 1].gameObject.GetComponent<Ball>().anim == false)
                        {
                            GameObject tmp = Manager.Map[row - 1][col + 1];

                            if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                            {
                                tmp.GetComponent<Animator>().SetTrigger("destroy");
                            }
                            else
                            {
                                tmp.GetComponent<Animator>().SetTrigger("item");
                            }
                            destroyCnt++;
                            Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                            Manager.Map[row - 1][col + 1] = null;
                        }
                        if (0 <= col - 1 && Manager.Map[row][col - 1] && Manager.Map[row][col - 1].tag != "stone" && Manager.Map[row][col - 1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row][col - 1].gameObject.GetComponent<Ball>().anim == false)
                        {
                            GameObject tmp = Manager.Map[row][col - 1];

                            if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                            {
                                tmp.GetComponent<Animator>().SetTrigger("destroy");
                            }
                            else
                            {
                                tmp.GetComponent<Animator>().SetTrigger("item");
                            }
                            destroyCnt++;
                            Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                            Manager.Map[row][col - 1] = null;
                        }
                        if (col + 1 < Manager.Map[row].Length && Manager.Map[row][col + 1] && Manager.Map[row][col + 1].tag != "stone" && Manager.Map[row][col + 1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row][col + 1].gameObject.GetComponent<Ball>().anim == false)
                        {
                            GameObject tmp = Manager.Map[row][col + 1];

                            if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                            {
                                tmp.GetComponent<Animator>().SetTrigger("destroy");
                            }
                            else
                            {
                                tmp.GetComponent<Animator>().SetTrigger("item");
                            }
                            destroyCnt++;
                            Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                            Manager.Map[row][col + 1] = null;
                        }
                        if (row + 1 < Manager.total_row && Manager.Map[row + 1][col] && Manager.Map[row + 1][col].tag != "stone" && Manager.Map[row + 1][col].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row + 1][col].gameObject.GetComponent<Ball>().anim == false)
                        {
                            GameObject tmp = Manager.Map[row + 1][col];

                            if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                            {
                                tmp.GetComponent<Animator>().SetTrigger("destroy");
                            }
                            else
                            {
                                tmp.GetComponent<Animator>().SetTrigger("item");
                            }
                            destroyCnt++;
                            Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                            Manager.Map[row + 1][col] = null;
                        }
                        if (row + 1 < Manager.total_row && col + 1 < Manager.Map[row + 1].Length && Manager.Map[row + 1][col + 1] && Manager.Map[row + 1][col + 1].tag != "stone" && Manager.Map[row + 1][col + 1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row + 1][col + 1].gameObject.GetComponent<Ball>().anim == false)
                        {
                            GameObject tmp = Manager.Map[row + 1][col + 1];

                            if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                            {
                                tmp.GetComponent<Animator>().SetTrigger("destroy");
                            }
                            else
                            {
                                tmp.GetComponent<Animator>().SetTrigger("item");
                            }
                            destroyCnt++;
                            Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                            Manager.Map[row + 1][col + 1] = null;
                        }
                    }
                    else
                    {
                        if (0 <= row - 1 && 0 <= col - 1 && Manager.Map[row - 1][col - 1] && Manager.Map[row - 1][col - 1].tag != "stone" && Manager.Map[row - 1][col - 1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row - 1][col - 1].gameObject.GetComponent<Ball>().anim == false)
                        {
                            GameObject tmp = Manager.Map[row - 1][col - 1];

                            if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                            {
                                tmp.GetComponent<Animator>().SetTrigger("destroy");
                            }
                            else
                            {
                                tmp.GetComponent<Animator>().SetTrigger("item");
                            }
                            destroyCnt++;
                            Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                            Manager.Map[row - 1][col - 1] = null;
                        }
                        if (0 <= row - 1 && Manager.Map[row - 1][col] && Manager.Map[row - 1][col].tag != "stone" && Manager.Map[row - 1][col].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row - 1][col].gameObject.GetComponent<Ball>().anim == false)
                        {
                            GameObject tmp = Manager.Map[row - 1][col];
                            Manager.Map[row - 1][col] = null;
                            if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                            {
                                tmp.GetComponent<Animator>().SetTrigger("destroy");
                            }
                            else
                            {
                                tmp.GetComponent<Animator>().SetTrigger("item");
                            }
                            destroyCnt++;
                            Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                        }
                        if (0 <= col - 1 && Manager.Map[row][col - 1] && Manager.Map[row][col - 1].tag != "stone" && Manager.Map[row][col - 1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row][col - 1].gameObject.GetComponent<Ball>().anim == false)
                        {
                            GameObject tmp = Manager.Map[row][col - 1];

                            if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                            {
                                tmp.GetComponent<Animator>().SetTrigger("destroy");
                            }
                            else
                            {
                                tmp.GetComponent<Animator>().SetTrigger("item");
                            }
                            destroyCnt++;
                            Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                            Manager.Map[row][col - 1] = null;
                        }
                        if (col + 1 < Manager.Map[row].Length && Manager.Map[row][col + 1] && Manager.Map[row][col + 1].tag != "stone" && Manager.Map[row][col + 1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row][col + 1].gameObject.GetComponent<Ball>().anim == false)
                        {
                            GameObject tmp = Manager.Map[row][col + 1];

                            if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                            {
                                tmp.GetComponent<Animator>().SetTrigger("destroy");
                            }
                            else
                            {
                                tmp.GetComponent<Animator>().SetTrigger("item");
                            }
                            destroyCnt++;
                            Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                            Manager.Map[row][col + 1] = null;
                        }
                        if (row + 1 < Manager.total_row && 0 <= col - 1 && Manager.Map[row + 1][col - 1] && Manager.Map[row + 1][col - 1].tag != "stone" && Manager.Map[row + 1][col - 1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row + 1][col - 1].gameObject.GetComponent<Ball>().anim == false)
                        {
                            GameObject tmp = Manager.Map[row + 1][col - 1];

                            if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                            {
                                tmp.GetComponent<Animator>().SetTrigger("destroy");
                            }
                            else
                            {
                                tmp.GetComponent<Animator>().SetTrigger("item");
                            }
                            destroyCnt++;
                            Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                            Manager.Map[row + 1][col - 1] = null;
                        }
                        if (row + 1 < Manager.total_row && Manager.Map[row + 1][col] && Manager.Map[row + 1][col].tag != "stone" && Manager.Map[row + 1][col].gameObject.GetComponent<Ball>().connect == true && Manager.Map[row + 1][col].gameObject.GetComponent<Ball>().anim == false)
                        {
                            GameObject tmp = Manager.Map[row + 1][col];

                            if ((tmp.GetComponent<Ball>().rowbomb == true) || (tmp.GetComponent<Ball>().sixbomb == true) || (tmp.GetComponent<Ball>().diebomb == true))
                            {
                                tmp.GetComponent<Animator>().SetTrigger("destroy");
                            }
                            else
                            {
                                tmp.GetComponent<Animator>().SetTrigger("item");
                            }
                            destroyCnt++;
                            Destroy(tmp, tmp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                            Manager.Map[row + 1][col] = null;
                        }
                    }
                    sem.play(4);
                }
            }

            //Manager.ing = false;

            if ((rowbomb == true || sixbomb == true)&&this.gameObject.transform.position.y>=0.4f)
            {
                Manager.ing = true;
                //연결 여부 판별
                discon_total = 0; discon_cnt = 0; //연결되지 않은 구슬갯수 초기화

                for (int i = 0; i < Manager.total_row; i++)
                {
                    for (int j = 0; j < Manager.Map[i].Length; j++)
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
                                    if (r + 1 < Manager.total_row && c < Manager.Map[r + 1].Length && Manager.Map[r + 1][c] != null && Manager.Map[r + 1][c].GetComponent<Ball>().visit == false && Manager.Map[r+1][c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r+1][c].gameObject.GetComponent<Ball>().anim == false)
                                    {
                                        s.Push(Manager.Map[r + 1][c]);
                                        Manager.Map[r + 1][c].GetComponent<Ball>().visit = true;
                                    }
                                    if (r + 1 < Manager.total_row && c + 1 < Manager.Map[r + 1].Length && Manager.Map[r + 1][c + 1] != null && Manager.Map[r + 1][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r+1][c+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r+1][c+1].gameObject.GetComponent<Ball>().anim == false)
                                    {
                                        s.Push(Manager.Map[r + 1][c + 1]);
                                        Manager.Map[r + 1][c + 1].GetComponent<Ball>().visit = true;
                                    }
                                    if (0 <= c - 1 && Manager.Map[r][c - 1] != null && Manager.Map[r][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r][c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r][c-1].gameObject.GetComponent<Ball>().anim == false)
                                    {
                                        s.Push(Manager.Map[r][c - 1]);
                                        Manager.Map[r][c - 1].GetComponent<Ball>().visit = true;
                                    }
                                    if (c + 1 < Manager.Map[r].Length && Manager.Map[r][c + 1] != null && Manager.Map[r][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r][c+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r][c+1].gameObject.GetComponent<Ball>().anim == false)
                                    {
                                        s.Push(Manager.Map[r][c + 1]);
                                        Manager.Map[r][c + 1].GetComponent<Ball>().visit = true;
                                    }
                                    if (0 <= r - 1 && c < Manager.Map[r - 1].Length && Manager.Map[r - 1][c] != null && Manager.Map[r - 1][c].GetComponent<Ball>().visit == false && Manager.Map[r- 1][c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r - 1][c].gameObject.GetComponent<Ball>().anim == false)
                                    {
                                        s.Push(Manager.Map[r - 1][c]);
                                        Manager.Map[r - 1][c].GetComponent<Ball>().visit = true;
                                    }
                                    if (0 <= r - 1 && c + 1 < Manager.Map[r - 1].Length && Manager.Map[r - 1][c + 1] != null && Manager.Map[r - 1][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r - 1][c+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r- 1][c+1].gameObject.GetComponent<Ball>().anim == false)
                                    {
                                        s.Push(Manager.Map[r - 1][c + 1]);
                                        Manager.Map[r - 1][c + 1].GetComponent<Ball>().visit = true;
                                    }

                                }
                                else //10개
                                {
                                    if (r + 1 < Manager.total_row && 0 <= c - 1 && Manager.Map[r + 1][c - 1] != null && Manager.Map[r + 1][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r+1][c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r+1][c-1].gameObject.GetComponent<Ball>().anim == false)
                                    {
                                        s.Push(Manager.Map[r + 1][c - 1]);
                                        Manager.Map[r + 1][c - 1].GetComponent<Ball>().visit = true;
                                    }
                                    if (r + 1 < Manager.total_row && c < Manager.Map[r + 1].Length && Manager.Map[r + 1][c] != null && Manager.Map[r + 1][c].GetComponent<Ball>().visit == false && Manager.Map[r+1][c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r+1][c].gameObject.GetComponent<Ball>().anim == false)
                                    {
                                        s.Push(Manager.Map[r + 1][c]);
                                        Manager.Map[r + 1][c].GetComponent<Ball>().visit = true;
                                    }
                                    if (0 <= c - 1 && Manager.Map[r][c - 1] != null && Manager.Map[r][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r][c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r][c-1].gameObject.GetComponent<Ball>().anim == false)
                                    {
                                        s.Push(Manager.Map[r][c - 1]);
                                        Manager.Map[r][c - 1].GetComponent<Ball>().visit = true;
                                    }
                                    if (c + 1 < Manager.Map[r].Length && Manager.Map[r][c + 1] != null && Manager.Map[r][c + 1].GetComponent<Ball>().visit == false && Manager.Map[r][c+1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r][c+1].gameObject.GetComponent<Ball>().anim == false)
                                    {
                                        s.Push(Manager.Map[r][c + 1]);
                                        Manager.Map[r][c + 1].GetComponent<Ball>().visit = true;
                                    }
                                    if (0 <= r - 1 && 0 <= c - 1 && Manager.Map[r - 1][c - 1] != null && Manager.Map[r - 1][c - 1].GetComponent<Ball>().visit == false && Manager.Map[r- 1][c-1].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r- 1][c-1].gameObject.GetComponent<Ball>().anim == false)
                                    {
                                        s.Push(Manager.Map[r - 1][c - 1]);
                                        Manager.Map[r - 1][c - 1].GetComponent<Ball>().visit = true;
                                    }
                                    if (0 <= r - 1 && c < Manager.Map[r - 1].Length && Manager.Map[r - 1][c] != null && Manager.Map[r - 1][c].GetComponent<Ball>().visit == false && Manager.Map[r - 1][c].gameObject.GetComponent<Ball>().connect == true && Manager.Map[r- 1][c].gameObject.GetComponent<Ball>().anim == false)
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
                {
                    //Shooter.possible = true;
                    //Shooter.starlinepossible = true;
                    //Manager.ing = false;
                }
            }

            destroyCnt--;
            if (destroyCnt == 0)
            {
                Manager.ing = false;
            }
            Debug.Log("out:" + destroyCnt);
        }
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "starline" && Shooter.possible)
        {
            starlineSprite = collision.gameObject.GetComponent<SpriteRenderer>();
            starlineColor = starlineSprite.color;
            starlineColor.a = 1f;
            starlineSprite.color = starlineColor;
            
        }
    }

    public void animationstart()
    {
        //Shooter.possible = false;
        //Shooter.starlinepossible = false;
        Manager.ing = true;
        anim = true;
        anim_cnt++;

        if (count != 0 )
        {
            Manager.limit_cnt += count;

            if (LimitCnt_Text.limit)
            {
                LimitCnt_Text.limit.GetComponent<Animator>().SetTrigger("size");
            }


        }
    }

    public void animationend()
    {
        anim_cnt--;
        //Shooter.possible = true;
        //Shooter.starlinepossible = true;
        if(anim_cnt==0)
            //Manager.ing = false;

        /*
        if (Manager.limit_cnt == 0)
        {
            Manager.fail = true;
            
        }*/

        if (diebomb == true)
        {
            Manager.fail = true;
            Manager.die = true;
        }

    }


    
}
