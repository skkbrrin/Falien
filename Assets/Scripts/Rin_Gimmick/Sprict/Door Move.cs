using UnityEngine;

public class DoorMove : MonoBehaviour
{
    public Sprite m_doorBack;
    public Sprite m_doorRight;
    public Sprite m_doorLeft;

    private bool m_isOpen = false;
    private bool m_isMoving = false;

    public float OpenDistance = 2.0f;
    public float MoveTime = 1.0f;

    private Vector3 m_leftClosedPosition;
    private Vector3 m_rightClosedPosition;

    private Vector3 m_leftOpenPosition;
    private Vector3 m_rightOpenPosition;

    public Transform LeftDoor;
    public Transform RightDoor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 閉じた状態の位置を保存
        m_leftClosedPosition = LeftDoor.localPosition;
        m_rightClosedPosition = RightDoor.localPosition;

        // 開いた状態の位置を作る
        m_leftOpenPosition = m_leftClosedPosition + Vector3.left * OpenDistance;
        m_rightOpenPosition = m_rightClosedPosition + Vector3.right * OpenDistance;
    }

    public void OpenDoor()
    {
        if (m_isMoving || m_isOpen)
        {
            return;
        }

        StartCoroutine(MoveDoor(
            m_leftOpenPosition,
            m_rightOpenPosition,
            true
        ));
    }

    public void CloseDoor()
    {
        if (m_isMoving || !m_isOpen)
        {
            return;
        }

        StartCoroutine(MoveDoor(
            m_leftClosedPosition,
            m_rightClosedPosition,
            false
        ));
    }

    private System.Collections.IEnumerator MoveDoor(
        Vector3 leftTarget,
        Vector3 rightTarget,
        bool open
    )
    {
        m_isMoving = true;

        Vector3 leftStart = LeftDoor.localPosition;
        Vector3 rightStart = RightDoor.localPosition;

        float elapsedTime = 0.0f;

        while (elapsedTime < MoveTime)
        {
            elapsedTime += Time.deltaTime;

            float rate = elapsedTime / MoveTime;

            // 動きを滑らかにする
            rate = Mathf.SmoothStep(0.0f, 1.0f, rate);

            LeftDoor.localPosition =
                Vector3.Lerp(leftStart, leftTarget, rate);

            RightDoor.localPosition =
                Vector3.Lerp(rightStart, rightTarget, rate);

            yield return null;
        }

        // 最終位置を確実に設定
        LeftDoor.localPosition = leftTarget;
        RightDoor.localPosition = rightTarget;

        m_isOpen = open;
        m_isMoving = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OpenDoor();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CloseDoor();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CloseDoor();
    }
}