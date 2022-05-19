using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CardGameResultPanel : MonoBehaviour
{

    [SerializeField] TMP_Text resultTMP;


    public void Show(string message)
    {
        resultTMP.text = message;
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutQuad);
    }

    public void Restart()
    {
        SceneManager.LoadScene(5);
    }

    public void MainMene()
    {
        SceneManager.LoadScene(2);
    }

    void Start() => ScaleZero();

    [ContextMenu("ScaleOne")]
    void ScaleOne() => transform.localScale = Vector3.one;

    [ContextMenu("ScaleZero")]
    public void ScaleZero() => transform.localScale = Vector3.zero;

}
