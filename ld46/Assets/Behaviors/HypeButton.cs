using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypeButton : MonoBehaviour
{
    public float fatigueIncreaseOnUse;
    public float fatigueDecreasePerSecond;
    public float hypeAddedOnUse;
    public int cost;
    public float duration;

    public bool playTrack;

    public bool isToggler;

    public Transform instantiate;

    public Music.Tracks track;

     float runtime;
    private bool isActive;

    private Transform instance;
    private float currentFatigue;

    void Start()
    {
        isActive = false;
        runtime = 0;
        currentFatigue = 0;
    }

    void Update()
    {
        if (currentFatigue > 0) {
            currentFatigue -= fatigueDecreasePerSecond * Time.deltaTime;
            if (currentFatigue < 0)
            {
                currentFatigue = 0;
            }
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
        var MetricsObject = GameObject.Find("Metrics").GetComponent<Metrics>();
        if (MetricsObject.currentMoney - cost <= 0) {
            Debug.Log("Not enough money!");
            return;
        }
        MetricsObject.DecreaseMoney(cost);
        MetricsObject.Hype += hypeAddedOnUse - currentFatigue;
        currentFatigue += fatigueIncreaseOnUse;
        if (isToggler && isActive) {
            DeactivateHype();
            return;
        }
        UnmuteTrack(track);
        PlayAudio();
        if (instantiate != null) {
            instance = Instantiate(instantiate);
        }
        isActive = true;
    }

    void PlayAudio() {
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
        Debug.Log("Deactivating");
        isActive = false;
        runtime = 0;
        MuteTrack(track);
        if (instance != null) {
            Destroy(instance.gameObject);
        }
    }

    void MuteTrack(Music.Tracks track) {
        if (!playTrack) { return; }
        Music music = GameObject.Find("Music").GetComponent<Music>();
        music.MuteTrack(track);
    }

    void UnmuteTrack(Music.Tracks track) {
        if (!playTrack) { return; }
        Music music = GameObject.Find("Music").GetComponent<Music>();
        music.UnmuteTrack(track);
    }
}
