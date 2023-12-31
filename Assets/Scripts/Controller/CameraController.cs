using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CameraMode cameraMode = CameraMode.QuarterView;
    [SerializeField] Vector3 delta = new Vector3(0,6,-5); // 카메라와 플레이어의 위치 차이
    [SerializeField] GameObject player;

    public void SetPlayer(GameObject player) { this.player = player; }

    void LateUpdate()
    {
        if (cameraMode == CameraMode.QuarterView)
        { if (player == null || !player.activeSelf)
                return;
            if(Physics.Raycast(player.transform.position,delta,out RaycastHit rayHit, delta.magnitude, LayerMask.GetMask("Block")))
            {
                float dist = (rayHit.point - player.transform.position).magnitude * 0.8f;
                transform.position = player.transform.position + delta.normalized * dist;
            }
            else
            {
                transform.position = player.transform.position + delta;
                transform.LookAt(player.transform); // 대상을 바라보도록 강제로 rotation 시키는 함수
            }
        }
    }

    public void SetQuaterView()
    {
        cameraMode = CameraMode.QuarterView;
        delta = new Vector3(0, 6, -5);
    }
}
