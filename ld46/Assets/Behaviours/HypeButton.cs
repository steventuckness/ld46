using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperButton : MonoBehaviour
{
    public float fatigueIncreasePerActivation;
    public float currentFatigue;
    public float fatigueDecreaseSpeed;
    public int hypeFactor;
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
        if (m.money - cost <= 0) { return; }
        var m = metrics.GetComponent<Metrics>(); 
        m.money -= cost; 
        m.hype += hypeFactor - currentFatigue;
        currentFatigue += fatigueIncreasePerActivation;
        if (trackName != "") {
            var s = soundSystem.GetComponent<Sound>();
            s.UnmuteTrack(trackName);
        }
    }
}
