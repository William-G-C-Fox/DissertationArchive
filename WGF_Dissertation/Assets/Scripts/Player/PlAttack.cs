using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class PlAttack : MonoBehaviour
{
    private BoxCollider2D boxCol;
    public string wepName;
    public float dmg;

    private float waitTime;

    public bool canAttk, lcDown;
    private float wepType;
    public int effect;
    private float ang;
    public bool attacking;
    string path = "Assets/Resources/PlAttkStats.txt";
    // Start is called before the first frame update




    // Start is called before the first frame update
    void Start()
    {

        boxCol = GetComponent<BoxCollider2D>();
        canAttk = true;
        ReadInStats(path);
        attacking = false;
        Debug.Log(dmg);
    }

    // Update is called once per frame
    void Update()
    {



    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //CompareTag compares the unity tag of the collisions
        if (collision.CompareTag("Mob"))
        {


            //timerRunning = true;


            if (Input.GetMouseButtonDown(0))
            {


                collision.GetComponentInParent<Monster>().OnHit(dmg, effect);
                attacking = true;

            }





        }

        if (collision.CompareTag("Chest"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                collision.GetComponent<Chest>().Open();
            }
        }

        if (collision.CompareTag("Item"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                collision.GetComponent<Item>().OnPickUp();

            }
        }

        if (collision.CompareTag("Weapon"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                collision.GetComponent<WeaponBase>().OnPickUp();

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        attacking = false;
    }

    public void SetDmg(float setDmg)
    {
        this.dmg = setDmg;
    }

    public void SetWTme(float speed)
    {
        this.waitTime = speed;
    }

    public void SetRange(float collX, float collY)
    {
        transform.localScale = new Vector3(collX, collY, 0);
    }

    public void SetType(float type)
    {
        this.wepType = type;
    }

    public void SetEffect(int setEffect)
    {
        this.effect = setEffect;
    }

    public void SetName(string nName)
    {
        name = nName;
    }
    public string GetWepName()
    {
        return wepName;
    }

    public void ReadInStats(string path)
    {
        StreamReader streamReader = new StreamReader(path);

        dmg = float.Parse(streamReader.ReadLine());
        effect = int.Parse(streamReader.ReadLine());
        waitTime = float.Parse(streamReader.ReadLine());
        float x = float.Parse(streamReader.ReadLine());
        float y = float.Parse(streamReader.ReadLine());
        wepName = streamReader.ReadLine();
        Debug.Log(wepName);
        SetRange(x, y);

        streamReader.Close();

    }
    public void WriteStats(string path)
    {
        StreamWriter streamWriter = new StreamWriter(path);

        streamWriter.Write(dmg + "\n");
        streamWriter.Write(effect + "\n");
        streamWriter.Write(waitTime + "\n");
        streamWriter.Write(transform.localScale.x + "\n");
        streamWriter.Write(transform.localScale.y + "\n");
        streamWriter.Write(name + "\n");

        streamWriter.Close();

        //AssetDatabase.ImportAsset("PlAttkStats");
    }

    public bool GetCanAttk()
    {
        return canAttk;
    }


}
