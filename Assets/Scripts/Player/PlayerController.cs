using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;
    Vector3 destinationPos;
    bool moveDestination = false;

    void Start()
    {
        MasterManager.Event.PlayerMouseMove -= PlayerInputMouse;
        MasterManager.Event.PlayerMouseMove += PlayerInputMouse;
    }
    public void PlayerInputMouse(MouseEvent evt)
    {
        if (evt != MouseEvent.Click)
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100f, Color.red, 1.0f);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, 100.0f, LayerMask.GetMask("Ground")))
        {
            destinationPos = rayHit.point;
            moveDestination = true;
        }
    }

    void Update()
    {
        if(moveDestination)
            PlayerMove();    
    }

    void PlayerMove()
    {
        Vector3 dir = destinationPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            moveDestination = false;
        }
        else
        {
            float moveDist = Mathf.Clamp(playerSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
    }
}
