using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hatsound : MonoBehaviour
{
    semmanager sem;
    int i = 1;
    // Start is called before the first frame update
    void Start()
    {
        sem = FindObjectOfType<semmanager>();
        if (this.gameObject.activeSelf == true)
        {
            StartCoroutine("hat");
        }
    }
    IEnumerator hat()
    {
        sem = FindObjectOfType<semmanager>();
        /*while (i==1)
        {
            i++;
            yield return new WaitForSeconds(1.5f);
            sem.play(9);
        }*/
        yield return new WaitForSeconds(1.5f);
        sem.play(9);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
