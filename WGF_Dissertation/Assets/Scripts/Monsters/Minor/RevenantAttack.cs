using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevenantAttack : MonsterAttack
{

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        dmg = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Turn(1);
    }




}
