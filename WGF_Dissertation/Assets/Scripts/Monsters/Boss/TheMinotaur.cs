using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheMinotaur : Monster
{
    public SpriteRenderer mSprite;
    public Animator mAnim;
    // Start is called before the first frame update
    void Start()
    {
        monRigBod = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        health = 10;
        accel = 0.4f;

        mAnim = GetComponentInChildren<Animator>();
        resistPos = 1;
        resistBleed = 1;
        dbleBurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //AI();
        ChargeSpeed();
        mAnim.SetFloat("Speed", accel);
        mAnim.SetBool("Attacking", GetComponentInChildren<MinotaurAttack>().attacking);
        ChangeDir();
    }

    public override void AI()
    {
        if (gameObject.GetComponentInChildren<MinotaurChargeAttack>().GetCharge() == false)
        {
            Vector2 playerPos = player.GetComponent<PlayerController2D>().GetPos(); // find player position

            if (Vector2.Distance(player.GetComponent<PlayerController2D>().GetPos(), this.GetPos()) <= 10) //if player's vector is within a distance of 3 from the monster then
            {
                monRigBod.velocity = Vector2.zero; //This is so the monster doesn't get pushed then continue in the direction pushed forever.
                monRigBod.position = Vector2.MoveTowards(monRigBod.position, playerPos, accel * Time.deltaTime); //Move towards the players position at a acceleration of half a second

            }
        }
        else if (GetComponentInChildren<MinotaurChargeAttack>().GetCharge() == true)
        {
            //instead of moving towards to the updating player position, a set position is saved and the minotaur charges towards it
            monRigBod.position = Vector2.MoveTowards(monRigBod.position, player.GetComponent<PlayerController2D>().GetPos(), accel * Time.deltaTime);

        }




    }

    void ChargeSpeed()
    {
        if (GetComponentInChildren<MinotaurChargeAttack>().GetCharge() == true)
        {
            SetSpeed(1);
        }
        else
        {
            SetSpeed(0.4f);
        }
    }

    /*
      * Since the sprite is either left or right, ChangeDir will flip the SpriteRenderer component in the direction that it is facing
      * this is done by taking the turn angle from the turn angle of the Turn method in Attack
      */
    private void ChangeDir()
    {
        float turnAng = GetComponentInChildren<MinotaurAttack>().GetAng();

        Debug.Log(turnAng);
        if (turnAng < 108 && turnAng > 106)
        {

            mSprite.transform.localScale = new Vector3(-1, 1, 1); //left
        }
        if (turnAng < 44 && turnAng > 42)
        {
            mSprite.transform.localScale = new Vector3(1, 1, 1); //right
        }
        if (turnAng > -110 && turnAng < -108)
        {
            mSprite.transform.localScale = new Vector3(-1, 1, 1); //left
        }
        if (turnAng > -45 && turnAng < -43)
        {
            mSprite.transform.localScale = new Vector3(1, 1, 1); //right
        }
    }



    public override void DestroyGameObject()
    {
        Instantiate(Resources.Load("Chest"), transform.position, Quaternion.Euler(0f, 0f, 0f)); //always drops a chest on the death of a boss
        Destroy(gameObject);
    }
}
