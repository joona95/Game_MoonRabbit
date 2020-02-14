using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stage_Text : MonoBehaviour
{
    TextMeshProUGUI stageText;
    // Start is called before the first frame update
    void Start()
    {
        stageText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        stageText.text = "STAGE "+Manager.current_stage.ToString();
    }
}
