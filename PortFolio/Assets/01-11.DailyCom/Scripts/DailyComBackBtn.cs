using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DailyComBackBtn : MonoBehaviour
{
   
    public void BackBtn()
    {
        SceneManager.LoadScene("01-1.Daily");
    }
}
