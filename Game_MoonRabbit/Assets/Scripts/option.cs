using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class option : MonoBehaviour
{
    public GameObject opacity;
    public GameObject opt;//옵션창 오브젝트
    

    private void Awake()
    {
        opt.SetActive(false);
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void turnonoff()
    {
        
        if (opt.activeSelf == true)//옵션창 켜져 있을때 닫기
        {
            GameObject.Find("대포").GetComponent<Shooter>().enabled = true;//옵션창 끌때 대포 스크립트 활성화
            opacity.SetActive(false);
            opt.SetActive(false);
            
        }
        else if (opt.activeSelf == false)//옵션창 키기
        {
            GameObject.Find("대포").GetComponent<Shooter>().enabled = false;//대포 스크립트 비활성화
            opacity.SetActive(true);
            opt.SetActive(true);
            
        }
    }
}
