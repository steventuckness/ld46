using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource drums;
    public AudioSource bass;
    public AudioSource lead;
    public AudioSource pads;
    public AudioSource stabs;
    private Dictionary<Tracks, AudioSource> tracking;
    private float time = 0;

    public enum Tracks
    {
        Drums,
        Bass,
        Lead,
        Pads,
        Stabs
    }

    // Start is called before the first frame update
    void Start()
    {
        this.tracking = new Dictionary<Tracks, AudioSource>();
        this.tracking.Add(Tracks.Drums, this.drums);
        this.tracking.Add(Tracks.Bass, this.bass);
        this.tracking.Add(Tracks.Lead, this.lead);
        this.tracking.Add(Tracks.Pads, this.pads);
        this.tracking.Add(Tracks.Stabs, this.stabs);
    }

    public void MuteTrack(Tracks track)
    {
        AudioSource source = this.tracking[track];

        source.mute = true;
    }

    public void UnmuteTrack(Tracks track)
    {
        AudioSource source = this.tracking[track];

        source.mute = false;
    }

    public void ToggleTrack(Tracks track)
    {
        AudioSource source = this.tracking[track];
        Debug.Log("Is muted? " + source.mute);
        bool newVal = !source.mute;
        source.mute = !source.mute;
        Debug.Log(" Is muted? " + source.mute);
    }

}
