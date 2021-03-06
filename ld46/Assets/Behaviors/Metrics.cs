﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Metrics : MonoBehaviour
{
    const float HYPE_CAP = 100;

    public Transform crowdBooObj;
    public Transform crowdCheerObj;

    private GameObject hourTextObject;
    private GameObject moneyTextObject;

    private string currentHourText = string.Empty;
    public static float currentHour = 0;
    private float hourIncrementTimeInterval = 5f; //in seconds
    private float hourIncrementTimer = 0.0f; //tracks time since last hour increment

    private string currentMoneyText = string.Empty;
    public float currentMoney;
    public float startingMoney = 0;
    //public float moneyIncrementStartDelay = 0;
    public float moneyIncrementTimeInterval = 2f;
    private float moneyIncrementTimer = 0.0f;
    public static float totalRevenue;

    public float startingHype = 41f;
    float hype;
    public float hypeDecrementFactor = 8f;
    private float hypeDecrementStartDelay = 10f;
    private float hypeDecrementTimeInterval = 2.5f;

    public event Action<float> HypeUpdated = delegate { };

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

        hype = startingHype;
        HypeUpdated.Invoke(hype);

        currentMoney = startingMoney;
        UpdateMoneyText();
        totalRevenue = 0;

        currentHour = 1;
        UpdateHourText();

        //InvokeRepeating("IncrementMoney", moneyIncrementStartDelay, moneyIncrementTimeInterval);
        //Debug.Log("Calling IncrementMoney every " + moneyIncrementTimeInterval + " seconds");

        InvokeRepeating("DecrementHype", hypeDecrementStartDelay, hypeDecrementTimeInterval);
        Debug.Log("Calling DecrementHype every " + hypeDecrementTimeInterval + " seconds");

        HypeUpdated += value => {
            if (hype <= 0)
            {
                CancelInvoke();
                SceneManager.LoadScene("Scenes/end-game");
            }
        };
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
        currentMoneyText = "$ " + currentMoney;
        moneyTextObject.GetComponent<Text>().text = currentMoneyText;
    }

    void IncrementMoney()
    {
        float moneyIncrease = Mathf.Ceil(hype / 5);
        totalRevenue += moneyIncrease;
        currentMoney += moneyIncrease;
        UpdateMoneyText();
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

    //Called periodically, separate so that the crowdBoo isn't used for it
    void DecrementHype()
    {
        hype -= hypeDecrementFactor;
        HypeUpdated.Invoke(hype);
    }
    //Called by buzzkills, separate so that the crowdBoo isn't used for it
    public void HypeBuzzkill(int amount)
    {
        hype -= amount;
        HypeUpdated.Invoke(hype);
    }

    public float Hype
    {
        get { return hype; }
        set
        {
            if (hype == value) return;
            else if (value - hype >= 10)
            {
                Instantiate(crowdCheerObj);
            }
            else if (value - hype <= -5)
            {
                Instantiate(crowdBooObj);
            }
            hype = Math.Min(value, HYPE_CAP);
            hype = Math.Max(hype, 0);
            HypeUpdated.Invoke(hype);
        }
    }
}
