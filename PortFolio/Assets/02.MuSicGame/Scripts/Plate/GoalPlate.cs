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


    //Player�� �ݶ��̴��� �����Ǹ� �������� �︮�� ���ִ� ��� /22.03.24 by����
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            theAudio.Play();
            PlayerController.s_canPressKey = false;
            theNote.RemoveNote();

            theResult.ShowResult();

            //���� ��� ���� ��� 22.06.07 ����
            theResult.boSang.SetActive(true);
            theResult.BtnClear();
        }
       
        
    }

   

}
