using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTracker : MonoBehaviour
{
    PlayerController2D player;
    private float amount;
    
  
    public Text health;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerController2D player = GetComponentInParent<PlayerController2D>();
        
        
        
       
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController2D player = GetComponentInParent<PlayerController2D>();
        amount = player.getHealth();
        health.text = amount.ToString();
    }

    

 
}
