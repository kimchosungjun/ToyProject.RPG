using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    NavMeshAgent nav;
    PlayerStat playerStat;
    PlayerState playerState = PlayerState.Idle;

    [Header("플레이어 이동 관련 변수")]
    Vector3 destinationPos;
    [SerializeField] LayerMask blockLayer;
    int mongroundLayer = (1 << (int)Layer.Monster) | (1 << (int)Layer.Ground);
    void Awake()
    {
        playerStat = gameObject.GetComponent<PlayerStat>();
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
        if (Physics.Raycast(ray, out rayHit, 100.0f, mongroundLayer))
        {
            destinationPos = rayHit.point;
            playerState = PlayerState.Move;
            if (rayHit.collider.gameObject.layer == ((int)Layer.Monster))
                Debug.Log("Mon!");
            else
                Debug.Log("Player!");
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
            float moveDist = Mathf.Clamp(playerStat.Speed * Time.deltaTime, 0, dir.magnitude);
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
        anim.SetFloat("Speed", playerStat.Speed);
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
