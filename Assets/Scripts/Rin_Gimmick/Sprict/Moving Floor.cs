using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    public float MoveSpeed = 1.0f;
    public bool MoveLeft = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;

        if (rb != null)
        {
            if (MoveLeft)
            {
                rb.linearVelocity = new Vector2(-MoveSpeed, rb.linearVelocity.y);
            } else if (!MoveLeft)
            {
                rb.linearVelocity = new Vector2(MoveSpeed, rb.linearVelocity.y);
            }
        }
    }
}
