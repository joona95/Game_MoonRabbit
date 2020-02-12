using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_Lock : MonoBehaviour
{
    public GameObject[] rabbits = new GameObject[21];
    public GameObject[] locks = new GameObject[21];
    public GameObject[] buttons = new GameObject[21];
    public GameObject[] characters = new GameObject[4];
    public GameObject[] char_locks = new GameObject[4];
    public GameObject[] char_buttons = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        int stage = PlayerPrefs.GetInt("User_stage");
        Debug.Log("user_stage:" + stage);

        for (int i = 1; i <= 20; i++)
        {
            if (i == stage)
                rabbits[i].SetActive(true);
            else
                rabbits[i].SetActive(false);
        }
        for (int i = 1; i <= 20; i++)
        {
            if (i <= stage + 1)
                buttons[i].GetComponent<Button>().interactable = true;
            else
                buttons[i].GetComponent<Button>().interactable = false;
        }
        for (int i = 2; i <= 20; i++)
        {
            if (i <= stage + 1)
                locks[i].SetActive(false);
            else
                locks[i].SetActive(true);
        }
        

        for(int i = 0; i < 4; i++)
        {
            if (i <= stage / 5)
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
        
        if (Manager.clear == true)
        {
            rabbits[stage].GetComponent<Animator>().SetTrigger("jump");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
