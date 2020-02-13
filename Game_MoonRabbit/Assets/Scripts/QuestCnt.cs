using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestCnt : MonoBehaviour
{
    public string color;
    public TextMeshProUGUI Text;
    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (color=="red")
        {
            Text.text = Manager.red_quest.ToString() + "/" + Manager.red_queCnt.ToString();

        }

        if (color == "yellow")
        {
            Text.text = Manager.yel_quest.ToString() + "/" + Manager.yel_queCnt.ToString();

        }

        if (color == "green")
        {
            Text.text = Manager.gre_quest.ToString() + "/" + Manager.gre_queCnt.ToString();

        }

        if (color == "blue")
        {
            Text.text = Manager.blu_quest.ToString() + "/" + Manager.blu_queCnt.ToString();

        }

        if (color == "purple")
        {
            Text.text = Manager.pur_quest.ToString() + "/" + Manager.pur_queCnt.ToString();

        }
    }
}
