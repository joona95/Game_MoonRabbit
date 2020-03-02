﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class AdmobScreenAd : MonoBehaviour
{
    private readonly string unitID = "ca-app-pub-8256776937865205/3481656575";

    private InterstitialAd screenAd;
    
    // Start is called before the first frame update
    private void Start()
    {

    }

    public void PlayAd()
    {
        InitAd();

        if((Manager.current_stage % 2 ) == 0)
        {
            Show();
            screenAd.OnAdClosed += (sender, e) => GameObject.Find("Main Camera").GetComponent<ChangeScene>().BackToMapButton();
            Debug.Log("is 2");
            
        }

        else
        {
            GameObject.Find("Main Camera").GetComponent<ChangeScene>().BackToMapButton();
            Debug.Log("not 2");
        }
    }

    private void InitAd()
    {
        string id = unitID;

        screenAd = new InterstitialAd(id);

        AdRequest request = new AdRequest.Builder().Build();

        screenAd.LoadAd(request);
        
    }

    public void Show()
    {
        StartCoroutine("ShowScreenAd");
    }

    private IEnumerator ShowScreenAd()
    {
        while (!screenAd.IsLoaded())
        {
            yield return null;
        }

        screenAd.Show();
    }
}
