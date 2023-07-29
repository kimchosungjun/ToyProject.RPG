using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    NavMeshAgent nav;
    PlayerStat playerStat;


    [SerializeField] PlayerState playerState = PlayerState.Idle;
    public PlayerState State { get { return playerState; }
        set 
        {
            playerState = value;
            switch (playerState)
            {
                case PlayerState.Idle:
                    anim.CrossFade("Idle",0.1f);
                    break;
                case PlayerState.Move:
                    anim.CrossFade("Run", 0.1f);
                    break;
                case PlayerState.Skill:
                    anim.CrossFade("Attack", 0.1f,-1,0);
                    break;
                case PlayerState.Dead:
                    break;
            }
        } 
    }
    GameObject lockTarget;
   
    [Header("플레이어 이동 관련 변수")]
    Vector3 destinationPos;
    [SerializeField] LayerMask blockLayer;
    int mongroundLayer = (1 << (int)Layer.Monster) | (1 << (int)Layer.Ground);
    bool stopSkill = false;

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
        if (State==PlayerState.Dead)
            return;
        switch (State)
        {
            case PlayerState.Idle:
                OnMouseEventIdle(evt);
                break;
            case PlayerState.Move:
                OnMouseEventIdle(evt);
                break;
            case PlayerState.Skill:
                {
                    if (evt == MouseEvent.PointerUp)
                        stopSkill = true;
                }
                break;
        }
    }

    void HitEvent()
    {
        if (stopSkill)
        {
            State = PlayerState.Idle;
        }
        else
        {
            State = PlayerState.Skill;
        }   
    }

    void OnMouseEventIdle(MouseEvent evt)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        bool rayCastHit = Physics.Raycast(ray, out rayHit, 100.0f, mongroundLayer);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100f, Color.red, 1.0f);

        switch (evt)
        {
            case MouseEvent.PointerDown:
                {
                    if (rayCastHit)
                    {
                        destinationPos = rayHit.point;
                        State = PlayerState.Move;
                        stopSkill = false;
                        if (rayHit.collider.gameObject.layer == ((int)Layer.Monster))
                            lockTarget = rayHit.collider.gameObject;
                        else
                            lockTarget = null;
                    }
                }
                break;
            case MouseEvent.Press:
                {
                    if (lockTarget == null && rayCastHit)
                        destinationPos = rayHit.point;
                }
                break;
            case MouseEvent.PointerUp:
                stopSkill = true;
                break;
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
            case PlayerState.Skill:
                UpdateSkill();
                break;
            case PlayerState.Damaged:
                UpdateDamaged();
                break;
            case PlayerState.Dead:
                UpdateDead();
                break;
        }
    }
    #region UpdateState

    void UpdateIdle() 
    {
        anim.SetFloat("Speed", 0);
    }

    void UpdateMove()
    {
        if (lockTarget != null)
        {
            float distance = (destinationPos - transform.position).magnitude;
            if (distance <= 1)
            {
                State = PlayerState.Skill;
                return;
            }
        }
        Vector3 dir = destinationPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = PlayerState.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(playerStat.Speed * Time.deltaTime, 0, dir.magnitude);
            nav.Move(dir.normalized * moveDist);
            Debug.DrawRay(transform.position + Vector3.up, dir.normalized, Color.red);
            if (Physics.Raycast(transform.position + Vector3.up, dir.normalized, 1.0f, blockLayer))
            {
                if (Input.GetMouseButton(0) == false)
                    State = PlayerState.Idle;
                return;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
    }

    void UpdateJump()
    {
        
    }

    void UpdateSkill()
    {
        if (lockTarget != null)
        {
            Vector3 dir = lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    void UpdateDamaged()
    {
        
    }

    void UpdateDead()
    {
        
    }

    #endregion
}
