using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPot : Item
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnPickUp()
    {
        float maxHealth = player.GetComponent<PlayerController2D>().getMaxHealth();
        float currentH = player.GetComponent<PlayerController2D>().getHealth();

        if (maxHealth == currentH)
        {
            Debug.Log("Max Health");
            DestroyGameObject();
        }
        else
        {
            player.GetComponent<PlayerController2D>().setHealth(currentH + health);
            DestroyGameObject();
        }



    }

}
