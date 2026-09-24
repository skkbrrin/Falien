using UnityEngine;

public class TitleLogoMover : MonoBehaviour
{
    [SerializeField] private float m_moveAmount = 10.0f;
    [SerializeField] private float m_moveSpeed = 2.0f;

    private RectTransform m_rectTransform;
    private Vector2 m_startPosition;

    private void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_startPosition = m_rectTransform.anchoredPosition;
    }

    private void Update()
    {
        float y = Mathf.Sin(Time.time * m_moveSpeed) * m_moveAmount;

        m_rectTransform.anchoredPosition =
            m_startPosition + new Vector2(0.0f, y);
    }
}