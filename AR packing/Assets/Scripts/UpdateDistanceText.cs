using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateDistanceText : MonoBehaviour
{
    public Text distanceText;

    // Update is called once per frame
    void Update()
    {
        var startBall = GameObject.FindGameObjectsWithTag("startBall");
        var endBall = GameObject.FindGameObjectsWithTag("endBall");
        foreach (var ball in startBall)
        {
            //if (ball.GetComponent<Distance>().AmILast == true)
            //{
                //float distanceInCm = ball.GetComponent<Distance>().distance * 100;
                double distanceInCm = System.Math.Round(ball.GetComponent<Distance>().distance, 2);
                distanceText.text = (distanceInCm*100).ToString() + " cm";
            //}
        }
    }
}
