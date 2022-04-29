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

    //player 이동방향 기능 22.04.26 by승주
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
        //키보드에 방향키에 따라 이동방향 정해지는 기능 22.04.26 by승주
        movement = new Vector3(Input.GetAxis("Horizontal"), 0,
                                           Input.GetAxis("Vertical"));


        //이동 방향이 있다면
        if (movement != Vector3.zero)
        {
            //player의 Y중심 값을 camrea Y중심과 함께 회전 시키는 기능 22.04.26 by승주
            //playerAxis.rotation = Quaternion.Euler(new Vector3(0,
             //  bossGameCamContrallor.camAxis_central.rotation.y + bossGameCamContrallor.mouseX, 0) * bossGameCamContrallor.camSpeed);

            //player 중심축을 movement방향으로 playerSpeed만큼 이동 시키는 기능 22.04.26 by승주 
            //playerAxis.Translate(movement * Time.deltaTime * playerSpeed);

            //player를 movement방향으로 회전 시키고 회전 속도를 지정하는 기능 22.04.26 by승주
           // player.localRotation = Quaternion.Slerp(player.localRotation,
           //                             Quaternion.LookRotation(movement), 10 * Time.deltaTime);

           // player.GetComponent<Animator>().SetBool("Walk", true);
        }
        if (movement == Vector3.zero)
            player.GetComponent<Animator>().SetBool("Walk", false);



        //camera 중심축이 player를 따라다니게 하는 기능 22.04.26 by승주
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
