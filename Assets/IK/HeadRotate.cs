using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotate : MonoBehaviour
{
    public Transform Player;
    public float headWeight;
    public float bodyWeight;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetLookAtPosition(Player.position);
        anim.SetLookAtWeight(1, bodyWeight, headWeight);
    }
}
