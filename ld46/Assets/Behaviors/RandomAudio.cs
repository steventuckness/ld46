using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RandomAudio : MonoBehaviour
{
    private AudioSource[] audioSources;
    private AudioSource activeAudioSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = this.GetComponents<AudioSource>();
        activeAudioSound = audioSources[Random.Range(0, audioSources.Length)];
        activeAudioSound.Play();
    }

}
