using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kopis : WeaponBase
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wName = "Kopis";
        dmg = 1;
        speed = 1.25f;
        collY = 1;
        collX = 2f;
        wType = 0;
        effect = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
