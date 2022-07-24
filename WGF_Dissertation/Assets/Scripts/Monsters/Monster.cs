using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Rigidbody2D monRigBod;
    public float health;
    public float accel = 0.5f;
    public Vector2 pos;
    public Vector2 enPos;

    public GameObject player;

    public float resistPos;
    public float resistBurn;
    public float resistBleed;
    public bool dbleBurn;
    public bool dblePos;
    public bool dbleBleed;
    public int effect;


    // Start is called before the first frame update
    void Start()
    {
        monRigBod = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        health = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            AI();
        }


    }
    //Damages the monster

    //Gets the postion of the monster from the rigid body
    public Vector2 GetPos()
    {
        pos = monRigBod.position;
        return pos;
    }

    //Takes damage
    public void OnHit(float dmg, int neffect)
    {
        GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 0.4f, 0.4f, 1f);
        if (health > 0)
        {

            this.health = this.health - dmg; //minus the health from the damage intake
            this.effect = neffect;

            Effect();

        }
        else
        {
            //Debug.Log("Dead");
            DestroyGameObject(); //if health >= 0 then the monster is destoryed 
        }
        GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

    }
    /** 
     * AI handles the movement of the base monster class
     */
    public virtual void AI()
    {
        Vector2 playerPos = player.GetComponent<PlayerController2D>().GetPos(); // find player position


        if (Vector2.Distance(player.GetComponent<PlayerController2D>().GetPos(), this.GetPos()) <= 3) //if player's vector is within a distance of 3 from the monster then
        {
            monRigBod.velocity = Vector2.zero; //This is so the monster doesn't get pushed then continue in the direction pushed forever.
            monRigBod.position = Vector2.MoveTowards(monRigBod.position, playerPos, accel * Time.deltaTime); //Move towards the players position at a acceleration of half a second

        }

    }

    public void Slow()
    {
        this.accel = accel - 0.25f;
    }

    public void ResetSpeed()
    {
        this.accel = accel + 0.25f;
    }
    /**
     * EffectDmg uses the IEnumerator class to be used a ourtine
     * 
     */
    public IEnumerator EffectDmg(float dmg, float delay, float speed, int nEffect)
    {
        if (nEffect == 1)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 0.2f, 0.2f, 1f); //changes the colour of the player to match the effect
        }
        else if (nEffect == 2)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 0.8f, 0.4f, 1f);
        }
        else if (nEffect == 3)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color(0.4f, 1f, 0.4f, 1f);
        }
        accel = accel - speed;
        yield return new WaitForSeconds(delay);
        health = health - dmg;
        GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);


        accel = accel + speed;

        effect = 0;
    }

    public void Effect()
    {
        switch (effect)
        {
            case 1:
                if (resistBleed == 0.5)
                {
                    StartCoroutine(EffectDmg(0.25f, 0.5f, 0, 1));
                    StartCoroutine(EffectDmg(0.25f, 0.5f, 0, 1));
                }
                else if (resistBleed == 1)
                {
                    StartCoroutine(EffectDmg(0, 0.5f, 0, 1));
                }
                else if (resistBleed == 0)
                {
                    StartCoroutine(EffectDmg(0.5f, 0.5f, 0, 1));
                    StartCoroutine(EffectDmg(0.5f, 0.5f, 0, 1));
                }
                else if (dbleBleed == true)
                {
                    StartCoroutine(EffectDmg(1, 0.5f, 0, 1));
                    StartCoroutine(EffectDmg(1, 0.5f, 0, 1));
                }
                break;

            case 2:
                if (resistBurn == 0.5)
                {
                    StartCoroutine(EffectDmg(0.5f, 1, 0, 2));

                }
                else if (resistBurn == 1)
                {
                    StartCoroutine(EffectDmg(0, 1, 0, 2));
                }
                else if (resistBurn == 0)
                {
                    StartCoroutine(EffectDmg(1, 1, 0, 2));

                }
                else if (dbleBurn == true)
                {
                    StartCoroutine(EffectDmg(1.5f, 1, 0, 2));

                }
                break;

            case 3:
                if (resistPos == 0.5)
                {
                    StartCoroutine(EffectDmg(0.25f, 1, 0.25f, 3));

                }
                else if (resistPos == 1)
                {
                    StartCoroutine(EffectDmg(0, 1, 0.25f, 3));
                }
                else if (resistPos == 0)
                {
                    StartCoroutine(EffectDmg(0.5f, 1, 0.25f, 3));

                }
                else if (dblePos == true)
                {
                    StartCoroutine(EffectDmg(1, 1, 0.25f, 3));

                }
                break;

            case 0:
                break;
        }
    }

    public void SetSpeed(float speed)
    {
        accel = speed;
    }

    public virtual void DestroyGameObject()
    {
        ChestChance();
        Destroy(gameObject);
    }

    private void ChestChance()
    {
        int rand = Random.Range(0, 11);
        if (rand == 0)
        {
            Instantiate(Resources.Load("Chest"), transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
    }


}
