using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnTouch : MonoBehaviour
{
    public static PlaceOnTouch instance;
    public InputField inputField;
    public RectTransform menu;
    public Camera currentCamera;
    public GameObject placementIndicator;
    public GameObject startBall;
    public GameObject endBall;
    public GameObject linePrefab;
    
    private GameObject line;
    private ARSessionOrigin arOrigin;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    public bool measuring = false;
    private float distance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one PlaceOnTouch is active");
            return;
        }

        instance = this;
    }

    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
        UpdateMeasurement();
    }

    void UpdateMeasurement()
    {
        //var startBalls = GameObject.FindGameObjectsWithTag("startBall");
        //var endBalls = GameObject.FindGameObjectsWithTag("endBall");

        if (!(EventSystem.current.IsPointerOverGameObject() || EventSystem.current.currentSelectedGameObject != null) && (menu.gameObject.activeInHierarchy == false))
        {
            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                SetStart(startBall);
            }
            else if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                SetEnd(endBall);
            }
            else
            {
                if (measuring)
                {
                    AdjustLine();
                }
            }
        }
    }
    void SetStart(GameObject ball)
    {
        measuring = true;
        startBall = (GameObject)Instantiate(ball, placementPose.position, placementPose.rotation);
        line = (GameObject)Instantiate(linePrefab, placementPose.position, Quaternion.identity);
    }

    void SetEnd(GameObject ball)
    {
        measuring = false;
        Instantiate(ball, placementPose.position, placementPose.rotation);
        endBall = (GameObject)Instantiate(ball, placementPose.position, placementPose.rotation);
        inputField.text = (System.Math.Round(distance, 2) * 100).ToString();
        menu.gameObject.SetActive(true);
        AddButtonScript.instance.AddNewObjectToDictionary();
    }

    void AdjustLine()
    {
        endBall.transform.position = placementIndicator.transform.position;
        //adjustLine();
        startBall.transform.LookAt(endBall.transform.position);
        endBall.transform.LookAt(startBall.transform.position);
        distance = Vector3.Distance(startBall.transform.position, endBall.transform.position);
        line.transform.position = startBall.transform.position + distance / 2 * startBall.transform.forward;
        line.transform.rotation = startBall.transform.rotation;
        line.transform.localScale = new Vector3(line.transform.localScale.x, line.transform.localScale.y, distance + 0.005f);
    }

    void adjustLine()
    {
        startBall.transform.LookAt(endBall.transform.position);
        endBall.transform.LookAt(startBall.transform.position);
        distance = Vector3.Distance(startBall.transform.position, endBall.transform.position);
        line.transform.position = startBall.transform.position + distance / 2 * startBall.transform.forward;
        line.transform.rotation = startBall.transform.rotation;
        line.transform.localScale = new Vector3(line.transform.localScale.x, line.transform.localScale.y, distance + 0.005f);
    }

    void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void UpdatePlacementPose()
    {
        var hits = new List<ARRaycastHit>();
        var screenCenter = currentCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        arOrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);
        placementPoseIsValid = hits.Count > 0;
        //placementPoseIsValid = arOrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);

        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            //var cameraFoward = currentCamera.transform.forward;
            //var cameraBearing = new Vector3(cameraFoward.x, 0, cameraFoward.z).normalized;
            //placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
