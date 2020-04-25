using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    public SpriteRenderer renderer;
    public float speed = 1.5f;
    public Rigidbody2D rb;
    public Collider2D collider;
    public string name;
    Vector3 tmpPos;

    void Awake()
    {
        int r = GetRandomNumber(0,255);
        int g = GetRandomNumber(0, 255);  
        int b = GetRandomNumber(0, 255);      
        renderer.color = new Color(r/255f, g/255f, b/255f, 1f);
    }

    private void Start()
    {
        rb.velocity = new Vector2(0.01f,0.01f);
    }

    private void FixedUpdate()
    {
        Vector3 vel = rb.velocity;
        if (vel.magnitude == 0)
        {
            //Destroy(rb);
            //Destroy(collider,5f);
            //Destroy(this);
            rb.bodyType = RigidbodyType2D.Static;
        }
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
