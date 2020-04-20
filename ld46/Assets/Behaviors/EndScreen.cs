using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        int hours = (int)Metrics.currentHour;
        int finalRevenue = (int)Metrics.totalRevenue;
        Text totalRevenueText = GameObject.Find("TotalRevenueText").GetComponent<Text>();
        totalRevenueText.text = "Total Revenue: $" + finalRevenue;
        Text scoreText = this.GetComponent<Text>();
        scoreText.text = "You kept it alive for " + hours + " hours.";

        Text comment = GameObject.Find("EncouragingCommentText").GetComponent<Text>();
        if(hours < 4) {
            comment.text = "I know children's birthday parties that lasted longer than that.";
        } else if (hours < 8) {
            comment.text = "Lord of the Rings kept people's attention longer than your party.";
        } else if (hours < 12) {
            comment.text = "Couldn't quite keep it going until the sun came up.";
        } else if (hours < 24) {
            comment.text = "Got pretty close to a full day, didn't you?";
        } else if (hours < 36) {
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
