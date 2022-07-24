using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revenant : Monster
{

    private Animator revAnim;
    // Start is called before the first frame update
    void Start()
    {
        monRigBod = GetComponent<Rigidbody2D>();
        revAnim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        health = 3;
        accel = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        AI();

        revAnim.SetFloat("TurnAng", GetComponentInChildren<RevenantAttack>().GetAng());
        revAnim.SetFloat("Speed", accel);
        revAnim.SetBool("Left", GetComponentInChildren<RevenantAttack>().GetLeftTurn());
        revAnim.SetBool("CanAttk", GetComponentInChildren<RevenantAttack>().attacking);

    }





}

