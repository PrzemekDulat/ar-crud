using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCamera : MonoBehaviour
{
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        var plane = GameObject.FindGameObjectWithTag("AssemblyCamera");

        //x
        //1000 / 200 = 5  
        // 5 - 0.5 = 4.5
        //this.gameObject.transform.position = new Vector3(plane.transform.position.x, plane.transform.position.y);
        //camera.orthographicSize = (plane.transform.localScale.x / 100f);
    }
}
