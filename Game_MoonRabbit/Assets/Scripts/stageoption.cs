using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stageoption : MonoBehaviour
{
    public GameObject StageOption;
    bgmmanager bgm;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stagestart()
    {
        bgm = FindObjectOfType<bgmmanager>();
        bgm.play(1);
        SceneManager.LoadScene("Stage");
    }
}
