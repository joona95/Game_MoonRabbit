using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour
{
    Vector3 normalized;
    Vector3 colVec;
    Vector3 currentVec;
    Vector3 incomingVec;
    Vector3 toVec;

    int firstcol = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "wall" && firstcol == 0) // 첫 충돌
        {
            colVec = this.gameObject.GetComponent<Transform>().position; //충돌 지점
            currentVec = GameObject.Find("Shotspawn").transform.position; //시작 지점
            incomingVec = colVec - currentVec; //입사 벡터
            toVec = new Vector3(-incomingVec.x, incomingVec.y, 0f); //향할 방향
            normalized = toVec.normalized; // 정규화
            GetComponent<Rigidbody2D>().velocity = normalized * 4f; //반사
            firstcol++;
        }
        else if(col.tag == "wall" && firstcol > 0) //두번째 충돌부터
        {
            incomingVec = toVec;
            toVec = new Vector3(-incomingVec.x, incomingVec.y, 0f);
            normalized = toVec.normalized;
            GetComponent<Rigidbody2D>().velocity = normalized * 5f;
        }

    }
}