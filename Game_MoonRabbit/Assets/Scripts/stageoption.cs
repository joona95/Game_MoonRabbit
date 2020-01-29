using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stageoption : MonoBehaviour
{
    public GameObject StageOption;
    // Start is called before the first frame update
    void Awake()
    {
        StageOption.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stagestart()
    {
        SceneManager.LoadScene("Stage");
    }
}
