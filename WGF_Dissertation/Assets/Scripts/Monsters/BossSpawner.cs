using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        SpawnBoss();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SpawnBoss()
    {
        int random = Random.Range(1, 4);

        switch (random)
        {
            case 1:
                boss = Instantiate(Resources.Load<GameObject>("Minotaur"), transform.position, transform.rotation) as GameObject;
                break;
            case 2:
                boss = Instantiate(Resources.Load<GameObject>("Medusa"), transform.position, transform.rotation) as GameObject;
                break;
            case 3:
                boss = Instantiate(Resources.Load<GameObject>("Chimera"), transform.position, transform.rotation) as GameObject;
                break;
        }
    }
}
