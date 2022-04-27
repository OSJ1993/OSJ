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
        //game이 시작 될 때 카메리 위치 지정 기능 22.04.26 by승주
        wheel = -10;
        mouseY = 5;

        
        
    }

     

    void CamMove()
    {

        mouseX += Input.GetAxis("Mouse X");

        //mouseY 상하 반전을 하는 기능 22.04.26 by승주  
        mouseY += Input.GetAxis("Mouse Y") * -1;

        //camera가 빙빙 돌지 않게 하는 기능 22.04.26 by승주
        if (mouseY > 20) mouseY = 20;
        if (mouseY < -5) mouseY = -5;

        //중심축 회전 기능 22.04.26 by승주
        camAxis_central.rotation = Quaternion.Euler(new Vector3(
                                         camAxis_central.rotation.x + mouseY,
                                         camAxis_central.rotation.y + mouseX, 0) * camSpeed);
    }

    void Zoom()
    {
        wheel += Input.GetAxis("Mouse ScrollWheel") * 10;

        //zoomIn, zoomOut 범위 지정 기능 22.04.26 by승주
        if (wheel >= -10) wheel = -10;
        if (wheel <= -20) wheel = -20;

        //camera에 wheel 값 적응 시킨 기능 22.04.26 by승주
        cam.localPosition = new Vector3(0, 0, wheel);
    }

   

    void Update()
    {
        CamMove();
        Zoom();
    }

   
}
