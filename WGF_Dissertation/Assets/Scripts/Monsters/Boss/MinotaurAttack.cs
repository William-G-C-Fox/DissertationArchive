using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurAttack : MonsterAttack
{


    bool charge;

    // Start is called before the first frame update
    void Start()
    {
        dmg = 1;
        effect = 1;
        charge = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Turn(0);



    }


    public override void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") && charge == true)
        {
            StartCoroutine(Stuck(3));
        }
        else if (collision.CompareTag("Player") && charge == true)
        {
            player.GetComponent<PlayerController2D>().OnHit(1, 1);
            charge = false;
        }
        else if (collision.CompareTag("Player") && charge == false && canAttk == true)
        {
            player.GetComponent<PlayerController2D>().OnHit(1.5f, 0);

        }
    }

    public IEnumerator Stuck(float delay) //If the Minotaur hits a wall then the speed is set to zero
    {
        gameObject.GetComponentInParent<TheMinotaur>().SetSpeed(0);
        yield return new WaitForSeconds(delay);
        charge = false;
        gameObject.GetComponentInParent<TheMinotaur>().SetSpeed(0.5f);
    }

    public bool GetCharge()
    {
        return charge;
    }

    public Vector3 GetTargPos()
    {
        return setPlPos;
    }

    public void setCharge(bool nCharge)
    {
        charge = nCharge;
    }


}
