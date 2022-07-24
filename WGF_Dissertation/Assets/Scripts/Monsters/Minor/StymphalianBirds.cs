using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StymphalianBirds : Monster
{

    private Animator stAnim;
    // Start is called before the first frame update
    void Start()
    {
        stAnim = GetComponentInChildren<Animator>();
        monRigBod = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        health = 0.5f;
        accel = 0.75f;
    }

    // Update is called once per frame
    void Update()
    {
        AI();
        player = GameObject.FindGameObjectWithTag("Player");
        //AI();
        stAnim.SetFloat("TurnAng", GetComponentInChildren<StymphalianBirdsAttack>().GetAng());
        stAnim.SetFloat("Speed", accel);
        stAnim.SetBool("Left", GetComponentInChildren<StymphalianBirdsAttack>().GetLeftTurn());
    }
    public override void AI()
    {
        Vector2 playerPos = player.GetComponent<PlayerController2D>().GetPos(); // find player position


        if (Vector2.Distance(player.GetComponent<PlayerController2D>().GetPos(), this.GetPos()) <= 5 && Vector2.Distance(player.GetComponent<PlayerController2D>().GetPos(), this.GetPos()) >= 0.55f) //if player's vector is within a distance of 3 from the monster then
        {
            monRigBod.velocity = Vector2.zero; //This is so the monster doesn't get pushed then continue in the direction pushed forever.
            monRigBod.position = Vector2.MoveTowards(monRigBod.position, playerPos, accel * Time.deltaTime); //Move towards the players position at a acceleration of half a second

        }
        else if (Vector2.Distance(player.GetComponent<PlayerController2D>().GetPos(), this.GetPos()) <= 0.55f)
        {

            monRigBod.velocity = Vector2.zero;
            Vector3 nPos = playerPos;
            nPos.x = nPos.x + 5;
            nPos.y = nPos.y + 5;
            monRigBod.position = Vector2.MoveTowards(monRigBod.position, nPos, accel * Time.deltaTime);
        }

    }

    public IEnumerator AttackWait(float delay)
    {

        yield return new WaitForSeconds(delay);

    }
}
