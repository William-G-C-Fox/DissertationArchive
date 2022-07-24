using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaAttack : MonsterAttack
{

    private Vector3 attRange;



    public bool inColl;
    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponentInChildren<BoxCollider2D>();
        inColl = false;
        canAttk = true;
        dmg = 1;
        effect = 0;
        attkRate = 1;

    }
    private void Awake()
    {
        //StartCoroutine(CanAttk(1));
    }
    // Update is called once per frame
    void Update()
    {
        Attack();
        // Debug.Log(inColl);

        Turn(0);
    }



    void NormalRange()
    {
        boxCol.size = new Vector3(2, 1, 0);
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            inColl = true;

        }

    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inColl = false;
        }
    }





}

