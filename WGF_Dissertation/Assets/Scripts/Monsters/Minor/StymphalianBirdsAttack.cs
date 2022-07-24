using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StymphalianBirdsAttack : MonsterAttack
{

    // Start is called before the first frame update
    void Start()
    {
        dmg = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Turn(1);
        Attack();
    }


}
