using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : Item
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        speed = 10;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnPickUp()
    {
        float plSpeed = player.GetComponent<PlayerController2D>().getSpeed();

        player.GetComponent<PlayerController2D>().setSpeed(plSpeed + speed);
        DestroyGameObject();




    }
}
