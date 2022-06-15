using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CardGameResultPanel : MonoBehaviour
{

    [SerializeField] TMP_Text resultTMP;

    CardGameEntity cardGameEntity;

    public GameObject boSang;


    public void Show(string message)
    {
        resultTMP.text = message;
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutQuad);
    }

    

    public void Restart()
    {
        SceneManager.LoadScene("02.CardGame");
    }

    public void MainMene()
    {
        SceneManager.LoadScene("01-11.DailyCom");
    }

    public void BtnClear()
    {
        boSang.SetActive(true);
        if (gameObject == true)
        {
            boSang.SetActive(true);

        }
        else if (gameObject == false)
        {
            boSang.SetActive(false);
        }


    }

    public void OnClickLoadGame()
    {
        SceneManager.LoadScene("01-1.Daily");
        ClearManager.stageClear[1] = true;


        BtnClear();
    }



    void Start() => ScaleZero();

    [ContextMenu("ScaleOne")]
    void ScaleOne() => transform.localScale = Vector3.one;

    [ContextMenu("ScaleZero")]
    public void ScaleZero() => transform.localScale = Vector3.zero;

}
