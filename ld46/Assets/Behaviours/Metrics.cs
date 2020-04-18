using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metrics : MonoBehaviour
{
    public float money;
    public float hype;

    private float counter;

    // Start is called before the first frame update
    void Start()
    {
       counter = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        // Prints out money and hype every second
        counter += Time.deltaTime;
        if (counter > 1) {
            //Debug.Log("Money: " + money);        
            //Debug.Log("Hype: " + hype);
            counter = 0;
        }
    }
}
