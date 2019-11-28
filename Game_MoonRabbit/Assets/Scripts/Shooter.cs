using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooter : MonoBehaviour
{
    Vector3 touchPos;
    float degree;

    public static float GetAngle(Vector3 from, Vector3 to)
    {
        Vector3 v = to - from;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

#if (UNITY_ANDROID || UNITY_IOS)
        if (Input.touchCount > 0)
        {
            //화면 touch 처음 하나만 인식
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began) //화면을 touch한 순간
            {
                touchPos = touch.position;
                degree = GetAngle(this.gameObject.transform.position, touchPos)-90;
                if(-80<degree&&degree<80) 
                    this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, degree);
            }
            if(touch.phase == TouchPhase.Moved) //손가락이 화면 위에서 터치한 상태로 이동
            {
                touchPos = touch.position;
                degree = GetAngle(this.gameObject.transform.position, touchPos)-90;
                if(-80<degree&&degree<80)        
                    this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, degree);
            }
            if(touch.phase == TouchPhase.Ended) //손가락이 화면에서 떨어지면 touch가 끝난 경우
            {
                if (-80 < degree && degree < 80)
                    GameObject.Find("GameObject").GetComponent<Manager>().bubblepop();
            }
        }
#else
        if (Input.GetMouseButton(0))
        {
            touchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            degree = GetAngle(this.gameObject.transform.position, touchPos) - 90;
            if(-80<degree&&degree<80)
                this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, degree);
        }
        if (Input.GetMouseButtonUp(0))//터치 뗄때
        {
            if (-80 < degree && degree < 80)
                GameObject.Find("GameObject").GetComponent<Manager>().bubblepop();//구슬 생성함수 Manager에서 불러오기
        }
#endif
    }


}
