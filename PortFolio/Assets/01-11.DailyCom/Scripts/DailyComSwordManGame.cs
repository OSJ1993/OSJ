using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DailyComSwordManGame : MonoBehaviour
{
    public  GameObject[] game;
    public void SeeneChange()
    {
        SceneManager.LoadScene(7);
    }

    private void Update()
    {
        for (int i = 0; i < game.Length; i++)
        {
            game[i] = Instantiate(gameObject);
        }
    }
}
