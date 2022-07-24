using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spartae : Monster
{
    public SpriteRenderer sprite;
    public Animator sAnim;
    bool right;
    // Start is called before the first frame update
    void Start()
    {
        monRigBod = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        health = 3;
        accel = 0.5f;
        sAnim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GetComponentInChildren<SpartaeAttack>().GetAng());
        AI();
        sAnim.SetFloat("Speed", accel);
        sAnim.SetBool("Attacking", GetComponentInChildren<SpartaeAttack>().attacking);
        ChangeDir();

    }
    /*
     * Since the sprite is either left or right, ChangeDir will flip the SpriteRenderer component in the direction that it is facing
     * this is done by taking the turn angle from the turn angle of the Turn method in Attack
     */
    private void ChangeDir()
    {
        float turnAng = GetComponentInChildren<SpartaeAttack>().GetAng();


        if (turnAng < 108 && turnAng > 106)
        {

            sprite.transform.localScale = new Vector3(-1, 1, 1); //left
        }
        if (turnAng < 44 && turnAng > 42)
        {
            sprite.transform.localScale = new Vector3(1, 1, 1); //right
        }
        if (turnAng > -110 && turnAng < -108)
        {
            sprite.transform.localScale = new Vector3(-1, 1, 1); //left
        }
        if (turnAng > -45 && turnAng < -43)
        {
            sprite.transform.localScale = new Vector3(1, 1, 1); //right
        }
    }

}
