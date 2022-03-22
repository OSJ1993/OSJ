using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NoteManager : MonoBehaviour
{

    public int bpm = 0;

    //노트 생성을 위한 그 시간 체크해줄 변수.
    double curentTime = 0d;

    //노트가 생성 될 위치 변수.
    [SerializeField] Transform tfNoteAppear = null;


    //TimingManager theTimingManager 참조할 수 있게 만들어 주기.
    TimingManager theTimingManager;
    EffectManager theEffectManager;
    ComboManager theComboManager;
    void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        theComboManager = FindObjectOfType<ComboManager>();
        theTimingManager = GetComponent<TimingManager>();
    }



    void Update()
    {
        //curentTime을 1초에 1씩 증가
        curentTime += Time.deltaTime;

        //그러다가 curentTime이 60s초 나누기 bpm보다 커지면 비트 1개당 등장속도.
        if (curentTime >= 60d / bpm)
        {
            GameObject t_note = ObjectPool.instnace.noteQueue.Dequeue();
            t_note.transform.position = tfNoteAppear.position;
            t_note.SetActive(true);



            //노트가 생성되는 순간 노트List에 해당 노트를 추가.
            theTimingManager.boxNoteList.Add(t_note);

            //curentTime에 0이 아닌 60d/bpm을 빼주기.(0으로 하면 안되는 이유는 아주 작은 오차가 생기기 때문이다.
            curentTime -= 60d / bpm;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        //콜라이더 내에 들어오거나 나가면 발동하는 함수.
        //Note 테그로 된 콜라이더가 감지되면 그 객체를 파괴.
        if (collision.CompareTag("Note"))
        {
            //Note에 있는 public bool GetNoteFlag()에 enabled true라 Image가 닿으면 그 때만  theEffectManager.JudgementEffect(4);실행.
            //부딛힌 객체의 노트 스크립을 가져와서 GetNoteFlag 함수 호출 true일 때만 연출실행.
            if (collision.GetComponent<Note>().GetNoteFlag())
            {

                //노트가 화면 밖으로 나가면 그 구간에 숫자 4(miss)넘겨서 연출되게 하기.
                theEffectManager.JudgementEffect(4);

                //놓쳐서 미스뜨는 구간에 콤보초기화 넣어주기.
                theComboManager.ResetCombo();

            }


            //노트가 파괴되는 순간에도 해당 노트를 List에서 제거.
            theTimingManager.boxNoteList.Remove(collision.gameObject);

            //노트 이미지 생각해보기. noteImage.enabled가 false로 된 상태.


            //노트 Queue에 반납시켜주고 비활성화 상태.
            ObjectPool.instnace.noteQueue.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);




        }
    }
}
