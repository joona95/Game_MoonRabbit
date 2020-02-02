using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public Sprite item_rowbomb, item_sixbomb, item_rainbow;

    public void Item1()
    {
        Shooter.possible = false;

        GetComponent<Manager>().ballPrefabs[0].GetComponent<SpriteRenderer>().sprite = item_rowbomb;
        GetComponent<Manager>().ballPrefabs[0].GetComponent<Ball>().rowbomb = true;
        GetComponent<Manager>().ballPrefabs[0].GetComponent<Ball>().tag = "Untagged";
        Shooter.possible = true;
    }

    public void Item2()
    {
        Shooter.possible = false;

        GetComponent<Manager>().ballPrefabs[0].GetComponent<SpriteRenderer>().sprite = item_sixbomb;
        GetComponent<Manager>().ballPrefabs[0].GetComponent<Ball>().sixbomb = true;
        GetComponent<Manager>().ballPrefabs[0].GetComponent<Ball>().tag = "Untagged";
        Shooter.possible = true;
    }

    public void Item3()
    {
        Shooter.possible = false;

        GetComponent<Manager>().ballPrefabs[0].GetComponent<SpriteRenderer>().sprite = item_rainbow;
        GetComponent<Manager>().ballPrefabs[0].GetComponent<Ball>().rainbow = true;
        GetComponent<Manager>().ballPrefabs[0].GetComponent<Ball>().tag = "Untagged";
        Shooter.possible = true;
    }
}
