using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class AdmobScreenAd : MonoBehaviour
{
    private readonly string unitID = "ca-app-pub-8256776937865205/3481656575"; //달토끼 진짜 ID
    private readonly string testID = "ca-app-pub-3940256099942544/1033173712"; //테스트용 ID

    private InterstitialAd screenAd;
    
    // Start is called before the first frame update
    private void Start()
    {

    }

    public void PlayAd()
    {
        InitAd();

        if(((Manager.current_stage % 2 ) == 0)&&(Manager.current_stage!=40))
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
        string id = testID; //출시할 때 unitID로 바꿔야 함

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
