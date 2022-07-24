using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpy : Monster
{
    private Animator hAnim;
    // Start is called before the first frame update
    void Start()
    {
        monRigBod = GetComponent<Rigidbody2D>();
        hAnim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        health = 2;
        accel = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        AI();
        hAnim.SetFloat("TurnAng", GetComponentInChildren<HarpyAttack>().GetAng());
        hAnim.SetFloat("Speed", accel);
        hAnim.SetBool("Left", GetComponentInChildren<HarpyAttack>().GetLeftTurn());
        hAnim.SetBool("CanAttk", GetComponentInChildren<HarpyAttack>().GetCnAttk());
    }

    public override void AI()
    {
        Vector2 playerPos = player.GetComponent<PlayerController2D>().GetPos(); // find player position


        if (Vector2.Distance(player.GetComponent<PlayerController2D>().GetPos(), this.GetPos()) <= 5) //if player's vector is within a distance of 3 from the monster then
        {
            monRigBod.velocity = Vector2.zero; //This is so the monster doesn't get pushed then continue in the direction pushed forever.
            monRigBod.position = Vector2.MoveTowards(monRigBod.position, playerPos, accel * Time.deltaTime); //Move towards the players position at a acceleration of half a second

        }
        else if (Vector2.Distance(player.GetComponent<PlayerController2D>().GetPos(), this.GetPos()) <= 0.5f)
        {

            monRigBod.velocity = Vector2.zero;
            monRigBod.position = Vector2.MoveTowards(monRigBod.position, -playerPos, accel * Time.deltaTime); //Moves the harpy away from the player if too close
        }

    }
}
