using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAnimation : MonoBehaviour
{
    private NavMeshAgent Agent;
    private Animator Animator;

    private Vector2 Velocity;
    private Vector2 SmoothDeltaPosition;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = Agent.GetComponent<Animator>();

        Animator.applyRootMotion = true;
        Agent.updatePosition = false;
        Agent.updateRotation = true;
    }

    private void OnAnimatorMove()
    {
        Vector3 rootPosition = Animator.rootPosition;
        rootPosition.y = Agent.nextPosition.y;
        transform.position = rootPosition;
        Agent.nextPosition = rootPosition;
    }

    private void Update()
    {
        SynchronizeAnimatorAndAgent();
    }

    private void SynchronizeAnimatorAndAgent()
    {
        Vector3 worldDeltaPosition = Agent.nextPosition - transform.position;
        worldDeltaPosition.y = 0;

        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
        SmoothDeltaPosition = Vector2.Lerp(SmoothDeltaPosition, deltaPosition, smooth);

        Velocity = SmoothDeltaPosition / Time.deltaTime;
        if (Agent.remainingDistance <= Agent.stoppingDistance)
        {
            Velocity = Vector2.Lerp(
                Vector2.zero,
                Velocity,
                Agent.remainingDistance / Agent.stoppingDistance
            );
        }

        bool shouldMove = Velocity.magnitude > 0.5f
            && Agent.remainingDistance > Agent.stoppingDistance;
        if (shouldMove)
            Animator.speed = 5f;
        else
            Animator.speed = 1f;
        Animator.SetBool("Walk", shouldMove);
        Animator.SetFloat("Locomotion", Velocity.magnitude);

        float deltaMagnitude = worldDeltaPosition.magnitude;
        if (deltaMagnitude > Agent.radius / 2f)
        {
            transform.position = Vector3.Lerp(
                Animator.rootPosition,
                Agent.nextPosition,
                smooth
            );
        }
    }
}