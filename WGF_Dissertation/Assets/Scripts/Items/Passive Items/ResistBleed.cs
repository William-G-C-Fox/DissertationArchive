using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistBleed : Item
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnPickUp()
    {
        player.GetComponent<PlayerController2D>().SetResist(0.5f, 2);
    }
}
