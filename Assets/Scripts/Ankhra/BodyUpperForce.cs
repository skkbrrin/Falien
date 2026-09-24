using UnityEngine;

public class BodyUpperForce : MonoBehaviour
{
    public float upperCompensatingForce = 9.81f;

    public Rigidbody2D myRb2D;

    public void GiveCompensatingForce()
    {
        myRb2D.AddForce(transform.up * upperCompensatingForce * Time.deltaTime);
    }

    public void Update()
    {
        GiveCompensatingForce();
    }
}
