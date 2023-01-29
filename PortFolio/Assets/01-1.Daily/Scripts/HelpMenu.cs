using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class HelpMenu : MonoBehaviour
{

    public void BtnMusicGame() { SceneManager.LoadScene("06.MusicGameSetting"); }

   

    public void BtnCardGame() { SceneManager.LoadScene("07.CardGameSetting"); }

    public void BtnScrollGame(){ SceneManager.LoadScene("08.ScrollGameSetting"); }

    public void BtnSwordGame(){ SceneManager.LoadScene("09.SwordGameSetting"); }
    public void BtnDaily(){ SceneManager.LoadScene("10.DailyGameSetting"); }


}
