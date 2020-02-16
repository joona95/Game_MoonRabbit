using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    public GameObject gameobject;
    Scrollbar bar;

    public IEnumerator Start()
    {
        Debug.Log("Now its called");
        yield return null; // Waiting just one frame is probably good enough, yield return null does that
        bar = GetComponentInChildren<Scrollbar>();
        int stage = PlayerPrefs.GetInt("User_stage");
        float _value;
        if (stage > 0)
        {
            _value = (gameobject.GetComponent<Map_Lock>().rabbits[stage].GetComponent<RectTransform>().anchoredPosition.y) / 4000 - 0.2f;
            if (_value < 0)
            {
                _value = 0f;
            }
        }
        else
            _value = 0f;

        

        bar.value = _value;
        Debug.Log(_value);
        Debug.Log("Now its setted");
    }

    /*
    public GameObject gameobject;
    // Start is called before the first frame update
    void Start()
    {
        ScrollRect scroll = GetComponent<ScrollRect>();
        float value = 1; //+ (gameobject.GetComponent<Map_Lock>().rabbits[PlayerPrefs.GetInt("User_stage")].GetComponent<RectTransform>().anchoredPosition.y) / 6495;
        scroll.verticalScrollbar.value = value;
        //Debug.Log(value);
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
