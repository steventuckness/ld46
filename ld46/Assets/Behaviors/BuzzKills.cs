﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuzzKills : MonoBehaviour
{
    public float buzzKillFreeDurationInSeconds = 60.0f;
    public int buzzKillRangeMax = 20000;
    public float buzzKillMessageLifeInSeconds = 2f;
    public float messageLetterAddDelayInSeconds = .5f;
    public GameObject metrics;
    public GameObject buzzKillNotificationSystemText;
    public int buzzKillIndex = -1;
    public string[] buzzKills = {
        "New club accross the street just opened!",
        "Someone fainted while dancing!",
        "A polar bear fight broke out!",
        "Your turntable is overheating!",
        "Speaker malfunction!",
        "The cops have been called!",
        "Global Warming!",
        "Mama bear showed up!"
    };
    public int buzzKillHypeSubtraction = 5;

    private float timer = 0.0f;
    private string currentBuzzKillMessage = string.Empty;
    private int currentBuzzKillMessageIterator = 0;
    private bool isBuzzKillInProgress = false;
    private float secondsSinceLastBuzzKillMessageLetterAdded = 0f;

    // Start is called before the first frame update
    void Start()
    {
        buzzKillNotificationSystemText.GetComponent<Text>().text = "";
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
            
        if (timer >= buzzKillFreeDurationInSeconds)
        {
            if (!this.isBuzzKillInProgress)
            {
                var buzzKill = Random.Range(buzzKillFreeDurationInSeconds, buzzKillRangeMax);

                if (buzzKill <= timer)
                {
                    this.GetComponent<SpriteRenderer>().enabled = true;
                    this.GetComponent<Animator>().enabled = true;
                    this.GetComponent<AudioSource>().Play();
                    this.buzzKillIndex = Random.Range(0, buzzKills.Length);
                    this.currentBuzzKillMessage = buzzKills[buzzKillIndex] + " (-" + buzzKillHypeSubtraction + " hype)";
                    this.isBuzzKillInProgress = true;
                }
            }
            else
            {
                secondsSinceLastBuzzKillMessageLetterAdded += Time.deltaTime;

                if (this.secondsSinceLastBuzzKillMessageLetterAdded >= this.messageLetterAddDelayInSeconds && currentBuzzKillMessageIterator <= currentBuzzKillMessage.Length)
                {
                    buzzKillNotificationSystemText.GetComponent<Text>().text = this.currentBuzzKillMessage.Substring(0, this.currentBuzzKillMessageIterator);
                    currentBuzzKillMessageIterator++;
                    this.secondsSinceLastBuzzKillMessageLetterAdded = 0f;
    
                    if (currentBuzzKillMessageIterator == currentBuzzKillMessage.Length)
                    {
                        metrics.GetComponent<Metrics>().Hype -= buzzKillHypeSubtraction;
                        StartCoroutine(continueShowingBuzzKillMessageForSeconds(buzzKillMessageLifeInSeconds));
                    }
                }
            }
        }
   }

   private IEnumerator continueShowingBuzzKillMessageForSeconds(float seconds) 
   {
        yield return new WaitForSeconds(seconds);
        this.secondsSinceLastBuzzKillMessageLetterAdded = 0f;
        this.currentBuzzKillMessage = string.Empty;
        buzzKillNotificationSystemText.GetComponent<Text>().text = this.currentBuzzKillMessage;
        this.currentBuzzKillMessageIterator = 0;
        this.isBuzzKillInProgress = false;
        this.buzzKillIndex = -1;
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<Animator>().enabled = false;
   }
}
