using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnmuteTrack(string trackName) {
        Debug.Log("Unmuting track " + trackName);
    }

    public void MuteTrack(string trackName) {
        Debug.Log("Muting track " + trackName);
    }
}
