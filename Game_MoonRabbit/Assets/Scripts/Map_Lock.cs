using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_Lock : MonoBehaviour
{
    public static bool give1 = false, give2 = false, give3 = false;
    public static bool jump=false;
    public GameObject[] rabbits = new GameObject[41];
    public GameObject[] locks = new GameObject[41];
    public GameObject[] buttons = new GameObject[41];
    public GameObject[] characters = new GameObject[4];
    public GameObject[] char_locks = new GameObject[4];
    public GameObject[] char_buttons = new GameObject[4];
    static public int stage;
    public GameObject back_ground;
    public GameObject[] gives = new GameObject[3];
    public GameObject confirm;
    public GameObject allclear, allclear_star;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        allclear.SetActive(false);
        allclear_star.SetActive(false);


        if (!PlayerPrefs.HasKey("User_stage"))
        {
            PlayerPrefs.SetInt("User_stage", 0);
        }
        stage = PlayerPrefs.GetInt("User_stage");
        Debug.Log("user_stage:" + stage);

        if (jump == true)
            stage--;

        for (int i = 1; i <= 40; i++)
        {
            if (i == stage)
            {
                float x = buttons[i].GetComponent<RectTransform>().anchoredPosition.x;
                float y = buttons[i].GetComponent<RectTransform>().anchoredPosition.y;

                rabbits[i].GetComponent<RectTransform>().anchoredPosition=new Vector2(x, y + 320f);
                rabbits[i].SetActive(true);
            }
            else
                rabbits[i].SetActive(false);
        }
        for (int i = 1; i <= 40; i++)
        {
            if (i <= stage + 1)
                buttons[i].GetComponent<Button>().interactable = true;
            else
                buttons[i].GetComponent<Button>().interactable = false;
        }
        for (int i = 2; i <= 40; i++)
        {
            if (i <= stage + 1)
                locks[i].SetActive(false);
            else
                locks[i].SetActive(true);
        }
        

        for(int i = 0; i < 4; i++)
        {
            int tmp_stage = stage;
            if (jump == true)
            {
                tmp_stage++;
            }

            if (i <= tmp_stage / 10)
            {
                char_buttons[i].GetComponent<Button>().interactable = true;
                characters[i].SetActive(true);
                char_locks[i].SetActive(false);
            }
            else
            {
                char_buttons[i].GetComponent<Button>().interactable = false;
                characters[i].SetActive(false);
                char_locks[i].SetActive(true);
            }

        }
        
        if (jump == true)
        {
            if (stage > 0)
            {
                float x;
                if (((stage - 1) / 4) % 2 == 0)
                    x = rabbits[stage].GetComponent<RectTransform>().anchoredPosition.x + 30f;
                else
                    x = rabbits[stage].GetComponent<RectTransform>().anchoredPosition.x - 30f;
                float y = rabbits[stage].GetComponent<RectTransform>().anchoredPosition.y;
                rabbits[stage].GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

                Debug.Log("(x,y):" + rabbits[stage].GetComponent<RectTransform>().anchoredPosition.x + "," + rabbits[stage].GetComponent<RectTransform>().anchoredPosition.y);
                rabbits[stage].GetComponent<Animator>().SetTrigger("jump");

                //yield return new WaitForSeconds(rabbits[stage].GetComponent<Animation>())
                //rabbits[stage].SetActive(false);
                //rabbits[++stage].SetActive(true);
            }
            else
            {
                rabbits[++stage].SetActive(true);
                buttons[Map_Lock.stage + 1].GetComponent<Button>().interactable = true;
                locks[Map_Lock.stage + 1].SetActive(false);
            }

            jump = false;
        }

        if (jump == false && Manager.clear == true&&stage==40)
        {
            allclear.SetActive(true);
            allclear_star.SetActive(true);
            Manager.clear = false;
        }
    }

  

    // Update is called once per frame
    void Update()
    {
    }
    
}
