using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotate : MonoBehaviour
{
    public Transform Player;
    public float headWeight=1.0F;
    public float bodyWeight=0.5F;
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
