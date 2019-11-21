using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour
{
    public Vector3 normalized;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Vector3 colVec = col.contacts[0].point; //충돌 지점
        Vector3 currnentVec = this.gameObject.transform.position; //현재 지점
        Vector3 incomingVec = colVec - currnentVec; //입사 벡터
        Vector3 normalVec = col.contacts[0].normal; //노말 벡터
        Vector3 reflectVec = Vector3.Reflect(incomingVec, normalVec); //반사 벡터
        normalized = reflectVec.normalized; //반사 벡터 정규화
        GetComponent<Rigidbody2D>().velocity = normalized*4f; //반사
    }
}
