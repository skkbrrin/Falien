using UnityEngine;

public class CircleObject : MonoBehaviour
{

    public GameObject Spike;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float radius = 5.0f;

        for (int i = 0; i < 10; i++)
        {
            float angle = 360.0f / 10.0f * i;
            float radian = angle * Mathf.Deg2Rad;

            float x = Mathf.Cos(radian) * radius;
            float z = Mathf.Sin(radian) * radius;

            Vector3 position = new Vector3(x, 0.0f, z);

            Spike.transform.Translate(position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
