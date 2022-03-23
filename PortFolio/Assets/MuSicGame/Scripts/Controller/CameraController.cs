using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //카메라가 어떤 대상을 따라갈 지 타겟 설정. /22.03.23 by승주
    [SerializeField] Transform thePlayer = null;

    //카메라 이동 속도 /22.03.23 by승주
    [SerializeField] float followSpeed = 15;

    //카메라와 플레이어의 거리 차  /22.03.23 by승주
    Vector3 playerDistance = new Vector3();

    void Start()
    {
        //거리차: 카메라 위치 - 플레이어 위치 /22.03.23 by승주
        playerDistance = transform.position - thePlayer.position;
    }

    
    void Update()
    {
        //매 프레임 마다 플레이어 추적 이동할 좌표값을 구한 뒤, 카메라 이동 /22.03.23 by승주
        
        Vector3 t_destPos = thePlayer.position + playerDistance;

        //Vecter3.Lerp(A,B,C):A와 B사이의 값에서 C비율의 값을 추출 
        //ex:Mathf.Lerp(0,10,0.5)=>5
        transform.position = Vector3.Lerp(transform.position, t_destPos, followSpeed * Time.deltaTime);

    }
}
