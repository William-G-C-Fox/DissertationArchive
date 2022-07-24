using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private GameObject item;
    private Rigidbody2D chestRigBod;
    // Start is called before the first frame update
    void Start()
    {

        chestRigBod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        chestRigBod.velocity = Vector2.zero; //so it doesn't get pushed and continue moving
    }
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public void Open()
    {

        float rg = RetRanInt(10);

        switch (rg)
        {
            case 1:
                item = Instantiate(Resources.Load("HealthPot"), transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;

                break;
            case 2:
                item = Instantiate(Resources.Load("HealthBuff"), transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;

                break;
            case 3:
                item = Instantiate(Resources.Load("SpeedBuff"), transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;

                break;
            case 4:
                item = Instantiate(Resources.Load("Spear"), transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                break;
            case 5:
                item = Instantiate(Resources.Load("Labrys"), transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                break;
            case 6:
                item = Instantiate(Resources.Load("Xiphos"), transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                break;
            case 7:
                item = Instantiate(Resources.Load("Kopis"), transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                break;
            case 8:
                item = Instantiate(Resources.Load("BleedRest"), transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                break;
            case 9:
                item = Instantiate(Resources.Load("BurnRest"), transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                break;
            case 10:
                item = Instantiate(Resources.Load("PosRest"), transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                break;


        }

        DestroyGameObject();
    }

    float RetRanInt(float range)
    {
        float ret;

        ret = Mathf.Round(Random.Range(1, range));

        return ret;
    }
}
