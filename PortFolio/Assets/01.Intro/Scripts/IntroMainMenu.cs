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

    //�ǳ� ������Ʈ ��� 22.05.13 ����
    GameObject SplashObj;

    //�ǳ� �̹��� ��� 22.05.13 ����
    Image image;

    void Awake()
    {
        SplashObj = this.gameObject;
        image = SplashObj.GetComponent<Image>();
    }




}
