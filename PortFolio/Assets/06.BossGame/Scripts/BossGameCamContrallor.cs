using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGameCamContrallor : MonoBehaviour
{
    [Header("Camera")]
    public Transform camAxis_central;
    public Transform cam;
    public float camSpeed;
    [HideInInspector] public float mouseX;
    [HideInInspector] public float mouseY;
    [HideInInspector] public float wheel;



    

    void Start()
    {
        //game�� ���� �� �� ī�޸� ��ġ ���� ��� 22.04.26 by����
        wheel = -10;
        mouseY = 5;

        
        
    }

     

    void CamMove()
    {

        mouseX += Input.GetAxis("Mouse X");

        //mouseY ���� ������ �ϴ� ��� 22.04.26 by����  
        mouseY += Input.GetAxis("Mouse Y") * -1;

        //camera�� ���� ���� �ʰ� �ϴ� ��� 22.04.26 by����
        if (mouseY > 20) mouseY = 20;
        if (mouseY < -5) mouseY = -5;

        //�߽��� ȸ�� ��� 22.04.26 by����
        camAxis_central.rotation = Quaternion.Euler(new Vector3(
                                         camAxis_central.rotation.x + mouseY,
                                         camAxis_central.rotation.y + mouseX, 0) * camSpeed);
    }

    void Zoom()
    {
        wheel += Input.GetAxis("Mouse ScrollWheel") * 10;

        //zoomIn, zoomOut ���� ���� ��� 22.04.26 by����
        if (wheel >= -10) wheel = -10;
        if (wheel <= -20) wheel = -20;

        //camera�� wheel �� ���� ��Ų ��� 22.04.26 by����
        cam.localPosition = new Vector3(0, 0, wheel);
    }

   

    void Update()
    {
        CamMove();
        Zoom();
    }

   
}
