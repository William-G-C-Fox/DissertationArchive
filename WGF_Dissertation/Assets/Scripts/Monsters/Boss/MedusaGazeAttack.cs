using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaGazeAttack : MonsterAttack
{
    bool gaze;
    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        GetComponentInChildren<MedusaGazeAttack>().BoxEnabled();
    }

    // Update is called once per frame
    void Update()
    {
        Turn(0.25f);
    }

    private void Awake()
    {
        StartCoroutine(GazeAttack(5));
    }

    public IEnumerator GazeAttack(float delay)
    {

        while (true)
        {

            canAttk = false;
            yield return new WaitForSeconds(delay);
            canAttk = true;
            yield return new WaitForSeconds(delay);

        }

    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (canAttk == true)
            {
                if (collision.GetComponent<PlayerController2D>().getSpeed() > 5)
                {
                    collision.GetComponent<PlayerController2D>().SetOOGaze(false);
                    collision.GetComponent<PlayerController2D>().setSpeed(collision.GetComponent<PlayerController2D>().getSpeed() - 1);
                }
            }


        }

    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerController2D>().SetOOGaze(true);
        }
    }




}
