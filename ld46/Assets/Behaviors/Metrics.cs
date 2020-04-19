using UnityEngine;
using UnityEngine.SceneManagement;

public class Metrics : MonoBehaviour
{
    const float HYPE_CAP = 100f;

    public float money = 0;
    public float moneyIncrementStartDelay = 0;
    public float moneyIncrementTimeInterval = 1f;

    public float startingHype = 0;
    float hype = 0;
    public float hypeDecrementFactor = 1;
    public float hypeDecrementStartDelay = 0;
    public float hypeDecrementTimeInterval = 1f;

    public static float hour = 0; // TODO: If we need to pass more vars between scenes, make a static vars script instead
    public float hourIncrementStartDelay = 0;
    public float hourIncrementTimeInterval = 5f;

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
            hour = 0;

            InvokeRepeating("IncrementMoney", moneyIncrementStartDelay, moneyIncrementTimeInterval);
            Debug.Log("Calling IncrementMoney every " + moneyIncrementTimeInterval + " seconds");

            InvokeRepeating("DecrementHype", hypeDecrementStartDelay, hypeDecrementTimeInterval);
            Debug.Log("Calling DecrementHype every " + hypeDecrementTimeInterval + " seconds");

            InvokeRepeating("IncrementHour", hourIncrementStartDelay, hourIncrementTimeInterval);
            Debug.Log("Calling IncrementHour every " + hourIncrementTimeInterval + " seconds");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void IncrementMoney()
    {
        money += hype;
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

    void IncrementHour()
    {
        hour += 1;
        Debug.Log("Hour " + hour);
    }
}
