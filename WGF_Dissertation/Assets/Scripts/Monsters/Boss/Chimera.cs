using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chimera : Monster
{
    //private bool canFire;
    private Animator chAnim;
    private float turnAng;
    public Sprite goatFlat, goatSide;
    private SpriteRenderer gH;
    // Start is called before the first frame update
    void Start()
    {
        chAnim = GetComponentInChildren<Animator>();
        monRigBod = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        health = 10;
        accel = 0.4f;
        resistPos = 1;
        resistBurn = 1;
        dbleBleed = true;

    }

    // Update is called once per frame
    void Update()
    {
        turnAng = GetComponentInChildren<ChimeraAttack>().GetAng();
        chAnim.SetFloat("TurnAng", GetComponentInChildren<ChimeraAttack>().GetAng());
        chAnim.SetFloat("Speed", accel);
        chAnim.SetBool("Left", GetComponentInChildren<ChimeraAttack>().GetLeftTurn());
        chAnim.SetBool("Attacking", GetComponentInChildren<ChimeraAttack>().attacking);


        AI();
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    public override void AI()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Vector2 playerPos = player.GetComponent<PlayerController2D>().GetPos(); // find player position


        if (Vector2.Distance(player.GetComponent<PlayerController2D>().GetPos(), this.GetPos()) <= 10) //if player's vector is within a distance of 3 from the monster then
        {

            monRigBod.velocity = Vector2.zero; //This is so the monster doesn't get pushed then continue in the direction pushed forever.

            monRigBod.position = Vector2.MoveTowards(monRigBod.position, playerPos, accel * Time.deltaTime); //Move towards the players position at a acceleration of half a second

        }




    }



    public override void DestroyGameObject()
    {
        Instantiate(Resources.Load("Chest"), transform.position, Quaternion.Euler(0f, 0f, 0f));
        Destroy(gameObject);
    }
}
