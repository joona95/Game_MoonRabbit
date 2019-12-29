using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LimitCnt_Text : MonoBehaviour
{
    TextMeshProUGUI limitcntText;
    // Start is called before the first frame update
    void Start()
    {
        limitcntText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        limitcntText.text = Manager.limit_cnt.ToString(); //구슬갯수제한 text로 보이게

    }
}
