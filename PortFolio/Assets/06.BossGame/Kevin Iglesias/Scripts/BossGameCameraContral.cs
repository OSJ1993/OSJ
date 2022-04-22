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

        //상하 반전을 해주는 기능 22.04.22 by승주
        mouseY += Input.GetAxis("Mouse Y") * -1;

        //Camera가 돌지 않게 위아래로 제한 거는 기능 22.04.22 by승주
        if (mouseY > 10) mouseY = 10;
        if (mouseY < 0) mouseY = 0;

        //중심 축 회전 시키는 기능 22.04.22 by승주
        camAxis_contral.rotation = Quaternion.Euler
            (new Vector3(camAxis_contral.rotation.x + mouseY,
                                     camAxis_contral.rotation.y + mouseX, 0) * camSpeed
            );
    }

    void Zoom()
    {
        //wheel에 Mouse wheel 값 추가 기능 22.04.22 by승주
        wheel += Input.GetAxis("Mouse ScrollWheel") * 10;

        //zoomIn zoomOut 범위 제한 기능 22.04.22 by승주
        if (wheel >= -5) wheel = -5;
        if (wheel <= -10) wheel = -10;

        //camera Z값에 wheel 값 적용하는 기능 22.04.22 by승주
        Maincam.localPosition = new Vector3(0, 0, wheel);
    }

    
    void Update()
    {
        CamMove();
        Zoom();
    }
}
