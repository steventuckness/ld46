using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypeMeter : MonoBehaviour
{
    const float blinkTimeout = .3f;
    public GameObject metrics;

    int currentSpriteNumber;
    float blinkTimer = blinkTimeout;

    const string spriteFilepath = "hype-meter-spritesheet";

    // Start is called before the first frame update
    void Start()
    {
        int hypeSpriteNumber = GetSpriteIndex();
        SetSpriteByIndex(hypeSpriteNumber);
        currentSpriteNumber = hypeSpriteNumber;
    }

    // Update is called once per frame
    void Update()
    {
        int hypeSpriteNumber = GetSpriteIndex();
        int hype = (int) metrics.GetComponent<Metrics>().Hype;
        if( hype < 5 ) {
            // Under 5 hype, blink last bar, alternating between sprite 0 and 1
            blinkTimer -= Time.deltaTime;
            if( blinkTimer < 0 ) {
                // timer up, swap sprites and reset timer
                int newSpriteIndex = (currentSpriteNumber == 1) ? 0 : 1;
                SetSpriteByIndex(newSpriteIndex);
                currentSpriteNumber = newSpriteIndex;
                // Reset timer
                blinkTimer = blinkTimeout;
            }
        } else if(hypeSpriteNumber != currentSpriteNumber) {
            // Over 5 hype, just use the appropriate sprite
            SetSpriteByIndex(hypeSpriteNumber);
            currentSpriteNumber = hypeSpriteNumber;
        }
    }

    void SetSpriteByIndex(int index) {
        if(index < 0 ) {
            Debug.Log("Invalid HypeMeter sprite request");
            index = 0; // return base sprite for now
        }
        if(index > 10) {
            index = 10;
        }

        Object[] sprites = Resources.LoadAll(spriteFilepath);
        // The array returned by Resources.LoadAll has an empty first element, so add one here
        Sprite newSprite = sprites[index+1] as Sprite;
        this.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    int GetSpriteIndex() {
        int hype = (int) metrics.GetComponent<Metrics>().Hype;
        return (hype / 10) + 1; // always have at least 1 bar
    }
}
