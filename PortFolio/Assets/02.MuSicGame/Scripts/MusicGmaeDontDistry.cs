using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGmaeDontDistry : MonoBehaviour
{

    public GameObject bbSang;

    Result result;

    void BoSang()
    {
        GoalPlate golPlate;

        golPlate = GetComponent<GoalPlate>();
        result = GetComponent<Result>();

        
        //bbSang.SetActive(true);
        //DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bbSang.SetActive(false);
            
            
        }
        else if(other.CompareTag("Player"))
        {
            result.BtnClear();
            DontDestroyOnLoad(gameObject);
        }
    }


}
