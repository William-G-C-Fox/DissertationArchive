using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLSpawner : MonoBehaviour
{
    private Transform spTran;
    public GameObject pl;
    // Start is called before the first frame update
    void Start()
    {

        pl = Instantiate(Resources.Load<GameObject>("Player"), transform.position, transform.rotation) as GameObject; //Loads in the player gameobject
        pl.gameObject.tag = "Player";


    }

    // Update is called once per frame
    void Update()
    {

    }
}
