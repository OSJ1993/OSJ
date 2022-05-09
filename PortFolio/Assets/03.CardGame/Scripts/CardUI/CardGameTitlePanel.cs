using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameTitlePanel : MonoBehaviour
{
   public void StartGameClick()
    {
        CardGameManager.Inst.StartGame();
        Active(false);
    }

    public void Active(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
