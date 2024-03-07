using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAnimation : MonoBehaviour
{
    //public GameObject cube;
    Animator anim;
    NavMeshAgent nav;

    Vector2 velocity;
    Vector2 smoothDeltaPos;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

        anim.applyRootMotion = true;
        nav.updatePosition = false;
        nav.updateRotation = true;
    }

    private void OnAnimatorMove()
    {
        Vector3 rootPos = anim.rootPosition;
        rootPos.y = nav.nextPosition.y;
        transform.position = rootPos;
        //nav.nextPosition = rootPos;
    }

    private void Update()
    {
        //nav.SetDestination(cube.transform.position);
        Vector3 worldDeltaPos = nav.nextPosition - transform.position;
        worldDeltaPos.y = 0;
        float dx = Vector3.Dot(transform.right, worldDeltaPos);
        float dy = Vector3.Dot(transform.forward, worldDeltaPos);
        Vector2 deltaPos = new Vector2(dx, dy);
        float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
        smoothDeltaPos = Vector2.Lerp(smoothDeltaPos, deltaPos, smooth);

        velocity = smoothDeltaPos / Time.deltaTime;
        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            velocity = Vector2.Lerp(
                Vector2.zero,
                velocity,
                nav.remainingDistance / nav.stoppingDistance
            );
        }

        bool shouldMove = velocity.magnitude > 0.5f
            && nav.remainingDistance > nav.stoppingDistance;

        anim.SetBool("Walk", shouldMove);
        anim.SetFloat("VelY", velocity.magnitude);

        float deltaMagnitude = worldDeltaPos.magnitude;
        if (deltaMagnitude > nav.radius / 2f)
        {
            transform.position = Vector3.Lerp(
                anim.rootPosition,
                nav.nextPosition,
                smooth
            );
        }
    }
}