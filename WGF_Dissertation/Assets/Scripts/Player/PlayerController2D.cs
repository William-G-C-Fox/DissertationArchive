using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class PlayerController2D : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float acceleration;
    public float speed;
    public GameObject player, axe, sword, spear, kopis;
    public PlAttack weap;
    public GameObject comp;
    public Vector3 pos;

    private bool leftTran;
    private Animator plAnim;
    private Rigidbody2D playerRigBod;

    private float baseSpeed;
    private float comSpeed;
    private float xSpeed;
    private float ySpeed;
    private Vector3 vel;
    private bool outOfGaze;
    private bool isPos;
    private bool isBurn;
    private bool isBleed;
    private float resistPos;
    private float resistBurn;
    private float resistBleed;
    private int effect;
    private float ang;
    private float effectTimeMax;


    string path = "Assets/Resources/PlStats.txt";

    void Start()
    {

        ReadInStats(path);

        PlAttack weap = GetComponentInChildren<PlAttack>();
        playerRigBod = GetComponent<Rigidbody2D>();
        comp.SetActive(false);
        plAnim = GetComponentInChildren<Animator>();
        player.name = "Player";
        baseSpeed = acceleration;
        speed = acceleration;

        outOfGaze = false;
        //SwitchAnim("Sword", "Axe");
    }
    private void Update()
    {
        //handles all of the animation requirements
        plAnim.SetFloat("PlayerAngle", ang);
        plAnim.SetFloat("Speed", 0.2f);
        plAnim.SetBool("Left", leftTran);
        plAnim.SetBool("CanAttk", GetComponentInChildren<PlAttack>().attacking);
        SpeedReset();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        RotateWeapon();




    }
    private void Move() //Moves the player in game
    {


        // float horizontalAxis, verticalAxis;
        float horizontalAxis = Input.GetAxis("Horizontal"); //horizontalAxis = get axis from the input of the a or d key for the -x and +x axis
        float verticalAxis = Input.GetAxis("Vertical"); //verticalAxis = get axis from the input of the w or s key for the -y and +y axis
        comSpeed = horizontalAxis + verticalAxis;

        Vector2 vectMove = new Vector2(horizontalAxis, verticalAxis); //create a new move vector from the input axis
        if (Input.GetKey(KeyCode.LeftShift))
        {

            speed = acceleration + 20;

        }
        else
        {
            speed = acceleration;
        }

        playerRigBod.AddForce(vectMove * speed);
        // add force in the direction of the vectMove vector by the acceleration



        if (Input.anyKeyDown != true) // if no key is pressed then stop the player movement
        {
            Stop();

        }
    }

    public void Stop()
    {
        playerRigBod.velocity = Vector2.zero; //sets the movement to 0
    }


    //If the player is hit this is called
    public void OnHit(float dmg, int nEffect)
    {
        GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 0.4f, 0.4f, 1f);
        Debug.Log(nEffect);
        effect = nEffect;
        if (health > 0)
        {
            this.health = this.health - dmg; //health is minused by the damage

            Effect();
            //Debug.Log("PL: Health");
            //Debug.Log(health);

        }
        else
        {
            //Debug.Log("Dead");
            DestroyGameObject(); //Kills the player
        }
        GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }
    //returns the position of the player
    public Vector3 GetPos()
    {
        playerRigBod = GetComponent<Rigidbody2D>();
        pos = playerRigBod.position;
        return pos;
    }

    private void RotateWeapon()
    {

        Pivot pivot = gameObject.GetComponentInChildren(typeof(Pivot)) as Pivot;

        //player = GameObject.FindGameObjectWithTag("Player");
        float angle;
        Vector3 pointPos = (Input.mousePosition); // Find the position of the mouse pointer
        pointPos = Camera.main.ScreenToWorldPoint(pointPos); //Convert the position to one in the game world

        Vector3 target = pointPos - transform.position; //-Transform.position to find the position needed to rotate to

        target.Normalize(); //Make the vector have a magnitude of 1


        angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        /** 
         * Calculates the angle to rotate to by taking the y and z of the target and using
         * Atan2 to calculate the angle to the target and converting it from radians to degrees   
         * To acheive a target angle
         */
        //Debug.Log(angle);

        ang = angle;

        if (ang > 130 && ang < 179)
        {
            leftTran = true;
        }
        else if (ang > -179 && ang < -130)
        {
            leftTran = true;
        }
        else
        {
            leftTran = false;
        }
        pivot.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        /**
         * rotates the gameobject pivot which, is a parent of the weapon, since it's pos is set to
         * the middle of the player spirit. -90 is required since all the weapons are pointing up on start.
         */

        Debug.Log(pivot.transform.rotation);
    }


    public void DestroyGameObject() //Removes gameobject from scene
    {
        Destroy(gameObject);
    }

    public void setHealth(float nH)
    {
        this.health = nH;
    }

    public void setSpeed(float nSp)
    {
        this.acceleration = nSp;
    }

    public float getHealth()
    {
        return this.health;
    }

    public float getSpeed()
    {
        return this.acceleration;
    }

    public float getMaxHealth()
    {
        return this.maxHealth;
    }
    public void setMaxHealth(float nH)
    {
        this.maxHealth = nH;
    }
    /**
     * EffectDmg hits the player if the player has been hit with an effect
     */
    IEnumerator EffectDmg(float dmg, float delay, float nSpeed, int effect)
    {

        if (effect == 1)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 0.2f, 0.2f, 1f);
        }
        else if (effect == 2)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 0.8f, 0.4f, 1f);
        }
        else if (effect == 3)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color(0.4f, 1f, 0.4f, 1f);
        }

        acceleration = acceleration - nSpeed;
        yield return new WaitForSeconds(delay);
        health = health - dmg;
        GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

        acceleration = acceleration + nSpeed;

        effect = 0;
    }

    void Effect()
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

                break;

            case 0:
                break;
        }
    }

    public void SetResist(float chng, int effect)
    {
        switch (effect)
        {
            case 1:
                resistBleed = chng;
                break;
            case 2:
                resistBurn = chng;
                break;
            case 3:
                resistPos = chng;
                break;
        }
    }

    public void SpeedReset()
    {
        if (baseSpeed != acceleration)
        {
            acceleration = baseSpeed;

        }
        else if (baseSpeed < acceleration)
        {
            baseSpeed = acceleration;
        }

    }



    public void SetOOGaze(bool nState)
    {
        outOfGaze = nState;
    }

    public void ReadInStats(string path)
    {
        StreamReader streamReader = new StreamReader(path);

        maxHealth = float.Parse(streamReader.ReadLine());
        health = float.Parse(streamReader.ReadLine());
        acceleration = float.Parse(streamReader.ReadLine());
        resistPos = float.Parse(streamReader.ReadLine());
        resistBurn = float.Parse(streamReader.ReadLine());
        resistBleed = float.Parse(streamReader.ReadLine());

        streamReader.Close();

    }

    public void WriteStats(string path)
    {
        StreamWriter streamWriter = new StreamWriter(path);

        streamWriter.Write(maxHealth + "\n");
        streamWriter.Write(health + "\n");
        streamWriter.Write(acceleration + "\n");
        streamWriter.Write(resistPos + "\n");
        streamWriter.Write(resistBurn + "\n");
        streamWriter.Write(resistBleed + "\n");

        streamWriter.Close();

        //AssetDatabase.ImportAsset("PlStats");
    }
    /**
     * SwitchAnim deactivates the game object containing the previous gameobject with the specific animator attached for each weapon 
     * And activates the gameobject with the attached new weapon 
     */
    public void SwitchAnim(string nWepType)
    {
        string wepType;
        wepType = GetComponentInChildren<PlAttack>().GetWepName();

        switch (wepType)
        {
            case "Sword":
                sword.SetActive(false);
                break;
            case "Axe":
                axe.SetActive(false);
                break;
            case "Spear":
                spear.SetActive(false);
                break;
            case "Kopis":
                kopis.SetActive(false);
                break;
        }

        switch (nWepType)
        {
            case "Sword":
                sword.SetActive(true);
                plAnim = sword.GetComponent<Animator>();
                break;

            case "Axe":
                axe.SetActive(true);
                plAnim = axe.GetComponent<Animator>();
                break;

            case "Spear":
                spear.SetActive(true);
                plAnim = spear.GetComponent<Animator>();
                break;

            case "Kopis":
                kopis.SetActive(true);
                plAnim = kopis.GetComponent<Animator>();
                break;
        }

    }


}

