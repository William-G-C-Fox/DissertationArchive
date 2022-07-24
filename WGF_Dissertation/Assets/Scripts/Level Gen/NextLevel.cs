using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private BoxCollider2D boxCol;
    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController2D>().WriteStats("Assets/Resources/PlStats.txt");
            collision.GetComponentInChildren<PlAttack>().WriteStats("Assets/Resources/PlAttkStats.txt");
            SceneManager.LoadScene("Level", LoadSceneMode.Single);
        }
    }
}
