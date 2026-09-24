using UnityEngine;

public class Left_RightScaleObj : MonoBehaviour
{
    // Time to reach the opposite position
    public float ChangeingTime = 5.0f;

    // Distance to move
    public float MaxScale = 10.0f;

    // true = Left
    // false = Right
    public bool IsLeft = false;

    private bool m_isMove = true;

    private float m_startPositionX;

    void Start()
    {
        m_startPositionX = transform.position.x;
    }

    void Update()
    {
        ChangePosition();
    }

    private void ChangePosition()
    {
        Vector3 position = transform.position;

        if (ChangeingTime <= 0.0f)
        {
            return;
        }

        float speed = MaxScale / ChangeingTime;

        if (m_isMove)
        {
            if (IsLeft)
            {
                position.x -= speed * Time.deltaTime;

                if (position.x <= m_startPositionX - MaxScale)
                {
                    position.x = m_startPositionX - MaxScale;
                    m_isMove = false;
                }
            }
            else
            {
                position.x += speed * Time.deltaTime;

                if (position.x >= m_startPositionX + MaxScale)
                {
                    position.x = m_startPositionX + MaxScale;
                    m_isMove = false;
                }
            }
        }
        else
        {
            if (IsLeft)
            {
                position.x += speed * Time.deltaTime;

                if (position.x >= m_startPositionX)
                {
                    position.x = m_startPositionX;
                    m_isMove = true;
                }
            }
            else
            {
                position.x -= speed * Time.deltaTime;

                if (position.x <= m_startPositionX)
                {
                    position.x = m_startPositionX;
                    m_isMove = true;
                }
            }
        }

        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            m_isMove = !m_isMove;
        }
    }
}