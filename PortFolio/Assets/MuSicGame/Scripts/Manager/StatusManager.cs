using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatusManager : MonoBehaviour
{
    //HP 22.03.26 by╫баж
    int maxHp = 3;
    int current = 3;

    //Shield 22.03.26 by╫баж
    int maxShield = 3;
    int currentShield = 0;

    [SerializeField] GameObject[] hpObject = null;
    [SerializeField] GameObject[] shield = null;


    void Start()
    {

    }


    void Update()
    {

    }
}
