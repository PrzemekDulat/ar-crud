using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    public float distance = 0f;
    private GameObject placementBall;

    void Update()
    {
        if (PlaceOnTouch.instance.measuring)
        {
            placementBall = GameObject.FindGameObjectWithTag("placementBall");
            distance = Vector3.Distance(transform.position, placementBall.transform.position);
        }
    }
}
