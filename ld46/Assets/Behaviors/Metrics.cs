using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Metrics : MonoBehaviour
{
    const float HYPE_CAP = 100f;

    public GameObject hourTextObject;
    public GameObject moneyTextObject;

    public string currentHourText = string.Empty;
    public static float currentHour = 0;
    public float hourIncrementTimeInterval = 10f; //in seconds
    private float hourIncrementTimer = 0.0f; //tracks time since last hour increment

    public string currentMoneyText = string.Empty;
    public float currentMoney;
    public float startingMoney = 0;
    public float moneyIncrementStartDelay = 0;
    public float moneyIncrementTimeInterval = 1f;
    private float moneyIncrementTimer = 0.0f;

    public float startingHype = 200;
    float hype;
    public float hypeDecrementFactor = 50;
    public float hypeDecrementStartDelay = 0;
    public float hypeDecrementTimeInterval = 0.5f;

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

        // Only start everything up if we're in the main scene
        if( SceneManager.GetActiveScene().name == "MainScene") {
            hype = startingHype;

            currentMoney = startingMoney;

            currentHour = 1;
            hourTextObject.GetComponent<Text>().text = "Hour 1";

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
            currentHour++;
            currentHourText = "Hour " + currentHour;
            hourTextObject.GetComponent<Text>().text = currentHourText;
            hourIncrementTimer = 0;
        }

        moneyIncrementTimer += Time.deltaTime;
        if (moneyIncrementTimer >= moneyIncrementTimeInterval)
        {
            IncrementMoney();
            currentMoneyText = "Money: " + currentMoney;
            moneyTextObject.GetComponent<Text>().text = currentMoneyText;
            moneyIncrementTimer = 0;
        }

    }

    void IncrementMoney()
    {
        currentMoney += hype;
        // Debug.Log("Money value: " + money);
    }

    // Called periodically
    void DecrementHype()
    {
        hype -= (hype == 0) ? 0 : hypeDecrementFactor;
        // Debug.Log("Hype value: " + hype);
        if(hype == 0 ) {
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
