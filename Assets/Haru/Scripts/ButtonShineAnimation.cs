using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class ButtonShineAnimation : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [SerializeField] private Image m_shine;

    [Header("Animation")]
    [SerializeField] private float m_duration = 0.3f;

    private Coroutine m_animationCoroutine;

    private void Awake()
    {
        SetAlpha(0.0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayAnimation(0.0f, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayAnimation(0.5f, 0.0f);
    }

    private void PlayAnimation(float from, float to)
    {
        if (m_animationCoroutine != null)
        {
            StopCoroutine(m_animationCoroutine);
        }

        m_animationCoroutine = StartCoroutine(
            FadeShine(from, to)
        );
    }

    private IEnumerator FadeShine(float from, float to)
    {
        float time = 0.0f;

        while (time < m_duration)
        {
            time += Time.deltaTime;

            float t = Mathf.Clamp01(time / m_duration);

            // 滑らかにフェード
            t = Mathf.SmoothStep(0.0f, 1.0f, t);

            float alpha = Mathf.Lerp(from, to, t);

            SetAlpha(alpha);

            yield return null;
        }

        SetAlpha(to);
        m_animationCoroutine = null;
    }

    private void SetAlpha(float alpha)
    {
        Color color = m_shine.color;
        color.a = alpha;
        m_shine.color = color;
    }
}