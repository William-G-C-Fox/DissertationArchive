using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject player;
    public float health;
    public float speed;
    public float strength;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setHealth(float nH)
    {
        this.health = nH;
    }

    public void setSpeed(float nSp)
    {
        this.speed = nSp;
    }

    public void setStrength(float nStr)
    {
        this.strength = nStr;
    }

    public virtual void OnPickUp()
    {

    }
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

}
