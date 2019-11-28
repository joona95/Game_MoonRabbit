using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int type;
    public float row, col;

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
        }
    }
}
