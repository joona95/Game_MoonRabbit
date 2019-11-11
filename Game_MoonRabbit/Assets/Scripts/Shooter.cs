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

#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            touchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            degree = GetAngle(this.gameObject.transform.position, touchPos) - 90;
            this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, degree);
        }
#else
        if (Input.touchCount > 0)
        {
            //화면 touch 처음 하나만 인식
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began) //화면을 touch한 순간
            {
                touchPos = touch.position;
                degree = GetAngle(this.gameObject.transform.position, touchPos)-90;
                this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, degree);
            }
            if(touch.phase == TouchPhase.Moved) //손가락이 화면 위에서 터치한 상태로 이동
            {
                touchPos = touch.position;
                degree = GetAngle(this.gameObject.transform.position, touchPos)-90;
                this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, degree);
            }
            if(touch.phase == TouchPhase.Ended) //손가락이 화면에서 떨어지면 touch가 끝난 경우
            {

            }
        }
#endif
    }

#if UNITY_EDITOR
    //사용자가 마우스 놓을 때
    private void OnMouseUp()
    {
        
    }
#endif
}
