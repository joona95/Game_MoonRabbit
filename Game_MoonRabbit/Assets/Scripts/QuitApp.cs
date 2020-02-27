using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApp : MonoBehaviour
{
    int ClickCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickCount++;
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 1.0f);

        }
        else if (ClickCount == 2)
        {
            CancelInvoke("DoubleClick");
            Application.Quit();
        }
    }

    void DoubleClick()
    {
        ClickCount = 0;
    }
}
