﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuzzKills : MonoBehaviour
{
    public float buzzKillFreeDuration = 60.0f;
    public int buzzKillRangeMax = 20000;
    // public GameObject metrics;

    public GameObject buzzKillNotificationSystemText;
    public string[] buzzKills = { "message", "cost" };

    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= buzzKillFreeDuration)  
        {
            var buzzKill = Random.Range(buzzKillFreeDuration, buzzKillRangeMax);
            if (buzzKill <= timer) 
            {
                var buzzKillIndex = Random.Range(0, buzzKills.Length);
                Debug.Log("buzzkill: " + buzzKills[buzzKillIndex]);

                if (buzzKills[buzzKillIndex] == "message")
                {
                    buzzKillNotificationSystemText.GetComponent<Text>().text = "super buzz kill";
                }
            }
        }     
    }
}