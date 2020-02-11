using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class menuoption : MonoBehaviour
{
    public GameObject opacity;
    public GameObject opt;
    private void Awake()
    {
        opt.SetActive(false);//옵션창은 시작부터 꺼져있기 떄문

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

        if (opt.activeSelf == true)//Map 씬에서 옵션창 꺼지면서 스테이지 버튼 활성화 및 스크롤 가능
        {
            opacity.SetActive(false);
            opt.SetActive(false);
            GameObject.Find("Scroll View").GetComponent<ScrollRect>().vertical = true;
            GameObject.Find("Button1").GetComponent<Button>().interactable = true;
            GameObject.Find("Button2").GetComponent<Button>().interactable = true;
            GameObject.Find("Button3").GetComponent<Button>().interactable = true;
            GameObject.Find("Button4").GetComponent<Button>().interactable = true;
            GameObject.Find("Button5").GetComponent<Button>().interactable = true;
            GameObject.Find("Button6").GetComponent<Button>().interactable = true;
            GameObject.Find("Button7").GetComponent<Button>().interactable = true;
            GameObject.Find("Button8").GetComponent<Button>().interactable = true;
            GameObject.Find("Button9").GetComponent<Button>().interactable = true;
            GameObject.Find("Button10").GetComponent<Button>().interactable = true;
            GameObject.Find("Button11").GetComponent<Button>().interactable = true;
            GameObject.Find("Button12").GetComponent<Button>().interactable = true;
            GameObject.Find("Button13").GetComponent<Button>().interactable = true;
            GameObject.Find("Button14").GetComponent<Button>().interactable = true;
            GameObject.Find("Button15").GetComponent<Button>().interactable = true;
            GameObject.Find("Button16").GetComponent<Button>().interactable = true;
            GameObject.Find("Button17").GetComponent<Button>().interactable = true;
            GameObject.Find("Button18").GetComponent<Button>().interactable = true;
            GameObject.Find("Button19").GetComponent<Button>().interactable = true;
            GameObject.Find("Button20").GetComponent<Button>().interactable = true;
        }
        else if (opt.activeSelf == false)//옵션창 켜지면서 스크롤 및 스테이지 버튼 비활성화
        {
            opacity.SetActive(true);
            opt.SetActive(true);
            GameObject.Find("Scroll View").GetComponent<ScrollRect>().vertical = false;
            GameObject.Find("Button1").GetComponent<Button>().interactable = false;
            GameObject.Find("Button2").GetComponent<Button>().interactable = false;
            GameObject.Find("Button3").GetComponent<Button>().interactable = false;
            GameObject.Find("Button4").GetComponent<Button>().interactable = false;
            GameObject.Find("Button5").GetComponent<Button>().interactable = false;
            GameObject.Find("Button6").GetComponent<Button>().interactable = false;
            GameObject.Find("Button7").GetComponent<Button>().interactable = false;
            GameObject.Find("Button8").GetComponent<Button>().interactable = false;
            GameObject.Find("Button9").GetComponent<Button>().interactable = false;
            GameObject.Find("Button10").GetComponent<Button>().interactable = false;
            GameObject.Find("Button11").GetComponent<Button>().interactable = false;
            GameObject.Find("Button12").GetComponent<Button>().interactable = false;
            GameObject.Find("Button13").GetComponent<Button>().interactable = false;
            GameObject.Find("Button14").GetComponent<Button>().interactable = false;
            GameObject.Find("Button15").GetComponent<Button>().interactable = false;
            GameObject.Find("Button16").GetComponent<Button>().interactable = false;
            GameObject.Find("Button17").GetComponent<Button>().interactable = false;
            GameObject.Find("Button18").GetComponent<Button>().interactable = false;
            GameObject.Find("Button19").GetComponent<Button>().interactable = false;
            GameObject.Find("Button20").GetComponent<Button>().interactable = false;
        }
    }
    
}
