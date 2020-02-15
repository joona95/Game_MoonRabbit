using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item3_Text : MonoBehaviour
{
    TextMeshProUGUI Item3Text;
    // Start is called before the first frame update
    void Start()
    {
        Item3Text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Item3Text.text = PlayerPrefs.GetInt("Item3_Cnt").ToString(); //구슬갯수제한 text로 보이게

    }

}
