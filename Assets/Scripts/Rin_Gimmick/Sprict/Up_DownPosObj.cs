using UnityEngine;

public class Up_DownPosObj : MonoBehaviour
{
    // Time to reach the destination
    public float ChangeingTime = 5.0f;

    // Distance to move
    public float MoveDistance = 10.0f;

    // false = Up
    // true  = Down
    public bool IsDown = false;

    // true = currently moving to the destination
    // false = currently returning to the start position
    private bool m_isMove = true;

    // Starting Y position
    private float m_startPositionY;

    void Start()
    {
        m_startPositionY = transform.position.y;
    }

    void Update()
    {
        ChangePosition();
    }

    private void ChangePosition()
    {
        Vector3 position = transform.position;

        if (ChangeingTime <= 0.0f || MoveDistance <= 0.0f)
        {
            return;
        }

        // Calculate movement speed
        float speed = MoveDistance / ChangeingTime;

        if (m_isMove)
        {
            // Move to destination
            if (IsDown)
            {
                // Move downward
                position.y -= speed * Time.deltaTime;

                // Reached destination
                if (position.y <= m_startPositionY - MoveDistance)
                {
                    position.y = m_startPositionY - MoveDistance;
                    m_isMove = false;
                }
            }
            else
            {
                // Move upward
                position.y += speed * Time.deltaTime;

                // Reached destination
                if (position.y >= m_startPositionY + MoveDistance)
                {
                    position.y = m_startPositionY + MoveDistance;
                    m_isMove = false;
                }
            }
        }
        else
        {
            // Return to starting position
            if (IsDown)
            {
                // Return upward
                position.y += speed * Time.deltaTime;

                // Reached starting position
                if (position.y >= m_startPositionY)
                {
                    position.y = m_startPositionY;
                    m_isMove = true;
                }
            }
            else
            {
                // Return downward
                position.y -= speed * Time.deltaTime;

                // Reached starting position
                if (position.y <= m_startPositionY)
                {
                    position.y = m_startPositionY;
                    m_isMove = true;
                }
            }
        }

        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reverse movement when hitting something other than Player
        if (!collision.gameObject.CompareTag("Player"))
        {
            m_isMove = !m_isMove;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Reverse movement when entering something other than Player
        if (!collision.gameObject.CompareTag("Player"))
        {
            m_isMove = !m_isMove;
        }
    }
}