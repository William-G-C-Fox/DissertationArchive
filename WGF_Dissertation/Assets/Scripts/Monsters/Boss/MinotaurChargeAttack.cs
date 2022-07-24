using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurChargeAttack : MonsterAttack
{
    public bool charge;
    public bool canCharge;
    // Start is called before the first frame update
    void Start()
    {
        canCharge = false;
        charge = false;
        StartCoroutine(ChargeUp(1));

    }


    // Update is called once per frame
    void Update()
    {
        Turn(0.5f);
        ResetCharge();
    }

    public Vector3 GetTargPos()
    {
        return setPlPos;
    }

    public IEnumerator ChargeUp(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            canCharge = true;
            yield return new WaitForSeconds(delay);
            canCharge = false;
            yield return new WaitForSeconds(delay);
        }

    }



    public bool GetCharge()
    {
        return charge;
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (canCharge == true)
            {
                charge = true;
            }
            else
            {
                charge = false;
            }

        }

    }

    public void ResetCharge()
    {
        if (canCharge == false)
        {
            charge = false;
        }
    }
}
