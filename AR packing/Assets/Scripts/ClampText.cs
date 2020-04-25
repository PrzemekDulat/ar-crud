using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampText : MonoBehaviour
{
    public GameObject distanceText;
    GameObject uiClone;
    // Start is called before the first frame update
    void Start()
    {
        var position = Camera.main.WorldToScreenPoint(this.transform.position);
        uiClone = (GameObject)Instantiate(distanceText, position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        uiClone.transform.position = this.gameObject.transform.position;
        uiClone.transform.LookAt(Camera.main.transform);
        uiClone.transform.eulerAngles = new Vector3(uiClone.transform.eulerAngles.x + 180, uiClone.transform.eulerAngles.y, uiClone.transform.eulerAngles.z + 180);
        double distanceInCm = System.Math.Round(this.transform.localScale.z, 2);
        uiClone.GetComponentInChildren<Text>().text = (distanceInCm * 100).ToString() + " cm";
    }
}
