using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        int hours = (int)Metrics.currentHour;
        UnityEngine.UI.Text scoreText = this.GetComponent<UnityEngine.UI.Text>();
        scoreText.text = "You kept it going for " + hours + " hours.";

        UnityEngine.UI.Text comment = GameObject.Find("EncouragingCommentText").GetComponent<UnityEngine.UI.Text>();
        if(hours < 4) {
            comment.text = "I know children's birthday parties that lasted longer than that";
        } else if (hours < 8) {
            comment.text = "LOTR kept people's attention longer than your party";
        } else if (hours < 12) {
            comment.text = "Couldn't quite keep it going until the sun came up";
        } else if (hours < 24) {
            comment.text = "Got pretty close to 1 full day, didn't you?";
        } else if (hours < 48) {
            comment.text = "Over 24 hours... I guess that's a start.";
        } else {
            comment.text = "Now that's what I call a party!";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
