using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item2_Text : MonoBehaviour
{
    TextMeshProUGUI Item2Text;
    // Start is called before the first frame update
    void Start()
    {
        Item2Text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Item2Text.text = PlayerPrefs.GetInt("Item2_Cnt").ToString(); //구슬갯수제한 text로 보이게

    }

}
