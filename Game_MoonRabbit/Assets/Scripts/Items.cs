using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public GameObject opacity, confirm, hat, confetti;
    public GameObject[] hat_items = new GameObject[3];
    public GameObject item_rowbomb, item_sixbomb, item_rainbow;
    public GameObject InfoQ, InfoPnM, InfoStone, InfoBomb, checkinfobutton;

    public void Start()
    {
        if (!PlayerPrefs.HasKey("Item1_Cnt"))
        {
            PlayerPrefs.SetInt("Item1_Cnt", 0);
        }

        if (!PlayerPrefs.HasKey("Item2_Cnt"))
        {
            PlayerPrefs.SetInt("Item2_Cnt", 0);
        }

        if (!PlayerPrefs.HasKey("Item3_Cnt"))
        {
            PlayerPrefs.SetInt("Item3_Cnt", 0);
        }
    }

    public void Item1()
    {
        GameObject.Find("대포").GetComponent<Shooter>().enabled = false;
        int cnt = PlayerPrefs.GetInt("Item1_Cnt");

        if (cnt > 0)
        {
            Shooter.possible = false;
            GameObject tmp = GetComponent<Manager>().ballPrefabs[0];
            Destroy(tmp);
            GetComponent<Manager>().ballPrefabs[0] = (GameObject)Instantiate(item_rowbomb, new Vector3(0f, -3.5f, 0f), Quaternion.identity);
            GetComponent<Manager>().ballPrefabs[0].GetComponent<Ball>().rowbomb = true;
            GetComponent<Manager>().ballPrefabs[0].GetComponent<Ball>().tag = "Untagged";
            PlayerPrefs.SetInt("Item1_Cnt", cnt - 1);

            Shooter.possible = true;
        }
        GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
    }

    public void Item2()
    {
        GameObject.Find("대포").GetComponent<Shooter>().enabled = false;
        int cnt = PlayerPrefs.GetInt("Item2_Cnt");
        if (cnt> 0)
        {
            Shooter.possible = false;
            GameObject tmp = GetComponent<Manager>().ballPrefabs[0];
            Destroy(tmp);
            GetComponent<Manager>().ballPrefabs[0] = (GameObject)Instantiate(item_sixbomb, new Vector3(0f, -3.5f, 0f), Quaternion.identity);
            GetComponent<Manager>().ballPrefabs[0].GetComponent<Ball>().sixbomb = true;
            GetComponent<Manager>().ballPrefabs[0].GetComponent<Ball>().tag = "Untagged";
            PlayerPrefs.SetInt("Item2_Cnt", cnt - 1);

            Shooter.possible = true;
        }
        GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
    }

    public void Item3()
    {
        GameObject.Find("대포").GetComponent<Shooter>().enabled = false;
        int cnt = PlayerPrefs.GetInt("Item3_Cnt");
        if ( cnt> 0)
        {
            Shooter.possible = false;
            GameObject tmp = GetComponent<Manager>().ballPrefabs[0];
            Destroy(tmp);
            GetComponent<Manager>().ballPrefabs[0] = (GameObject)Instantiate(item_rainbow, new Vector3(0f, -3.5f, 0f), Quaternion.identity);
            GetComponent<Manager>().ballPrefabs[0].GetComponent<Ball>().rainbow = true;
            GetComponent<Manager>().ballPrefabs[0].GetComponent<Ball>().tag = "Untagged";
            PlayerPrefs.SetInt("Item3_Cnt", cnt - 1);

            Shooter.possible = true;
        }
        GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
    }


    public void randomItem()
    {
        hat.SetActive(false);
        confetti.SetActive(false);
        if (hat_items[0].activeSelf == true)
        {
            hat_items[0].SetActive(false);
            int cnt = PlayerPrefs.GetInt("Item1_Cnt");
            PlayerPrefs.SetInt("Item1_Cnt", cnt + 1);
        }
        if (hat_items[1].activeSelf == true)
        {
            hat_items[1].SetActive(false);
            int cnt = PlayerPrefs.GetInt("Item2_Cnt");
            PlayerPrefs.SetInt("Item2_Cnt", cnt + 1);
        }
        if (hat_items[2].activeSelf == true)
        {
            hat_items[2].SetActive(false);
            int cnt = PlayerPrefs.GetInt("Item3_Cnt");
            PlayerPrefs.SetInt("Item3_Cnt", cnt + 1);
        }
        confirm.SetActive(false);

        switch (Manager.current_stage)
        {
            case 1:
                InfoQ.SetActive(true);
                checkinfobutton.SetActive(true);
                break;
            case 11:
                InfoStone.SetActive(true);
                checkinfobutton.SetActive(true);
                break;
            case 15:
                InfoPnM.SetActive(true);
                checkinfobutton.SetActive(true);
                break;
            case 20:
                InfoBomb.SetActive(true);
                checkinfobutton.SetActive(true);
                break;
            default:
                opacity.SetActive(false);               
                GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
                break;
        }

    }
}
