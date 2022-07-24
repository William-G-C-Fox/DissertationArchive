using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : WeaponBase
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wName = "Spear";
        dmg = 1;
        speed = 1;
        collY = 0.5f;
        collX = 2;
        wType = 2;
        GenEffect();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
