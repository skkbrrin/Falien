using UnityEngine;
using UnityEngine.Audio;

public class SEManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioMixer m_audioMixer;

    private const string SE_VOLUME_PARAM = "SEVolume";

    public float Volume { get; private set; } = 1.0f;

    public void Play(AudioClip clip)
    {
        if (clip == null)
            return;

        m_audioSource.PlayOneShot(clip);
    }

    public void SetVolume(float volume)
    {
        Volume = Mathf.Clamp01(volume);

        float decibel = Mathf.Log10(Mathf.Max(Volume, 0.0001f)) * 20.0f;
        m_audioMixer.SetFloat(SE_VOLUME_PARAM, decibel);
    }
}