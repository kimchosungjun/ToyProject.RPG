using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    NavMeshAgent nav;
    PlayerState playerState = PlayerState.Idle;

    [Header("플레이어 이동 관련 변수")]
    Vector3 destinationPos;
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] LayerMask blockLayer;
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        nav = gameObject.GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        MasterManager.Input.PlayerMouseMove -= PlayerInputMouse;
        MasterManager.Input.PlayerMouseMove += PlayerInputMouse;
    }
    public void PlayerInputMouse(MouseEvent evt)
    {
        if (playerState==PlayerState.Dead)
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100f, Color.red, 1.0f);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, 100.0f, LayerMask.GetMask("Ground")))
        {
            destinationPos = rayHit.point;
            playerState = PlayerState.Move;
        }

    }

    void Update()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Move:
                UpdateMove();
                break;
            case PlayerState.Jump:
                UpdateJump();
                break;
            case PlayerState.Hit:
                UpdateHit();
                break;
            case PlayerState.Dead:
                UpdateDead();
                break;
        }
    }

    void PlayerMove()
    {
        Vector3 dir = destinationPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            playerState = PlayerState.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(playerSpeed * Time.deltaTime, 0, dir.magnitude);
            nav.Move(dir.normalized * moveDist);
            Debug.DrawRay(transform.position+ Vector3.up, dir.normalized, Color.red);
            if (Physics.Raycast(transform.position + Vector3.up, dir.normalized, 1.0f, blockLayer))
            {
                playerState = PlayerState.Idle; 
                return;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
    }
    void UpdateIdle() 
    {
        anim.SetFloat("Speed", 0);
    }

    void UpdateMove()
    {
        PlayerMove();
        anim.SetFloat("Speed", playerSpeed);
    }
    void UpdateJump()
    {

    }

    void UpdateHit()
    {
        anim.SetTrigger("Hit");
    }

    void UpdateDead()
    {
        anim.SetTrigger("Dead");
    }
}
