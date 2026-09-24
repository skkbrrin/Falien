using UnityEngine;

public class BodyDecreaser : MonoBehaviour
{
    public Rigidbody2D mybody2D;

    public float easyLevel = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mybody2D.angularVelocity *= ((easyLevel *1) / 2) * Time.deltaTime;
    }
}
