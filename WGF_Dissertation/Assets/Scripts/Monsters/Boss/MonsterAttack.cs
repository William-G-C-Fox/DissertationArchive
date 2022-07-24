using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public Pivot pivot;
    public BoxCollider2D boxCol;
    public float dmg;
    public int effect;
    public GameObject player;
    public bool canAttk;
    public Vector3 setPlPos;
    public Quaternion plTurnPos;
    public float turnAng;
    public bool leftTran;
    public bool attacking;
    public float attkRate = 2;
    public float nextAttk;
    public bool inCollision;
    // Start is called before the first frame update
    void Start()
    {
        Pivot pivot = gameObject.GetComponentInParent(typeof(Pivot)) as Pivot;
        effect = 0;
        dmg = 0.5f;
        canAttk = true;

    }


    // Update is called once per frame
    void Update()
    {
        Attack();
        //Timer();
        //attacking = false;
        Turn(0);
    }
    //On stay in the collision box
    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            //timerRunning = true;
            inCollision = true;


        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            //timerRunning = true;
            inCollision = false;
            attacking = false;

        }
    }


    public virtual void Turn(float delay)
    {

        GameObject player;


        Pivot pivot = gameObject.GetComponentInParent(typeof(Pivot)) as Pivot;
        player = GameObject.FindGameObjectWithTag("Player");
        float angle;


        Vector3 target = player.GetComponent<PlayerController2D>().GetPos() - transform.position; // -Transform.position to find the position needed to rotate to

        target.Normalize(); //Make the vector have a magnitude of 1



        angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;

        turnAng = angle;

        if (turnAng > 130 && turnAng < 179)
        {
            leftTran = true;
        }
        else if (turnAng > -179 && turnAng < -130)
        {
            leftTran = true;
        }
        else
        {
            leftTran = false;
        }

        /** 
         * Calculates the angle to rotate to by taking the y and x of the target and using
         * Atan2 to calculate the angle to the target and converting it from radians to degrees   
         * To acheive a target angle
         */


        plTurnPos = Quaternion.Euler(0f, 0f, angle - 90);

        pivot.transform.rotation = Quaternion.RotateTowards(pivot.transform.rotation, Quaternion.Euler(0f, 0f, angle - 90), delay);
        /**
        * rotates the gameobject pivot which, is a parent of the weapon, since it's pos is set to
        * the middle of the player spirit. -90 is required since all the weapons are pointing up on start.
        */
    }


    public void BoxEnabled()
    {
        boxCol = GetComponent<BoxCollider2D>();
        boxCol.enabled = true;
    }

    public void BoxDisabled()
    {
        boxCol = GetComponent<BoxCollider2D>();
        boxCol.enabled = false;
    }
    public float GetAng()
    {
        return turnAng;
    }
    public bool GetLeftTurn()
    {
        return leftTran;
    }
    public bool GetCnAttk()
    {
        return attacking;
    }
    public virtual void Attack()
    {
        if (inCollision == true && Time.time > nextAttk) //If player is in the collision and time is greater than the next attack
        {
            attacking = true; //Animator boolean for attacking set to true
            nextAttk = Time.time + attkRate; //nextAttk = in game time + attackRate (= 1)

            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponentInParent<PlayerController2D>().OnHit(dmg, effect);
        }
        else
        {
            //attacking = false;
        }

    }
}
