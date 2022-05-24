
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
        SceneManager.LoadScene(7);
    }

    public void MainMenu()
    {
        gameReset.SetActive(true);
        SceneManager.LoadScene(2);
    }
}
