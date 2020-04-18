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
    public float duration;
    public Music.Tracks track;

    private float runtime;
    private bool isActive;

    void Start()
    {
        isActive = false;
        runtime = 0;
    }

    void Update()
    {
        if (currentFatigue > 0) {
            currentFatigue -= fatigueCooldown / Time.deltaTime;
        }
        if (isActive) {
            runtime += Time.deltaTime;
        }
        if (runtime != 0 && runtime >= duration) {
            DeactivateHype();
        }
    }

    void OnMouseDown() {
        ActivateHype();
    }

    void ActivateHype()
    {
        var m = GameObject.Find("Metrics").GetComponent<Metrics>(); 
        if (m.money - cost <= 0) { 
            Debug.Log("Not enough money!");
            return; 
        }
        m.money -= cost; 
        m.hype += hypeFactor - currentFatigue;
        currentFatigue += fatigueIncrease;
        UnmuteTrack(track);
        PlayAudio();
    }

    void PlayAudio() {
        Debug.Log("Playing Audio");
        var audio = this.GetComponent<AudioSource>();
        Debug.Log(audio);
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
        MuteTrack(track);
    }

    void MuteTrack(Music.Tracks track) {
        Music music = GameObject.Find("Music").GetComponent<Music>();
        music.MuteTrack(track);
    }

    void UnmuteTrack(Music.Tracks track) {
        Music music = GameObject.Find("Music").GetComponent<Music>();
        music.UnmuteTrack(track);
    }
}
