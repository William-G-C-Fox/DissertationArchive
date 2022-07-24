using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpartaeAttack : MonsterAttack
{
    // Start is called before the first frame update
    void Start()
    {
        dmg = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Turn(1);
        Attack();
    }
}
