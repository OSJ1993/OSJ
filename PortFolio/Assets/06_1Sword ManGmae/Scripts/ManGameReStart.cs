
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManGameReStart : MonoBehaviour
{
    public GameObject gameReset;

   
    public void ReStart()
    {
        gameReset.SetActive(true);
        SceneManager.LoadScene("04Sword ManGmae");
    }

    public void MainMenu()
    {
        gameReset.SetActive(true);
        SceneManager.LoadScene("01-11.DailyCom");
    }

    public void YouWin()
    {
        gameReset.SetActive(true);
        SceneManager.LoadScene("01-1.Daily");

        ClearManager.stageClear[3] = true;
    }
}
