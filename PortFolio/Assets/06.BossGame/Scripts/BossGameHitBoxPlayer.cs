using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossGameHitBoxPlayer : MonoBehaviour
{
    public Animator playerAni;
    public TextMeshProUGUI message;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BossGame_Col_EnemyAtk")
        {
           
            if(gameObject.tag== "BossGameHitBoxPlayer")
            {
                playerAni.Play("Rigidity");
                message.text = "Rigidity";
                message.gameObject.SetActive(true);
            }

            if (gameObject.tag == "BossGameDefense")
            {
                message.text = "Block";
                message.gameObject.SetActive(true);
            }

            if (gameObject.tag == "BossGameParrying")
            {
                playerAni.Play("Knigth_Counter");
                message.text = "Parrying";
                message.gameObject.SetActive(true);
            }
        }
    }
}
