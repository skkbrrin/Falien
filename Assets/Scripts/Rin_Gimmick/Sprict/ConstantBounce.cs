using UnityEngine;

public class ConstantBounce : MonoBehaviour
{
    public float m_bounceSpeed = 10.0f;

    private Rigidbody2D m_rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                playerRb.linearVelocity = new Vector2(
                    playerRb.linearVelocity.x,
                    10.0f
                );
            }
        }
    }
}
