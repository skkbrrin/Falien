using UnityEngine;
using UnityEngine.UI;

public class BGMVolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider m_slider;

    private void Awake()
    {
        m_slider.value = AudioManager.Instance.BGM.Volume;

        m_slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDestroy()
    {
        m_slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(float value)
    {
        AudioManager.Instance.BGM.SetVolume(value);
    }
}