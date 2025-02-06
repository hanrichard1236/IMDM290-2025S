using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeartGenerator : MonoBehaviour
{
    public int numshapes = 100;

    public float size = 10;
    private GameObject[] spheres;
    float time = 0f;
    Vector3[] initPos;
    Vector3[] startPosition, endPosition;
    float lerpFraction; // Lerp point between 0~1
    float t;

    // Start is called before the first frame update
    void Start()
    {
        spheres = new GameObject[numshapes];
        initPos = new Vector3[numshapes];
        startPosition = new Vector3[numshapes]; 
        endPosition = new Vector3[numshapes];
        float iter = (float) (2 * Math.PI) / numshapes;
        for(int i = 0; i < numshapes; i++)
        {
            float t = (float) (i * iter);
            float x = (float) (Math.Sqrt(2) * Math.Pow(Math.Sin(t) , 3) * size);
            float y = (float) (((-1 * Math.Pow(Math.Cos(t) , 3)) - Math.Pow(Math.Cos(t) , 2) + 2 * Math.Cos(t)) * size);
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new Vector3(x, y, 0);
            initPos[i] = new Vector3(x, y, 0);
            startPosition[i] = new Vector3(x, y, 0);
            endPosition[i] = new Vector3(x *2, y *2, 0);
            spheres[i] = sphere;
            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();
            float hue = (float)i / numshapes;
            Color color = Color.HSVToRGB(hue, 1f, 1f);
            sphereRenderer.material.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float heartbeat = 0f;
        float beatTime = time % 2f;
        if (beatTime < 0.2f)  // First beat (sharp spike)
        {
            heartbeat = Mathf.Pow(Mathf.Sin(beatTime * Mathf.PI * 5f), 2);
        }
        else if (beatTime >= 0.5f && beatTime < 0.7f)  // Second beat (another spike)
        {
            heartbeat = Mathf.Pow(Mathf.Sin((beatTime - 0.5f) * Mathf.PI * 5f), 2);
        }
        else  // Rest period
        {
            heartbeat = 0f;
        }

        
        for (int i =0; i < numshapes; i++){
            lerpFraction = Mathf.Clamp01(heartbeat);

            // Lerp logic. Update position       
            t = i * 2 * Mathf.PI / numshapes;
            spheres[i].transform.position = Vector3.Lerp(startPosition[i], endPosition[i], lerpFraction);

            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();
            float hue = (float)i / numshapes; // Hue cycles through 0 to 1
            Color color = Color.HSVToRGB(Mathf.Abs(hue * Mathf.Sin(time)), Mathf.Cos(time), 2f + Mathf.Cos(time));
            sphereRenderer.material.color = color;
        }
    }
}