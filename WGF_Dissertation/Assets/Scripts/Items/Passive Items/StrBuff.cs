using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrBuff : Item
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

        player.GetComponent<PlayerController2D>().setMaxHealth(maxHealth + health);
        DestroyGameObject();




    }
}
