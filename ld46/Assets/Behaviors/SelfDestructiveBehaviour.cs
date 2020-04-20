using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructiveBehaviour : MonoBehaviour
{
    private float timer;
    public float timeToLive = 10; //in seconds
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToLive)
        {
            Object.Destroy(this.gameObject);
        }
    }
}
