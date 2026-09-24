using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonFloatEffect : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [Header("浮き上がる高さ")]
    [SerializeField] private float floatHeight = 10.0f;

    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 8.0f;

    private RectTransform m_rectTransform;
    private Vector2 m_defaultPosition;
    private Vector2 m_targetPosition;

    private void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();

        m_defaultPosition = m_rectTransform.anchoredPosition;
        m_targetPosition = m_defaultPosition;
    }

    private void Update()
    {
        m_rectTransform.anchoredPosition = Vector2.Lerp(
            m_rectTransform.anchoredPosition,
            m_targetPosition,
            Time.deltaTime * moveSpeed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_targetPosition = m_defaultPosition + Vector2.up * floatHeight;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_targetPosition = m_defaultPosition;
    }
}