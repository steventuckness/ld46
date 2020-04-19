﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Metrics : MonoBehaviour
{
    const float HYPE_CAP = 100f;

    private GameObject hourTextObject;
    private GameObject moneyTextObject;

    private string currentHourText = string.Empty;
    public static float currentHour = 0;
    private float hourIncrementTimeInterval = 10f; //in seconds
    private float hourIncrementTimer = 0.0f; //tracks time since last hour increment

    private string currentMoneyText = string.Empty;
    public float currentMoney;
    public float startingMoney = 0;
    //public float moneyIncrementStartDelay = 0;
    public float moneyIncrementTimeInterval = 1f;
    private float moneyIncrementTimer = 0.0f;

    public float startingHype = 31f;
    float hype;
    public float hypeDecrementFactor = 10f;
    private float hypeDecrementStartDelay = 5f;
    private float hypeDecrementTimeInterval = 2.5f;

    /**
     * Start is called before the first frame update
     *
     * Sets up a call to {IncrementMoney} method once after {moneyIncrementStartDelay} seconds,
     * and then every {moneyIncremenetTimeInterval} second(s).
     *
     * Sets up a call to {DecrementHype} method once after {hypeDecrementStartDelay} seconds,
     * and then every {hypeDecrementTimeInterval} second(s);
     */
    void Start()
    {
        Debug.Log("Started Metrics in " + SceneManager.GetActiveScene().name);

        hourTextObject = GameObject.Find("HourText");
        moneyTextObject = GameObject.Find("MoneyText");

        // Only start everything up if we're in the main scene
        if ( SceneManager.GetActiveScene().name == "MainScene") {
            hype = startingHype;

            currentMoney = startingMoney;
            UpdateMoneyText();

            currentHour = 1;
            UpdateHourText();

            //InvokeRepeating("IncrementMoney", moneyIncrementStartDelay, moneyIncrementTimeInterval);
            //Debug.Log("Calling IncrementMoney every " + moneyIncrementTimeInterval + " seconds");

            InvokeRepeating("DecrementHype", hypeDecrementStartDelay, hypeDecrementTimeInterval);
            Debug.Log("Calling DecrementHype every " + hypeDecrementTimeInterval + " seconds");
        }
    }

    // Update is called once per frame
    void Update()
    {
        hourIncrementTimer += Time.deltaTime;
        if (hourIncrementTimer >= hourIncrementTimeInterval)
        {
            IncrementHour();
            hourIncrementTimer = 0;
        }

        moneyIncrementTimer += Time.deltaTime;
        if (moneyIncrementTimer >= moneyIncrementTimeInterval)
        {
            IncrementMoney();
            moneyIncrementTimer = 0;
        }

    }

    void UpdateHourText()
    {
        currentHourText = "Hour " + currentHour;
        hourTextObject.GetComponent<Text>().text = currentHourText;
    }

    void IncrementHour()
    {
        currentHour++;
        UpdateHourText();
    }

    public float GetCurrentHour()
    {
        return currentHour;
    }

    void UpdateMoneyText()
    {
        currentMoneyText = "Money: " + currentMoney;
        moneyTextObject.GetComponent<Text>().text = currentMoneyText;
    }

    void IncrementMoney()
    {
        currentMoney += Mathf.Ceil(hype / 4);
        UpdateMoneyText();
        Debug.Log("Money value: " + currentMoney);
    }

    public void DecreaseMoney(int decreaseAmount)
    {
        currentMoney -= decreaseAmount;
        UpdateMoneyText();
    }

    public float GetCurrentMoney()
    {
        return currentMoney;
    }

    // Called periodically
    void DecrementHype()
    {
        hype -= (hype == 0) ? 0 : hypeDecrementFactor;
        // Debug.Log("Hype value: " + hype);
        if(hype <= 0 ) {
            CancelInvoke();
            SceneManager.LoadScene("Scenes/end-game");
        }
    }

    public void AddHype(float delta) {
        if(hype + delta > HYPE_CAP) {
            hype = HYPE_CAP;
        } else {
            hype += delta;
        }
    }

    public void SubtractHype(float delta) {
        if(hype - delta <= 0) {
            CancelInvoke();
            SceneManager.LoadScene("Scenes/end-game");
        } else {
            hype -= delta;
        }
    }

    public float GetHype() {
        return hype;
    }
}
