using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeBall : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public GameObject manager;
    public bool isPressed;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("down");
        isPressed = true;
        GameObject.Find("대포").GetComponent<Shooter>().enabled = false;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("up");
        isPressed = false;
        GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
    }

    public void changeball()
    {
        //isPressed = true;
        //GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
        

        float a_x = manager.GetComponent<Manager>().ballPrefabs[0].transform.position.x;
        float a_y = manager.GetComponent<Manager>().ballPrefabs[0].transform.position.y;
        float b_x = manager.GetComponent<Manager>().ballPrefabs[1].transform.position.x;
        float b_y = manager.GetComponent<Manager>().ballPrefabs[1].transform.position.y;
        string a_color = manager.GetComponent<Manager>().ballPrefabs[0].tag;
        string b_color = manager.GetComponent<Manager>().ballPrefabs[1].tag;
        GameObject tmp = manager.GetComponent<Manager>().ballPrefabs[0];

        if (tmp.GetComponent<Ball>().rowbomb != true && tmp.GetComponent<Ball>().sixbomb != true && tmp.GetComponent<Ball>().rainbow != true)
        {
            manager.GetComponent<Manager>().ballPrefabs[0] = manager.GetComponent<Manager>().ballPrefabs[1];
            manager.GetComponent<Manager>().ballPrefabs[1] = tmp;

            manager.GetComponent<Manager>().ballPrefabs[0].transform.position = new Vector3(a_x, a_y, 0f);
            manager.GetComponent<Manager>().ballPrefabs[1].transform.position = new Vector3(b_x, b_y, 0f);
            
        }
        //isPressed = false;
        //GameObject.Find("대포").GetComponent<Shooter>().enabled = false;
        
    }
}
