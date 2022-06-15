using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Sttings
{
    //사진 바꿀 때 마다 사진 표현하는 이미지 변경하는 기능 
    public Sprite sprite;


}

public class ImageChange : MonoBehaviour
{
    [SerializeField] Sttings[] sttings = null;
    [SerializeField] Image img = null;

    int currentImg = 0;


    public void BtnNext()
    {
        if (++currentImg > sttings.Length - 1) currentImg = 0;

        SettingImg();
    }

    public void BtnPrior()
    {
        if (--currentImg < 0)
            currentImg = sttings.Length - 1;

        SettingImg();
    }

    void SettingImg()
    {
        img.sprite = sttings[currentImg].sprite;
    }

    public void Skip()
    {
        SceneManager.LoadScene("01-1.Daily");
    }
}
