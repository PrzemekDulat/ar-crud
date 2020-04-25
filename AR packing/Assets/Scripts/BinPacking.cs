using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scenes.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class BinPacking : MonoBehaviour
{
    public SpriteRenderer renderer;
    public float spawnTime = 3f;
    public GameObject rectangle;
    public GameObject plane;
    //public GameObject assembler;
    public bool canFit = true;
    GameObject rectangleTmp;
    Camera camera;
    Text counterText;

    public static Dictionary<string, Dimensions> objects = new Dictionary<string, Dimensions>();
    List<GameObject> rectangles = new List<GameObject>();


    int rectanglesCounter = 1;
    float offsetX = 0f;
    bool pass = false;
    public bool firstIteration = true;
    float nextOffset = 0f;
    int allRectangles = 0;

    // Start is called before the first frame update
    public void Start()
    {
        objects.Clear();


        counterText = GameObject.FindGameObjectWithTag("StatisticText").GetComponent<Text>();
        #region rectangles
        //Dimensions dimensions = new Dimensions();

        //dimensions.x = 20000; dimensions.y = 15000;
        //dimensions.count = 1;
        //if (dimensions.x < dimensions.y)
        //{
        //    var tmpX = dimensions.x;
        //    var tmpY = dimensions.y;
        //    Dimensions tmpDim = new Dimensions();


        //    tmpDim.x = tmpY;
        //    tmpDim.y = tmpX;
        //    tmpDim.count = 1;

        //    objects.Add("Object -1", tmpDim);
        //}
        //else
        //{
        //    objects.Add("Object -1" + 0, dimensions);
        //}

        //for (int i = 0; i < 5000; i++)
        //{
        //    Dimensions dimensions5 = new Dimensions();

        //    dimensions5.x = GetRandomNumber(50, 200); dimensions5.y = GetRandomNumber(50, 300); dimensions5.count = GetRandomNumber(1, 5);
        //    if (dimensions5.x < dimensions5.y)
        //    {
        //        var tmpX = dimensions5.x;
        //        var tmpY = dimensions5.y;
        //        Dimensions tmpDim = new Dimensions();


        //        tmpDim.x = tmpY;
        //        tmpDim.y = tmpX;
        //        tmpDim.count = dimensions5.count;

        //        objects.Add("Object" + i, tmpDim);
        //    }
        //    else
        //    {
        //        objects.Add("Object" + i, dimensions5);
        //    }
        //}
        #endregion

        objects = AddButtonScript.instance.GetObjects();

        foreach (var rectangle in objects)
        {
            if (rectangle.Value.x < rectangle.Value.y)
            {
                var tmpXvalue = rectangle.Value.x;
                var tmpYvalue = rectangle.Value.y;
                rectangle.Value.x = tmpYvalue;
                rectangle.Value.y = tmpXvalue;
            }
        }

        objects = objects.OrderByDescending(x => x.Value.x).ToDictionary(x => x.Key, x => x.Value);

        plane = Instantiate(plane);

        float planeX = objects.ElementAt(0).Value.x;
        float planeY = objects.ElementAt(0).Value.y;
        plane.tag = "placedPlane";
        plane.transform.localScale = new Vector3(planeX, planeY, 1);
        plane.transform.position = new Vector3((planeX / 200f) - 0.5f, (planeY / -200f) + 0.5f, 0);

        camera = GameObject.FindGameObjectWithTag("AssemblyCamera").GetComponent<Camera>();
        camera.gameObject.transform.position = new Vector3(plane.transform.position.x, plane.transform.position.y);
        camera.orthographicSize = (plane.transform.localScale.x / 80f);

        foreach (var rect in objects)
        {
            allRectangles += rect.Value.count;
        }
        allRectangles = allRectangles - 1;
        InvokeRepeating("SpawnRectangle", spawnTime, spawnTime);        
    }

    int statCounter = 1;

    void SpawnRectangle()
    {
        if (rectanglesCounter >= objects.Count)
        {
            CancelInvoke();
        }
        if (objects.ElementAt(rectanglesCounter).Value.count >= 1)
        {
            objects.ElementAt(rectanglesCounter).Value.count--;

            if (canFit || pass)
            {
                float scaleX = objects.ElementAt(rectanglesCounter).Value.x;
                float scaleY = objects.ElementAt(rectanglesCounter).Value.y;

                this.transform.localScale = new Vector3(scaleX, scaleY, 1);
                this.transform.position = new Vector3((scaleX / 200) - 0.5f + (offsetX), (scaleY / -200) + 0.5f, 0);


                rectangleTmp = Instantiate(rectangle);
                rectangleTmp.transform.position = this.transform.position;
                rectangleTmp.transform.localScale = this.transform.localScale;
                rectangleTmp.GetComponent<RandomColor>().name = objects.ElementAt(rectanglesCounter).Key;
                rectangleTmp.tag = "placedRectangle";
                counterText.text = statCounter.ToString() + "/" + allRectangles.ToString();
                statCounter++;
                if (objects.ElementAt(rectanglesCounter).Value.count <= 0)
                {
                    float NextX = objects.ElementAt(rectanglesCounter + 1).Value.x;
                    float NextY = objects.ElementAt(rectanglesCounter + 1).Value.y;

                    this.transform.localScale = new Vector3(NextX, NextY, 1);
                    this.transform.position = new Vector3((NextX / 200) - 0.5f + (offsetX), (NextY / -200) + 0.5f, 0);
                }
               
                pass = false;
            }
            else
            {
                offsetX = objects.ElementAt(1).Value.x / 100f + nextOffset;
                nextOffset += objects.ElementAt(rectanglesCounter).Value.x / 100f;
                Debug.Log("Current offsetX:    " + offsetX);
                Debug.Log("Next offsetX:    " + nextOffset);
                objects.ElementAt(rectanglesCounter).Value.count++;
                firstIteration = false;
                pass = true;
            }

        }
        else
        {
            rectanglesCounter++;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        renderer.color = new Color(255f / 255f, 1f / 255f, 1f / 255f, 1f);
        canFit = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        renderer.color = new Color(1f / 255f, 255 / 255f, 1f / 255f, 1f);
        canFit = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        renderer.color = new Color(255f / 255f, 1f / 255f, 1f / 255f, 1f);
        canFit = false;
    }

    private static readonly System.Random getrandom = new System.Random();

    public static int GetRandomNumber(int min, int max)
    {
        lock (getrandom) // synchronize
        {
            return getrandom.Next(min, max);
        }
    }
}
