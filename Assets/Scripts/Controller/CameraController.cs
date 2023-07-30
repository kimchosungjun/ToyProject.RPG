using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CameraMode cameraMode = CameraMode.QuarterView;
    [SerializeField] Vector3 delta = new Vector3(0,6,-5); // ī�޶�� �÷��̾��� ��ġ ����
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
                transform.LookAt(player.transform); // ����� �ٶ󺸵��� ������ rotation ��Ű�� �Լ�
            }
        }
    }

    public void SetQuaterView()
    {
        cameraMode = CameraMode.QuarterView;
        delta = new Vector3(0, 6, -5);
    }
}
