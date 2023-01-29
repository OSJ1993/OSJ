using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DailyComCardGame : MonoBehaviour
{
    public void SeeneChange()
    {
        SceneManager.LoadScene("02.CardGame");
    }
}
