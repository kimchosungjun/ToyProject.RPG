using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [Header("플레이어 이동 관련 변수")]
    [SerializeField] protected GameObject lockTarget;
    [SerializeField] protected Vector3 destinationPos;
    [SerializeField] protected AnimationState playerState = AnimationState.Idle;

    public WorldObject WorldObjectType { get; protected set; } = WorldObject.UnKnown;
    public virtual AnimationState State
    {
        get { return playerState; }
        set
        {
            Animator anim = GetComponent<Animator>();
            playerState = value;
            switch (playerState)
            {
                case AnimationState.Idle:
                    anim.CrossFade("Idle", 0.1f);
                    break;
                case AnimationState.Move:
                    anim.CrossFade("Run", 0.1f);
                    break;
                case AnimationState.Skill:
                    anim.CrossFade("Attack", 0.1f, -1, 0);
                    break;
                case AnimationState.Dead:
                    break;
            }
        }
    }
    void Start()
    {
        Init();
    }

   
    void Update()
    {
        switch (State)
        {
            case AnimationState.Idle:
                UpdateIdle();
                break;
            case AnimationState.Move:
                UpdateMove();
                break;
            case AnimationState.Skill:
                UpdateSkill();
                break;
            case AnimationState.Dead:
                UpdateDead();
                break;
        }
    }
    public abstract void Init();
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateMove() { }
    protected virtual void UpdateSkill() { }
    protected virtual void UpdateDead() { }
}
