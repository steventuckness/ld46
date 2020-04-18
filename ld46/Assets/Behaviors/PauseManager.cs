using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    GameObject[] pauseObjects;
    float oldTimescale;
    bool paused;

    // Start is called before the first frame update
    void Start()
    {
        oldTimescale = Time.timeScale;
        paused = false;
        pauseObjects = GameObject.FindGameObjectsWithTag("PauseObjects");
        Unpause();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(!paused) {
                // Game is not paused, so pause it
                Pause();
            } else {
                // Game is paused, so unpause it
                Unpause();
            }
        }
    }

    public void GoToScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    public void Unpause() {
        // hide elements
        foreach(GameObject g in pauseObjects) {
            g.SetActive(false);
        }
        // resume speed
        Time.timeScale = oldTimescale;
        paused = false;
    }

    public void Pause() {
        oldTimescale = Time.timeScale;
        Time.timeScale = 0;
        foreach(GameObject g in pauseObjects) {
            g.SetActive(true);
        }
        paused = true;
    }
}
