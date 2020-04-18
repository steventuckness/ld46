using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HypeMeter : MonoBehaviour
{
    const float blinkTimeout = .3f;
    public GameObject metrics;

    int currentSpriteNumber;
    float blinkTimer = blinkTimeout;

    const string spriteFilepath = "Assets/Sprites/hype-meter-spritesheet.png";

    // Start is called before the first frame update
    void Start()
    {
        int hype = (int)(metrics.GetComponent<Metrics>().hype);
        int hypeSpriteNumber = (hype / 10) + 1; // always have at least 1 bar
        SetSpriteByIndex(hypeSpriteNumber);
        currentSpriteNumber = hypeSpriteNumber;
    }

    // Update is called once per frame
    void Update()
    {
        int hype = (int)(metrics.GetComponent<Metrics>().hype);
        int hypeSpriteNumber = (hype / 10) + 1; // always have at least 1 bar

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

        Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(spriteFilepath);
        Sprite newSprite = sprites[index] as Sprite;
        this.GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}
