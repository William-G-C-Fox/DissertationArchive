using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAttack : MonsterAttack
{
    public CircleCollider2D circCol;

    // Start is called before the first frame update
    void Start()
    {
        dmg = 1;
        effect = 3;
        canAttk = true;
        circCol = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }




}
