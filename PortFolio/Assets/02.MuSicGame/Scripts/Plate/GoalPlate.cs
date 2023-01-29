using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlate : MonoBehaviour
{
    AudioSource theAudio;
    NoteManager theNote;

    Result theResult;
    
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        theNote = FindObjectOfType<NoteManager>();
        theResult = FindObjectOfType<Result>();
    }


    //Player에 콜라이더가 감지되면 빵빠레가 울리게 해주는 기능 /22.03.24 by승주
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            theAudio.Play();
            PlayerController.s_canPressKey = false;
            theNote.RemoveNote();

            theResult.ShowResult();

            //보상 얻기 위한 기능 22.06.07 승주
            theResult.boSang.SetActive(true);
            theResult.BtnClear();
        }
       
        
    }

   

}
