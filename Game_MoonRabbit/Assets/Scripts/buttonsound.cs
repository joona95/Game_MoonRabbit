using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonsound : MonoBehaviour
{
    semmanager sem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void buttoonsound()
    {
        sem = FindObjectOfType<semmanager>();
        sem.play(10);

    }
}
