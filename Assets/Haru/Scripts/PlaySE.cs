using UnityEngine;

public class PlaySE : MonoBehaviour
{
    [SerializeField] private AudioClip m_clip;

    public void Play()
    {
        if (m_clip == null)
            return;

        AudioManager.Instance.SE.Play(m_clip);
    }
}