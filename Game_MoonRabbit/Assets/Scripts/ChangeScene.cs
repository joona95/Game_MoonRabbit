using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public GameObject StageOption;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("stage:"+PlayerPrefs.GetInt("User_stage"));
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

    public void restartButton()
    {
        SceneManager.LoadScene("Stage");
    }

    public void StageStartButton()
    {
        
    }
    //back버튼
    public void BackToMapButton()
    {
        for (int i = 0; i < Manager.total_row; i++)
        {
            for (int j = 0; j < Manager.Map[i].Length; j++)
            {
                if (Manager.Map[i][j])
                {
                    Manager.Map[i][j].GetComponent<Ball>().end = true;
                }
            }
        }
        Debug.Log("back");
        SceneManager.LoadScene("Map");
    }

    //stage이동 버튼
    public void Stage1_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 1;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage2_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 2;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage3_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 3;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage4_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 4;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage5_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 5;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage6_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 6;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage7_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 7;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage8_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 8;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage9_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 9;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage10_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 10;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage11_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 11;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage12_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 12;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage13_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 13;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage14_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 14;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage15_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 15;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage16_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 16;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage17_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 17;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage18_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 18;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage19_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 19;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage20_Button()
    {
        //StageOption.gameObject.SetActive(true);
        //StageOption.gameObject.GetComponent<menuoption>().turnonoff();
        Manager.current_stage = 20;
        //SceneManager.LoadScene("Stage");
    }

    public void Stage21_Button()
    {
        Manager.current_stage = 21;
    }

    public void Stage22_Button()
    {
        Manager.current_stage = 22;
    }

    public void Stage23_Button()
    {
        Manager.current_stage = 23;
    }

    public void Stage24_Button()
    {
        Manager.current_stage = 24;
    }

    public void Stage25_Button()
    {
        Manager.current_stage = 25;
    }

    public void Stage26_Button()
    {
        Manager.current_stage = 26;
    }

    public void Stage27_Button()
    {
        Manager.current_stage = 27;
    }

    public void Stage28_Button()
    {
        Manager.current_stage = 28;
    }

    public void Stage29_Button()
    {
        Manager.current_stage = 29;
    }

    public void Stage30_Button()
    {
        Manager.current_stage = 30;
    }

    public void Stage31_Button()
    {
        Manager.current_stage = 31;
    }

    public void Stage32_Button()
    {
        Manager.current_stage = 32;
    }

    public void Stage33_Button()
    {
        Manager.current_stage = 33;
    }

    public void Stage34_Button()
    {
        Manager.current_stage = 34;
    }

    public void Stage35_Button()
    {
        Manager.current_stage = 35;
    }

    public void Stage36_Button()
    {
        Manager.current_stage = 36;
    }

    public void Stage37_Button()
    {
        Manager.current_stage = 37;
    }

    public void Stage38_Button()
    {
        Manager.current_stage = 38;
    }

    public void Stage39_Button()
    {
        Manager.current_stage = 39;
    }

    public void Stage40_Button()
    {
        Manager.current_stage = 40;
    }
}
