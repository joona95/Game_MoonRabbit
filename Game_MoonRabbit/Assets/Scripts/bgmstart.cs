using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bgmstart : MonoBehaviour
{
    bgmmanager bgm;//스크립트에서 함수 불러옴
    // Start is called before the first frame update
    void Start()
    {
        bgm = FindObjectOfType<bgmmanager>();//여기는 맵의 배경음악을 시작할 매개체
        bgm.play(0);
        GameObject.Find("Content").GetComponent<RectTransform>().pivot = new Vector2(0f, 1f);
        GameObject.Find("Content").GetComponent<RectTransform>().anchoredPosition = new Vector2(-531.5f, 2748.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
