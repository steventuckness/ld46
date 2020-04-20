using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchPlayer : MonoBehaviour
{
    public AudioSource scratchSound1;
    public AudioSource scratchSound2;
    public AudioSource scratchSound3;
    private bool hasStopped = false;
    private AudioSource[] audioSources = new AudioSource[3];
    private AudioSource activeScratchSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSources[0] = scratchSound1;
        audioSources[1] = scratchSound2;
        audioSources[2] = scratchSound3;
        
        //Music music = GameObject.Find("Music").GetComponent<Music>();
        //music.MuteAllTracks();
        activeScratchSound = audioSources[Random.Range(0, 3)];
        activeScratchSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!activeScratchSound.isPlaying && !hasStopped)
        {
            //Music music = GameObject.Find("Music").GetComponent<Music>();
            //music.UnmuteAllTracks();
            hasStopped = true;
        }
    }
}
