using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterLine : MonoBehaviour
{
    static public bool linecol = false;
    public static bool avalue = false;
    SpriteRenderer sprite; //대포(경로) 이미지
    public Color color;
    public bool starcolball = false;

    // Start is called before the first frame update
    void Start()
    {
        //starcolball = false;
        linecol = false;
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        color = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "wall")
        {
            linecol = true;
            if (color.a == 1f)
            {
                avalue = true;
                Debug.Log("avlue is true");
            }
            else if(color.a == 0f) avalue = false;
        }

    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "wall")
        {
            linecol = false;
            avalue = false;
        }

    }
}
