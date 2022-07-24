using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeraAttack : MonsterAttack
{
    private Vector3 nPos;
    // Start is called before the first frame update
    void Start()
    {
        Pivot pivot = gameObject.GetComponentInParent(typeof(Pivot)) as Pivot;
        dmg = 1.5f;
        boxCol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Turn(1);
        Attack();
    }



}
