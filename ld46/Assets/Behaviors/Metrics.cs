using UnityEngine;

public class Metrics : MonoBehaviour
{
    public float money = 0;
    public float moneyIncrementStartDelay = 0;
    public float moneyIncrementTimeInterval = 1f;

    public float hype = 0;
    public float hypeDecrementFactor = 1;
    public float hypeDecrementStartDelay = 0;
    public float hypeDecrementTimeInterval = 1f;

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
        Debug.Log("Started Metrics");

        InvokeRepeating("IncrementMoney", moneyIncrementStartDelay, moneyIncrementTimeInterval);
        Debug.Log("Calling IncrementMoney every " + moneyIncrementTimeInterval + " seconds");

        InvokeRepeating("DecrementHype", hypeDecrementStartDelay, hypeDecrementTimeInterval);
        Debug.Log("Calling DecrementHype every " + hypeDecrementTimeInterval + " seconds");
    }

    // Update is called once per frame
    void Update()
    {
    }

    void IncrementMoney()
    {
        money += hype;
        Debug.Log("Money value: " + money);
    }

    void DecrementHype()
    {
        hype -= (hype == 0) ? 0 : hypeDecrementFactor;
        Debug.Log("Hype value: " + hype);
    }
}
