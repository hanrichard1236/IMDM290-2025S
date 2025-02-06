using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeartGenerator : MonoBehaviour
{
    public int shapes = 100;

    public float size = 10;
    private GameObject[] heart;


    // Start is called before the first frame update
    void Start()
    {
        heart = new GameObject[shapes];
        float iter = (float) (2 * Math.PI) / shapes;
        for(int i = 0; i < shapes; i++)
        {
            float t = (float) (i * iter);
            float x = (float) (Math.Sqrt(2) * Math.Pow(Math.Sin(t) , 3) * size);
            float y = (float) (((-1 * Math.Pow(Math.Cos(t) , 3)) - Math.Pow(Math.Cos(t) , 2) + 2 * Math.Cos(t)) * size);
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new Vector3(x, y, 0);
            heart[i] = sphere;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}