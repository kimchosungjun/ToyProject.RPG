using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CameraMode cameraMode = CameraMode.QuarterView;
    [SerializeField] Vector3 delta = new Vector3(0,6,-5); // ī�޶�� �÷��̾��� ��ġ ����
    [SerializeField] GameObject player;

    public void FindPlayer()
    {
        if (player == null)
            player = GameObject.Find("Player");
    }

    void LateUpdate()
    {
        if(cameraMode == CameraMode.QuarterView)
        {
            if(Physics.Raycast(player.transform.position,delta,out RaycastHit rayHit, delta.magnitude, LayerMask.GetMask("Wall")))
            {
                float dist = (rayHit.point - player.transform.position).magnitude * 0.8f;
                transform.position = player.transform.position + delta.normalized * dist;
            }
            transform.position = player.transform.position + delta;
            transform.LookAt(player.transform); // ����� �ٶ󺸵��� ������ rotation ��Ű�� �Լ�
        }
    }

    public void SetQuaterView()
    {
        cameraMode = CameraMode.QuarterView;
        delta = new Vector3(0, 6, -5);
    }
}
