using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public string wName;
    public GameObject player;
    public float wType;
    public float dmg;
    public float speed;
    public float collY;
    public float collX;
    public int effect;

    // Start is called before the first frame update
    void Start()
    {
        GenEffect();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPickUp()
    {
        //sets all the player stats
        player.GetComponentInChildren<PlAttack>().SetDmg(dmg);
        player.GetComponentInChildren<PlAttack>().SetRange(collX, collY);
        player.GetComponentInChildren<PlAttack>().SetWTme(speed);
        player.GetComponentInChildren<PlAttack>().SetEffect(effect);
        player.GetComponentInChildren<PlAttack>().SetType(wType);
        player.GetComponentInChildren<PlayerController2D>().SwitchAnim(wName);
        Debug.Log(wName);
        player.GetComponentInChildren<PlAttack>().SetName(wName);

        DestroyGameObject();
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public void GenEffect()
    {

        int rand = Random.Range(0, 3);
        if (rand == 1)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 0.2f, 0.2f, 1f);
        }
        else if (rand == 2)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 0.8f, 0f, 1f);
        }
        else if (rand == 3)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.2f, 1f, 0.2f, 1f);
        }
        effect = rand;

    }
}
