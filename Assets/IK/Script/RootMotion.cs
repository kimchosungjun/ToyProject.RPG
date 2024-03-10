using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RootMotion : MonoBehaviour
{
    NavMeshAgent nav;
    Animator anim;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (nav.remainingDistance < nav.radius)
            nav.ResetPath();
        if (nav.hasPath)
        {
            anim.SetBool("Walk", true);
            var dir = (nav.steeringTarget - transform.position).normalized;
            var animDir = transform.InverseTransformDirection(dir);
            anim.SetFloat("VX", animDir.x);
            anim.SetFloat("VY", animDir.z);
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetFloat("VX", 0);
            anim.SetFloat("VY", 0);
        }
    }
}
