using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : WeaponBase
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wName = "Axe";
        dmg = 1;
        speed = 2;
        collY = 2;
        collX = 1.5f;
        wType = 1;
        GenEffect();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
