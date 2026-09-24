using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonRotateEffect : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [Header("回転角度")]
    [SerializeField] private float rotateAngle = 90.0f;

    [Header("回転速度")]
    [SerializeField] private float rotateSpeed = 8.0f;

    private RectTransform m_rectTransform;

    private float m_defaultRotation;
    private float m_targetRotation;

    private void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();

        m_defaultRotation = m_rectTransform.localEulerAngles.z;
        m_targetRotation = m_defaultRotation;
    }

    private void Update()
    {
        float currentRotation = m_rectTransform.localEulerAngles.z;

        float newRotation = Mathf.LerpAngle(
            currentRotation,
            m_targetRotation,
            Time.deltaTime * rotateSpeed
        );

        m_rectTransform.localRotation =
            Quaternion.Euler(0.0f, 0.0f, newRotation);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_targetRotation = m_defaultRotation + rotateAngle;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_targetRotation = m_defaultRotation;
    }
}