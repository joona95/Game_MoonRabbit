using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public GameObject opacity, confirm, hat, confetti;
    public GameObject[] hat_items = new GameObject[3];
    public GameObject item_rowbomb, item_sixbomb, item_rainbow;

    public void Item1()
    {
        Shooter.possible = false;
        GameObject tmp = GetComponent<Manager>().ballPrefabs[0];
        Destroy(tmp);
        GetComponent<Manager>().ballPrefabs[0] = (GameObject)Instantiate(item_rowbomb, new Vector3(0f, -3.5f, 0f), Quaternion.identity);
        GetComponent<Manager>().ballPrefabs[0].GetComponent<Ball>().rowbomb = true;
        GetComponent<Manager>().ballPrefabs[0].GetComponent<Ball>().tag = "Untagged";
        Shooter.possible = true;
    }

    public void Item2()
    {
        Shooter.possible = false;
        GameObject tmp = GetComponent<Manager>().ballPrefabs[0];
        Destroy(tmp);
        GetComponent<Manager>().ballPrefabs[0] = (GameObject)Instantiate(item_sixbomb, new Vector3(0f, -3.5f, 0f), Quaternion.identity);
        GetComponent<Manager>().ballPrefabs[0].GetComponent<Ball>().sixbomb = true;
        GetComponent<Manager>().ballPrefabs[0].GetComponent<Ball>().tag = "Untagged";
        Shooter.possible = true;
    }

    public void Item3()
    {
        Shooter.possible = false;
        GameObject tmp = GetComponent<Manager>().ballPrefabs[0];
        Destroy(tmp);
        GetComponent<Manager>().ballPrefabs[0] = (GameObject)Instantiate(item_rainbow, new Vector3(0f, -3.5f, 0f), Quaternion.identity);
        GetComponent<Manager>().ballPrefabs[0].GetComponent<Ball>().rainbow = true;
        GetComponent<Manager>().ballPrefabs[0].GetComponent<Ball>().tag = "Untagged";
        Shooter.possible = true;
    }


    public void randomItem()
    {
        opacity.SetActive(false);
        hat.SetActive(false);
        confetti.SetActive(false);
        if (hat_items[0].activeSelf == true)
        {
            hat_items[0].SetActive(false);
        }
        if (hat_items[1].activeSelf == true)
        {
            hat_items[1].SetActive(false);
        }
        if (hat_items[2].activeSelf == true)
        {
            hat_items[2].SetActive(false);
        }
        confirm.SetActive(false);
        GameObject.Find("대포").GetComponent<Shooter>().enabled = true;
    }
}
