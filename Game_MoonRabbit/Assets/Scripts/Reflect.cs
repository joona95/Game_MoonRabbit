using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour
{
    public Vector3 normalized;

    public static float GetAngle(Vector3 incoming)
    {
        Vector3 v = incoming;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

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
        if(col.tag == "wall")
        {
            Vector3 colVec = this.gameObject.GetComponent<Transform>().position; //충돌 지점
            Vector3 currentVec = GameObject.Find("Shotspawn").transform.position; //현재 지점
            Vector3 incomingVec = colVec - currentVec; //입사 벡터

            Vector3 toVec = new Vector3(-incomingVec.x, incomingVec.y, 0f);
            //Vector3 normalVec = col.contacts[0].normal; //노말 벡터
            //Vector3 reflectVec = Vector3.Reflect(incomingVec, normalVec); //반사 벡터


            normalized = toVec.normalized;
            //Vector3 newreflectVec = -normalized;
            //newreflectVec = new Vector3(newreflectVec.x, newreflectVec.y, 2 * GetAngle(normalized));
            //Quaternion rotation = Quaternion.Euler(4*newreflectVec);
            //this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 2 * GetAngle(normalized));
            GetComponent<Rigidbody2D>().velocity = normalized * 4f;

            Debug.Log("col" + colVec);
            Debug.Log("incoming" + incomingVec);
            Debug.Log("currnet" + currentVec);
            Debug.Log("to" + toVec);

            //normalized = reflectVec.normalized; //반사 벡터 정규화
            //GetComponent<Rigidbody2D>().velocity = normalized*4f; //반사
        }

    }
}