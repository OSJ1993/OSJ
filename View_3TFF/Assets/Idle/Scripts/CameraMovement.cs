using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //따라가야할  오브젝트의 정보
    public Transform objectTofollow;

    //따라가야할 스피드
    public float followSpeed = 10f;

    //마우스 감도.
    public float sensitivity = 100f;

    //카메라 각도(앵글 제한)
    public float clampAngle = 70f;

    //마우스 인풋
    private float rotX;
    private float rotY;

    //카메라 정보,
    public Transform realCamera;

    //카메라 방향.
    public Vector3 dirNormalized;

    //최종방향 저장.
    public Vector3 finalDir;

    //카메라와 플레이어의 최소거리, 최대거리.
    public float minDistance;
    public float maxDistance;

    //최종적으로?
    public float finalDistance;

    public float smoothness=10f;

    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;

        //마우스 커서 안 보이게 하기.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
         
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        //최소값, 최대값
        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }

    //업데이트가 끝나고 실행하는 업데이트(LateUpdate)
    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, objectTofollow.position, followSpeed * Time.deltaTime);

        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        RaycastHit hit;

        if (Physics.Linecast(transform.position,finalDir,out hit))
        {
            //만약에 뭐가 있다면.
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else //만약에 뭐가 없다면.
        {
            finalDistance = maxDistance;
        }

        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);
    }
}
