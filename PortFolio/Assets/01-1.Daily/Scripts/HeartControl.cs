using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartControl : MonoBehaviour
{
    public GameObject[] heartPiece;

    private bool[] checkBoolean = { true, true, true, true };

    private bool clearState = false;
    void Start()
    {
        HeartController();

        if (ClearManager.stageClear.SequenceEqual(checkBoolean) && !clearState)
        {
            clearState = true;
            StartCoroutine(GameClear());
        }


    }

    public void HeartController()
    {
        for (int i = 0; i < heartPiece.Length; i++)
        {
            heartPiece[i].SetActive(ClearManager.stageClear[i]);

            
            
        }

               
        
    }

    IEnumerator GameClear()
    {
        
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(7);


        for(int i=0; i == heartPiece.Length;)
        {
            heartPiece[i].SetActive(ClearManager.stageClear[i] = false);
           
}


    }
}
