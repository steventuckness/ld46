using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource drums;
    public AudioSource bass;
    private Dictionary<Tracks, AudioSource> tracking;
    private float time = 0;

    public enum Tracks
    {
        Drums,
        Bass
    }

    // Start is called before the first frame update
    void Start()
    {
        this.tracking = new Dictionary<Tracks, AudioSource>();
        this.tracking.Add(Tracks.Drums, this.drums);
        this.tracking.Add(Tracks.Bass, this.bass);
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
}
