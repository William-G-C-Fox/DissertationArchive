using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject mob;
    // Start is called before the first frame update
    void Start()
    {
        //Creates three monsters at the specificed position with +0.5 y
        SpawnMinor();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SpawnMinor()
    {
        int random = Random.Range(1, 5);

        switch (random)
        {
            case 1:
                mob = Instantiate(Resources.Load<GameObject>("Revenant"), transform.position, transform.rotation) as GameObject;
                break;
            case 2:
                mob = Instantiate(Resources.Load<GameObject>("Harpy"), transform.position, transform.rotation) as GameObject;
                break;
            case 3:
                mob = Instantiate(Resources.Load<GameObject>("Spartae"), transform.position, transform.rotation) as GameObject;
                break;
            case 4:
                mob = Instantiate(Resources.Load<GameObject>("StymphalianBirds"), transform.position, transform.rotation) as GameObject;
                break;
        }
    }
}
