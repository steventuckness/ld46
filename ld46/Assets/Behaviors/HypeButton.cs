using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HypeButton : MonoBehaviour
{
    public string hypeButtonName;
    public GameObject buttonExplainTextObject;
    
    public float fatigueIncreaseOnUse;
    public float fatigueDecreasePerSecond;
    public float hypeAddedOnUse;
    public int cost;
    public float duration;
    public bool playTrack;
    public bool isToggler;
    public Transform instantiate;
    public Music.Tracks track;

    private bool isActive;
    private Transform instance;
    private float currentFatigue;
    private float runtime;

    void Start()
    {
        buttonExplainTextObject = GameObject.Find("ButtonExplainText");
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
        if (runtime != 0 && runtime >= duration && duration > 0) {
            DeactivateHype();
        }
    }

    void OnMouseDown() {
        ToggleHype();
    }

    void ToggleHype()
    {
        if (isToggler) {
            if (isActive) {
                DeactivateHype();
                return;
            } else {
                ActivateHype();
                return;
            }
        }
        ActivateHype();
    }

    void ActivateHype() {
        var MetricsObject = GameObject.Find("Metrics").GetComponent<Metrics>();
        if (MetricsObject.currentMoney - cost <= 0) {
            Debug.Log("Not enough money!");
            return;
        }
        MetricsObject.DecreaseMoney(cost);
        MetricsObject.Hype += hypeAddedOnUse - currentFatigue;
        currentFatigue += fatigueIncreaseOnUse;
        PlayAudio();
        if (instantiate != null) {
            instance = Instantiate(instantiate);
        }
        isActive = true;
        if (isToggler) {
            ToggleTrack(track);
        } else {
            UnmuteTrack(track);
        }
    }

    void DeactivateHype() {
        isActive = false;
        runtime = 0;
        if (isToggler) {
            ToggleTrack(track);
        } else {
            MuteTrack(track);
        }
        if (instance != null) {
            Destroy(instance.gameObject);
        }
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

    void ToggleTrack(Music.Tracks track) {
        Debug.Log("Toggling track");
        if (!playTrack) { return; }
        Music music = GameObject.Find("Music").GetComponent<Music>();
        music.ToggleTrack(track);
    }

    public void OnMouseEnter()
    {
        buttonExplainTextObject.GetComponent<Text>().text = hypeButtonName + " - $" + cost;
    }


}
