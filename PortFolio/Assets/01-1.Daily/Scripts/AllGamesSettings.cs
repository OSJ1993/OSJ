using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class AllGamesSettings : MonoBehaviour
{

    public GameObject settings;

    private Touch tempTouchs;
    private Vector3 touchedPos;
    private bool touchOn;

    void Awake()
    {

    }

    public void SettingIN()
    {
        settings.SetActive(true);
    }

    public void SettingOut()
    {
       

        
                settings.SetActive(false);
    }



}
