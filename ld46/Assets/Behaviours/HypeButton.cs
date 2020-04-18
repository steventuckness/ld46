using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypeButton : MonoBehaviour
{
    public float fatigueIncrease;
    public float currentFatigue;
    public float fatigueCooldown;
    public float hypeFactor;
    public int cost;
    public string trackName;
    public AudioSource playAudio; 
    public float duration;

    public GameObject metrics;
    public GameObject soundSystem;

    private float runtime;
    private bool isActive;

    void Start()
    {
        isActive = false;
        runtime = 0;
        if (metrics == null) {
            Debug.LogError("HypeButton has no associated metrics object");
        }
    }

    void Update()
    {
        if (currentFatigue > 0) {
            currentFatigue -= fatigueCooldown / Time.deltaTime;
        }
        if (isActive) {
            runtime += Time.deltaTime;
        }
        if (runtime >= duration) {
            DeactivateHype();
        }
    }

    void ActivateHype()
    {
        var m = metrics.GetComponent<Metrics>(); 
        if (m.money - cost <= 0) { return; }
        m.money -= cost; 
        m.hype += hypeFactor - currentFatigue;
        currentFatigue += fatigueIncrease;
        if (trackName != "" && soundSystem != null) {
            var s = soundSystem.GetComponent<SoundSystem>();
            s.UnmuteTrack(trackName);
        }
        if (playAudio != null) {
            playAudio.Play();
        }
    }

    void DeactivateHype() {
        isActive = false;
        runtime = 0;
        if (playAudio != null) {
            playAudio.Stop();
        }
        if (trackName != "" && soundSystem != null) {
            var s = soundSystem.GetComponent<SoundSystem>();
            s.MuteTrack(trackName);
        }
    }
}
