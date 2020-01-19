using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //start버튼
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Map");
        
    }

    //back버튼
    public void BackToMapButton()
    {
        Debug.Log("back");
        SceneManager.LoadScene("Map");
    }

    //stage이동 버튼
    public void Stage1_Button()
    {
        
        Manager.current_stage = 1;
        SceneManager.LoadScene("Stage");
    }

    public void Stage2_Button()
    {
        Manager.current_stage = 2;
        SceneManager.LoadScene("Stage");
    }

    public void Stage3_Button()
    {
        Manager.current_stage = 3;
        SceneManager.LoadScene("Stage");
    }

    public void Stage4_Button()
    {
        Manager.current_stage = 4;
        SceneManager.LoadScene("Stage");
    }

    public void Stage5_Button()
    {
        Manager.current_stage = 5;
        SceneManager.LoadScene("Stage");
    }

    public void Stage6_Button()
    {
        Manager.current_stage = 6;
        SceneManager.LoadScene("Stage");
    }

    public void Stage7_Button()
    {
        Manager.current_stage = 7;
        SceneManager.LoadScene("Stage");
    }

    public void Stage8_Button()
    {
        Manager.current_stage = 8;
        SceneManager.LoadScene("Stage");
    }

    public void Stage9_Button()
    {
        Manager.current_stage = 9;
        SceneManager.LoadScene("Stage");
    }

    public void Stage10_Button()
    {
        Manager.current_stage = 10;
        SceneManager.LoadScene("Stage");
    }

    public void Stage11_Button()
    {
        Manager.current_stage = 11;
        SceneManager.LoadScene("Stage");
    }

    public void Stage12_Button()
    {
        Manager.current_stage = 12;
        SceneManager.LoadScene("Stage");
    }

    public void Stage13_Button()
    {
        Manager.current_stage = 13;
        SceneManager.LoadScene("Stage");
    }

    public void Stage14_Button()
    {
        Manager.current_stage = 14;
        SceneManager.LoadScene("Stage");
    }

    public void Stage15_Button()
    {
        Manager.current_stage = 15;
        SceneManager.LoadScene("Stage");
    }

    public void Stage16_Button()
    {
        Manager.current_stage = 16;
        SceneManager.LoadScene("Stage");
    }

    public void Stage17_Button()
    {
        Manager.current_stage = 17;
        SceneManager.LoadScene("Stage");
    }

    public void Stage18_Button()
    {
        Manager.current_stage = 18;
        SceneManager.LoadScene("Stage");
    }

    public void Stage19_Button()
    {
        Manager.current_stage = 19;
        SceneManager.LoadScene("Stage");
    }

    public void Stage20_Button()
    {
        Manager.current_stage = 20;
        SceneManager.LoadScene("Stage");
    }
}
