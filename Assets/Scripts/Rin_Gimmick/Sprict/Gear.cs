using System;
using UnityEngine;

public class Gear : MonoBehaviour
{
    //public GameObject gear;

    public float Rollangle = 40.0f;
    public bool Left = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Left)
        {
            this.transform.Rotate(Vector3.forward, Rollangle * Time.deltaTime);
            
        }
        else if (!Left)
        {
            this.transform.Rotate(Vector3.back, Rollangle * Time.deltaTime);
            
        }
    }
}
