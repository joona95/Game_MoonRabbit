using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class onoffbutton : MonoBehaviour
{
    bgmmanager bgm;//배경음악 매니저의 스크립트 불러오기
    semmanager sem;
    public GameObject bbu;//배경음악 옆의 on/off 버튼
    public GameObject sbu;//사운트 옆의 on/off 버튼
    public Image gf;
    public Image hf;
    public Image currentImage; //기존에 존제하는 이미지
    public Sprite t1;
    public Sprite t2;

    
    // Start is called before the first frame update
    void Start()
    {
        gf = bbu.GetComponent<Image>();
        hf = sbu.GetComponent<Image>();
        bgm = FindObjectOfType<bgmmanager>();
        sem = FindObjectOfType<semmanager>();
        if (GameObject.Find("BGM").GetComponent<bgmmanager>().source.volume == 0f)
        {
            GameObject.Find("bbu").GetComponent<Image>().sprite = t1;
        }
        if (GameObject.Find("SEM").GetComponent<semmanager>().source.volume == 0f)
        {
            GameObject.Find("sbu").GetComponent<Image>().sprite = t1;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (gf.sprite == t1)//위의 on off 버튼 이미지가 off버튼일때 배경음악 소리끄기
        {
            bgm.soundoff();
        }
        else if (gf.sprite == t2)
            bgm.soundon();
        if (hf.sprite == t1)//아래의 on off 버튼 이미지가 off 버튼일때 효과음 소리 끄기
        {
            sem.soundoff();
        }
        else if (hf.sprite == t2)
            sem.soundon();
    }
    public void ChangeImage()
    {
        if (currentImage.sprite == t1)
        {
            currentImage.sprite = t2;//음악 끄기 버튼으로 바꾸기
        }
        else if (currentImage.sprite == t2)
        {
            currentImage.sprite = t1;//음악 켜기 버튼으로 바꾸기
        }
    }
}
