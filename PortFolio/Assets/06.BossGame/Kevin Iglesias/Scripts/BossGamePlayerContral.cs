using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGamePlayerContral : MonoBehaviour
{
    [Header("Player")]
    public Transform playerAxis;
    public Transform player;
    public float playerSpeed;
    public Vector3 movement;

    [SerializeField] [HideInInspector]  public BossGameCameraContral bossGameCameraContral;

    void Start()
    {
        bossGameCameraContral = FindObjectOfType<BossGameCameraContral>();
    }

    void PlayerMove()
    {
        
        //keyboard 방향키에 따라 이동 방향 정해지는 기능 22.04.22 by승주
        movement = new Vector3(Input.GetAxis("Horizontal"), 0,
                                                       Input.GetAxis("Vertical"));

        if (movement != Vector3.zero)
        {
            
            //player의 움직임이 있다면 camera도 같이 움직이게 하는 기능 22.04.22 by승주
            playerAxis.rotation = Quaternion.Euler(new Vector3(0, bossGameCameraContral.camAxis_contral.rotation.y
                                                                                                         + bossGameCameraContral.mouseX, 0)
                                                                                                         * bossGameCameraContral.camSpeed);

            playerAxis.Translate(movement * Time.deltaTime * playerSpeed);

            //player의 방향을 movement방향으로 이동시키는 기능 22.04.22 by승주
            player.localRotation = Quaternion.Slerp(player.localRotation
                                                 , Quaternion.LookRotation(movement), 5 * Time.deltaTime);
        }

        //camera가 player의 가슴 축 쯤을 따라가게 하는 기능 22.04.23 by승주
        bossGameCameraContral.camAxis_contral.position = new Vector3(player.position.x
                                                                                                                            ,player.position.y+0.5f
                                                                                                                            ,player.position.z);
        
    }


    void Update()
    {
        PlayerMove();
    }
}
