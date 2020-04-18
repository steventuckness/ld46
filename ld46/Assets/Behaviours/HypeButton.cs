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
    public float duration;
    public SoundSystem.Tracks tracks;

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

    void OnMouseDown() {
        ActivateHype();
    }

    void ActivateHype()
    {
        Debug.Log("Hyped!");
        var m = metrics.GetComponent<Metrics>(); 
        if (m.money - cost <= 0) { return; }
        m.money -= cost; 
        m.hype += hypeFactor - currentFatigue;
        currentFatigue += fatigueIncrease;
        if (trackName != "" && soundSystem != null) {
            var s = soundSystem.GetComponent<SoundSystem>();
            s.UnmuteTrack(trackName);
        }
        PlayAudio();
    }

    void PlayAudio() {
        var audio = this.GetComponent<AudioSource>();
        if (audio != null) {
            audio.Play();
        }
    }

    void StopAudio() {
        var audio = this.GetComponent<AudioSource>();
        if (audio != null) {
            audio.Stop();
        }

    }

    void DeactivateHype() {
        isActive = false;
        runtime = 0;
        if (trackName != "" && soundSystem != null) {
            var s = soundSystem.GetComponent<SoundSystem>();
            s.MuteTrack(trackName);
        }
    }
}
