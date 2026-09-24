using UnityEngine;
using UnityEngine.Audio;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioMixer m_audioMixer;

    private const string BGM_VOLUME_PARAM = "BGMVolume";

    public float Volume { get; private set; } = 1.0f;

    public void Play(AudioClip clip)
    {
        if (clip == null)
            return;

        m_audioSource.clip = clip;
        m_audioSource.Play();
    }

    public void Stop()
    {
        m_audioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        Volume = Mathf.Clamp01(volume);

        float decibel =
            Mathf.Log10(Mathf.Max(Volume, 0.0001f)) * 20.0f;

        m_audioMixer.SetFloat(BGM_VOLUME_PARAM, decibel);
    }
}