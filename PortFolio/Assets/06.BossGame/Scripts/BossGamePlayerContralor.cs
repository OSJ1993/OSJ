using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGamePlayerContralor : MonoBehaviour
{

    [Header("Player")]
    public Transform playerAxis;
    public Transform player;
    public float playerSpeed;

<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes

    //player �̵����� ��� 22.04.26 by����
    public Vector3 movement;

    
    

   // BossGameCamContrallor bossGameCamContrallor;

    public Animator animator;

<<<<<<< Updated upstream
   
=======

>>>>>>> Stashed changes

    void Start()
    {
       // bossGameCamContrallor = FindObjectOfType<BossGameCamContrallor>();


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
            //playerAxis.rotation = Quaternion.Euler(new Vector3(0,
             //  bossGameCamContrallor.camAxis_central.rotation.y + bossGameCamContrallor.mouseX, 0) * bossGameCamContrallor.camSpeed);

            //player �߽����� movement�������� playerSpeed��ŭ �̵� ��Ű�� ��� 22.04.26 by���� 
            //playerAxis.Translate(movement * Time.deltaTime * playerSpeed);

            //player�� movement�������� ȸ�� ��Ű�� ȸ�� �ӵ��� �����ϴ� ��� 22.04.26 by����
           // player.localRotation = Quaternion.Slerp(player.localRotation,
           //                             Quaternion.LookRotation(movement), 10 * Time.deltaTime);

           // player.GetComponent<Animator>().SetBool("Walk", true);
        }
        if (movement == Vector3.zero)
            player.GetComponent<Animator>().SetBool("Walk", false);



        //camera �߽����� player�� ����ٴϰ� �ϴ� ��� 22.04.26 by����
        //bossGameCamContrallor.camAxis_central.position = new Vector3(player.position.x,
        //                                                                                     player.position.y + 5,
        //                                                                                     player.position.z);


    }

<<<<<<< Updated upstream
    

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || 
            animator.GetCurrentAnimatorStateInfo(0).IsName("Knight_Running") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("NomalAtk1") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("NomalAtk2") ||
=======

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Knight_Running") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("NomalAtk1") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("NomalAtk2") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("NomalAtk2") ||
>>>>>>> Stashed changes
            animator.GetCurrentAnimatorStateInfo(0).IsName("NomalAtk3") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("SmashAtk1") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("SmashAtk2") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("SmashAtk3") ||
<<<<<<< Updated upstream
            animator.GetCurrentAnimatorStateInfo(0).IsName("Rigidity") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Knigth_Counter") 
=======
            animator.GetCurrentAnimatorStateInfo(0).IsName("Defense") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Rigidity") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Knigth_Counter")

>>>>>>> Stashed changes
            )
        {
            
        }

<<<<<<< Updated upstream
       
=======
>>>>>>> Stashed changes

        ///PlayerMove();

    }

}
