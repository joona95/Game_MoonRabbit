using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int type; //해당 구슬이 9개 행인지 10개 행인지 알려주는 변수  0:10개, 1:9개
    public float row, col; //해당 구슬의 맵 상 위치 행, 열
    public string color; //shootball tag 바궈주기 위한 변수

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position == GameObject.Find("Shotspawn").transform.position)//발사할 공에 한정하여 발사가 되게금 하는 함수
        {
            GetComponent<Rigidbody2D>().velocity = transform.right * 5f;//발사
            this.gameObject.GetComponent<Ball>().color = this.gameObject.tag;
            this.gameObject.tag = "shootball";
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.tag == "shootball" && collision.gameObject.tag!="wall")
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero; //발사되는 공이 벽이 아닌 다른 공과 닿았을 때 멈춤
         
           

        }
    }
}
