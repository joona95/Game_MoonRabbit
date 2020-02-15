using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item1_Text : MonoBehaviour
{
    TextMeshProUGUI Item1Text;
    // Start is called before the first frame update
    void Start()
    {
        Item1Text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Item1Text.text = PlayerPrefs.GetInt("Item1_Cnt").ToString(); //구슬갯수제한 text로 보이게

    }
}
