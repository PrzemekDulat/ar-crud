using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinPackingScene : MonoBehaviour
{
    public Camera camera;
    public GameObject arcostam;
    public GameObject rectangle;

    public GameObject clearButton;
    public GameObject assembleButton;
    public GameObject plane;
    // Start is called before the first frame update

    public void EnableBinPackingPanel()
    {
        camera.gameObject.SetActive(!camera.gameObject.activeSelf);           
        arcostam.gameObject.SetActive(!arcostam.gameObject.activeSelf);          
        assembleButton.gameObject.SetActive(!assembleButton.gameObject.activeSelf);
        clearButton.gameObject.SetActive(!clearButton.gameObject.activeSelf);
        plane.gameObject.SetActive(!plane.gameObject.activeSelf);


        var startBalls = GameObject.FindGameObjectsWithTag("startBall");
        var endBalls = GameObject.FindGameObjectsWithTag("endBall");
        var arElements = GameObject.FindGameObjectsWithTag("ar");

        foreach (var item in startBalls)
        {
            item.gameObject.SetActive(!item.gameObject.activeSelf);
        }

        foreach (var item in endBalls)
        {
            item.gameObject.SetActive(!item.gameObject.activeSelf);
        }

        foreach (var item in arElements)
        {
            item.gameObject.SetActive(!item.gameObject.activeSelf);
        }

    }
}
