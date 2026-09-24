using UnityEngine;

public class attachPicture : MonoBehaviour
{
    public Transform A;
    public Transform B;

    void Update()
    {
        if (A == null || B == null)
            return;

        Vector3 direction = B.position - A.position;

        float horizontalDistance = new Vector2(direction.x, direction.z).magnitude;

        float elevationAngle = Mathf.Atan2(
            direction.y,
            horizontalDistance
        ) * Mathf.Rad2Deg;

        Vector3 euler = transform.eulerAngles;

        euler.z = -elevationAngle;

        transform.eulerAngles = euler;

        transform.position = A.transform.position;
    }
}
