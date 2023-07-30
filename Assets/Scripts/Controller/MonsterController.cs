using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat stat;
    NavMeshAgent nav;
    [SerializeField] float scanRange = 10f;
    [SerializeField] float attackRange = 2f;

    private void Awake()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
    }
    public override void Init()
    {
        WorldObjectType = WorldObject.Monster;
        stat = gameObject.GetComponent<Stat>();
        if (gameObject.GetComponentInChildren<UIHpBar>() == null)
            MasterManager.UI.MakeWorldSpaceUI<UIHpBar>(transform);
    }

    protected override void UpdateIdle()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            return;
        float distance = (player.transform.position - transform.position).magnitude;
        if(distance<=scanRange)
        {
            lockTarget = player;
            State = AnimationState.Move;
            return;
        }
    }
    protected override void UpdateMove()
    {
        if (lockTarget != null)
        {
            destinationPos = lockTarget.transform.position;
            float distance = (destinationPos - transform.position).magnitude;
            if (distance <= attackRange)
            {
                State = AnimationState.Skill;
                nav.SetDestination(transform.position);
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
            nav.SetDestination(destinationPos);
            nav.speed = stat.Speed;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
    }
    protected override void UpdateSkill()
    {
        base.UpdateIdle();
    }

    public void HitEvent()
    {
        if (lockTarget != null)
        {
            Stat targetStat = lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(stat);
            if (targetStat.Hp > 0)
            {
                float distance = (lockTarget.transform.position - transform.position).magnitude;
                if (distance <= attackRange)
                {
                    State = AnimationState.Skill;
                } 
                else
                {
                    State = AnimationState.Move;
                }
            }
            else
            {
                State = AnimationState.Idle;
            }
        }
        else
        {
            State = AnimationState.Idle;
        }
    }
}
