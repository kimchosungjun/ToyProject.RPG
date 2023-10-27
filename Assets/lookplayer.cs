using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookplayer : MonoBehaviour
{
    public GameObject player;
    public GameObject head;
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 targetDirection = player.transform.position - head.transform.position;
            float step = 5.0f * Time.deltaTime; // 회전 속도 조절
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
           
    }
}
