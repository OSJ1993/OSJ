using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGamePlayerContralor : MonoBehaviour
{

    [Header("Player")]
    public Transform playerAxis;
    public Transform player;
    public float playerSpeed;

    

    //player 이동방향 기능 22.04.26 by승주
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
        //키보드에 방향키에 따라 이동방향 정해지는 기능 22.04.26 by승주
        movement = new Vector3(Input.GetAxis("Horizontal"), 0,
                                           Input.GetAxis("Vertical"));
        

        //이동 방향이 있다면
        if (movement != Vector3.zero)
        {
            //player의 Y중심 값을 camrea Y중심과 함께 회전 시키는 기능 22.04.26 by승주
            playerAxis.rotation = Quaternion.Euler(new Vector3(0,
               bossGameCamContrallor.camAxis_central.rotation.y + bossGameCamContrallor.mouseX, 0) * bossGameCamContrallor.camSpeed);

            //player 중심축을 movement방향으로 playerSpeed만큼 이동 시키는 기능 22.04.26 by승주 
            playerAxis.Translate(movement * Time.deltaTime * playerSpeed);

            //player를 movement방향으로 회전 시키고 회전 속도를 지정하는 기능 22.04.26 by승주
            player.localRotation = Quaternion.Slerp(player.localRotation,
                                        Quaternion.LookRotation(movement), 10 * Time.deltaTime);

            player.GetComponent<Animator>().SetBool("Walk", true);
        }
        if(movement==Vector3.zero)
            player.GetComponent<Animator>().SetBool("Walk", false);
            


        //camera 중심축이 player를 따라다니게 하는 기능 22.04.26 by승주
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
