using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGameCameraContral : MonoBehaviour
{
    [Header("Camera")]
    public Transform camAxis_contral;
    public Transform Maincam;
    public float camSpeed;
   [HideInInspector] public float mouseX;
    float mouseY;
    float wheel;

    void Start()
    {
        wheel = -5;
        mouseY = 2;
    }

    void CamMove()
    {

        mouseX += Input.GetAxis("Mouse X");

        //���� ������ ���ִ� ��� 22.04.22 by����
        mouseY += Input.GetAxis("Mouse Y") * -1;

        //Camera�� ���� �ʰ� ���Ʒ��� ���� �Ŵ� ��� 22.04.22 by����
        if (mouseY > 10) mouseY = 10;
        if (mouseY < 0) mouseY = 0;

        //�߽� �� ȸ�� ��Ű�� ��� 22.04.22 by����
        camAxis_contral.rotation = Quaternion.Euler
            (new Vector3(camAxis_contral.rotation.x + mouseY,
                                     camAxis_contral.rotation.y + mouseX, 0) * camSpeed
            );
    }

    void Zoom()
    {
        //wheel�� Mouse wheel �� �߰� ��� 22.04.22 by����
        wheel += Input.GetAxis("Mouse ScrollWheel") * 10;

        //zoomIn zoomOut ���� ���� ��� 22.04.22 by����
        if (wheel >= -5) wheel = -5;
        if (wheel <= -10) wheel = -10;

        //camera Z���� wheel �� �����ϴ� ��� 22.04.22 by����
        Maincam.localPosition = new Vector3(0, 0, wheel);
    }

    
    void Update()
    {
        CamMove();
        Zoom();
    }
}
