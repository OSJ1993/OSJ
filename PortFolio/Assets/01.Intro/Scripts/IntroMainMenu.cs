using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroMainMenu : MonoBehaviour
{
    


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    //판넬 오브젝트 기능 22.05.13 승주
    GameObject SplashObj;

    //판넬 이미지 기능 22.05.13 승주
    Image image;

    void Awake()
    {
        SplashObj = this.gameObject;
        image = SplashObj.GetComponent<Image>();
    }




}
