using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SEVolumeSlider : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private Slider m_slider;
    [SerializeField] private AudioClip m_testSE;

    private void Awake()
    {
        m_slider.value = AudioManager.Instance.SE.Volume;

        m_slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDestroy()
    {
        m_slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(float value)
    {
        AudioManager.Instance.SE.SetVolume(value);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (m_testSE == null)
            return;

        AudioManager.Instance.SE.Play(m_testSE);
    }
}