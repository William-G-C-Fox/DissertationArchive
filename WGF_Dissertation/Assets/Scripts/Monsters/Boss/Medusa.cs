using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medusa : Monster
{
    private Animator mAnim;

    // Start is called before the first frame update
    void Start()
    {
        mAnim = GetComponentInChildren<Animator>();
        monRigBod = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        health = 8;
        accel = 0.5f;
        resistPos = 1;
        resistBurn = 1;
        dbleBleed = true;
    }

    // Update is called once per frame
    void Update()
    {
        AI();

        mAnim.SetFloat("TurnAng", GetComponentInChildren<MedusaAttack>().GetAng());
        mAnim.SetFloat("Speed", accel);
        mAnim.SetBool("Left", GetComponentInChildren<MedusaAttack>().GetLeftTurn());
        mAnim.SetBool("CanAttk", GetComponentInChildren<MedusaAttack>().attacking);

    }

    public override void AI()
    {
        Vector2 playerPos = player.GetComponent<PlayerController2D>().GetPos(); // find player position

        if (Vector2.Distance(player.GetComponent<PlayerController2D>().GetPos(), this.GetPos()) <= 10 && Vector2.Distance(player.GetComponent<PlayerController2D>().GetPos(), this.GetPos()) >= 1.5f)
        {
            GetComponentInChildren<MedusaGazeAttack>().BoxEnabled();
            GetComponentInChildren<MedusaAttack>().BoxDisabled();

            if (player.GetComponent<PlayerController2D>().getSpeed() == 5)
            {
                monRigBod.velocity = Vector2.zero; //This is so the monster doesn't get pushed then continue in the direction pushed forever.
                monRigBod.position = Vector2.MoveTowards(monRigBod.position, playerPos, accel * Time.deltaTime);
            }
        }
        else if (Vector2.Distance(player.GetComponent<PlayerController2D>().GetPos(), this.GetPos()) <= 1.5f)
        {

            GetComponentInChildren<MedusaGazeAttack>().BoxDisabled();
            GetComponentInChildren<MedusaAttack>().BoxEnabled();

            monRigBod.velocity = Vector2.zero; //This is so the monster doesn't get pushed then continue in the direction pushed forever.
            monRigBod.position = Vector2.MoveTowards(monRigBod.position, playerPos, accel * Time.deltaTime);
        }

    }

    public override void DestroyGameObject()
    {
        Instantiate(Resources.Load("Chest"), transform.position, Quaternion.Euler(0f, 0f, 0f));
        Destroy(gameObject);
    }
}
