using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAnimation : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent nav;

    public Vector3 worldDeltaVec;
    public Vector2 groundVec;
    public Vector2 velocity = Vector2.zero;

    void Start()
    {
        nav.updatePosition = false;    
    }

    void Update()
    {
        worldDeltaVec = nav.nextPosition - transform.position;
        groundVec.x = Vector3.Dot(transform.right, worldDeltaVec);
        groundVec.y = Vector3.Dot(transform.forward, worldDeltaVec);
        velocity = (Time.deltaTime > 1e-5f)?  groundVec/Time.deltaTime : velocity = Vector2.zero;
        bool shouldMove = velocity.magnitude > 0.025f && nav.remainingDistance > nav.radius;
        anim.SetBool("Walk",shouldMove);
        anim.SetFloat("VelX",velocity.x);
        anim.SetFloat("VelY",velocity.y);
    }

    private void OnAnimatorMove()
    {
        transform.position = nav.nextPosition;
    }
}
