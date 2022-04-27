using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGamePlayerContralor : MonoBehaviour
{

    [Header("Player")]
    public Transform playerAxis;
    public Transform player;
    public float playerSpeed;

    

    //player �̵����� ��� 22.04.26 by����
    public Vector3 movement;

    BossGameCamContrallor bossGameCamContrallor;

    public Animator animator;
    

    void Start()
    {
        bossGameCamContrallor = FindObjectOfType<BossGameCamContrallor>();
        //animator = GetComponent<Animator>();
    }

    public void PlayerMove()
    {
        
        //if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || !animator.GetCurrentAnimatorStateInfo(0).IsName("Knight_Running")) return;

        //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
        //Ű���忡 ����Ű�� ���� �̵����� �������� ��� 22.04.26 by����
        movement = new Vector3(Input.GetAxis("Horizontal"), 0,
                                           Input.GetAxis("Vertical"));
        

        //�̵� ������ �ִٸ�
        if (movement != Vector3.zero)
        {
            //player�� Y�߽� ���� camrea Y�߽ɰ� �Բ� ȸ�� ��Ű�� ��� 22.04.26 by����
            playerAxis.rotation = Quaternion.Euler(new Vector3(0,
               bossGameCamContrallor.camAxis_central.rotation.y + bossGameCamContrallor.mouseX, 0) * bossGameCamContrallor.camSpeed);

            //player �߽����� movement�������� playerSpeed��ŭ �̵� ��Ű�� ��� 22.04.26 by���� 
            playerAxis.Translate(movement * Time.deltaTime * playerSpeed);

            //player�� movement�������� ȸ�� ��Ű�� ȸ�� �ӵ��� �����ϴ� ��� 22.04.26 by����
            player.localRotation = Quaternion.Slerp(player.localRotation,
                                        Quaternion.LookRotation(movement), 10 * Time.deltaTime);

            player.GetComponent<Animator>().SetBool("Walk", true);
        }
        if(movement==Vector3.zero)
            player.GetComponent<Animator>().SetBool("Walk", false);
            


        //camera �߽����� player�� ����ٴϰ� �ϴ� ��� 22.04.26 by����
        bossGameCamContrallor.camAxis_central.position = new Vector3(player.position.x,
                                                                                             player.position.y + 5,
                                                                                             player.position.z);
    }

    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Knight_Running"))
        {
            PlayerMove();
        }
        ///PlayerMove();
       
    }
    
}
