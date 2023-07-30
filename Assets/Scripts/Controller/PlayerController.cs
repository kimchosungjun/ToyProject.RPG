using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
    int mongroundLayer = (1 << (int)Layer.Monster) | (1 << (int)Layer.Ground);
    [SerializeField] LayerMask blockLayer;
    PlayerStat playerStat;
    bool stopSkill = false;
    private void Awake()
    {
        playerStat = gameObject.GetComponent<PlayerStat>();
    }

    public override void Init()
    {
        WorldObjectType = WorldObject.Player;
        MasterManager.Input.PlayerMouseMove -= PlayerInputMouse;
        MasterManager.Input.PlayerMouseMove += PlayerInputMouse;
        MasterManager.UI.MakeWorldSpaceUI<UIHpBar>(transform);
    }

    public void PlayerInputMouse(MouseEvent evt)
    {
        if (State==AnimationState.Dead)
            return;
        switch (State)
        {
            case AnimationState.Idle:
                OnMouseEventIdle(evt);
                break;
            case AnimationState.Move:
                OnMouseEventIdle(evt);
                break;
            case AnimationState.Skill:
                {
                    if (evt == MouseEvent.PointerUp)
                        stopSkill = true;
                }
                break;
        }
    }

    void HitEvent()
    {
        if (lockTarget != null)
        {
            Stat targetStat = lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(playerStat);
        }

        if (stopSkill)
        {
            State = AnimationState.Idle;
        }
        else
        {
            State = AnimationState.Skill;
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
                        State = AnimationState.Move;
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


    #region UpdateState

    protected override void UpdateIdle() 
    {

    }

    protected override void UpdateMove()
    {
        if (lockTarget != null)
        {
            destinationPos = lockTarget.transform.position;
            float distance = (destinationPos - transform.position).magnitude;
            if (distance <= 1)
            {
                State = AnimationState.Skill;
                return;
            }
        }
        Vector3 dir = destinationPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = AnimationState.Idle;
        }
        else
        {
            Debug.DrawRay(transform.position + Vector3.up, dir.normalized, Color.red);
            if (Physics.Raycast(transform.position + Vector3.up, dir.normalized, 1.0f, blockLayer))
            {
                if (Input.GetMouseButton(0) == false)
                    State = AnimationState.Idle;
                return;
            }
            float moveDist = Mathf.Clamp(playerStat.Speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
    }

    protected override void UpdateSkill()
    {
        if (lockTarget != null)
        {
            Vector3 dir = lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    protected override void UpdateDead()
    {
        
    }

    #endregion
}
