using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypeButton : MonoBehaviour
{
    public float fatigueIncrease;
    public float currentFatigue;
    public float fatigueDecreaseSpeed;
    public float hypeFactor;
    public int cost;
    public string trackName;

    public GameObject metrics;
    public GameObject soundSystem;

    void Start()
    {
    }

    void Update()
    {
        if (currentFatigue > 0) {
            currentFatigue = currentFatigue - (fatigueDecreaseSpeed / Time.deltaTime);
        }
    }

    void ActivateHype()
    {
        var m = metrics.GetComponent<Metrics>(); 
        if (m.money - cost <= 0) { return; }
        m.money -= cost; 
        m.hype += hypeFactor - currentFatigue;
        currentFatigue += fatigueIncrease;
        if (trackName != "") {
            var s = soundSystem.GetComponent<Sound>();
            s.UnmuteTrack(trackName);
        }
    }
}
