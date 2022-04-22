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
        //keyboard ����Ű�� ���� �̵� ���� �������� ��� 22.04.22 by����
        movement = new Vector3(Input.GetAxis("Horizontal"), 0,
                                                       Input.GetAxis("Vertical"));

        if (movement != Vector3.zero)
        {
            //player�� �������� �ִٸ� camera�� ���� �����̰� �ϴ� ��� 22.04.22 by����
            playerAxis.rotation = Quaternion.Euler(new Vector3(0, bossGameCameraContral.camAxis_contral.rotation.y
                                                                                                         + bossGameCameraContral.mouseX, 0)
                                                                                                         * bossGameCameraContral.camSpeed);

            playerAxis.Translate(movement * Time.deltaTime * playerSpeed);

            //player�� ������ movement�������� �̵���Ű�� ��� 22.04.22 by����
            player.localRotation = Quaternion.Slerp(player.localRotation
                                                 , Quaternion.LookRotation(movement), 5 * Time.deltaTime);
        }

        bossGameCameraContral.camAxis_contral.position = player.position;
    }


    void Update()
    {
        PlayerMove();
    }
}
