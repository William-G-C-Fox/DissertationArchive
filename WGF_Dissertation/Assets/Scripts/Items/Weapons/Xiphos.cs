using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xiphos : WeaponBase
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wName = "Sword";
        dmg = 1;
        speed = 0.75f;
        collY = 1;
        collX = 1.5f;
        wType = 0;
        GenEffect();
    }

    // Update is called once per frame
    void Update()
    {

    }



}
