using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingImageChanging : MonoBehaviour
{
    public Image[] images;

    public Image fadeImage;
    public float fadeTime = 0.5f;

    public float displayTime = 2.0f;

    private int m_currentIndex = 0;
    private float m_timer = 0.0f;

    private enum State
    {
        FadeIn,
        Display,
        FadeOut
    }

    private State m_state;
    private float m_fadeTimer = 0.0f;

    void Start()
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i] != null)
            {
                images[i].gameObject.SetActive(false);
            }
        }

        if (images == null || images.Length == 0)
        {
            return;
        }

        if (images[0] != null)
        {
            images[0].gameObject.SetActive(true);
        }

        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);

            Color color = fadeImage.color;
            color.a = 1.0f;
            fadeImage.color = color;
        }

        m_state = State.FadeIn;
        m_fadeTimer = 0.0f;
    }

    void Update()
    {
        switch (m_state)
        {
            case State.FadeIn:

                m_fadeTimer += Time.deltaTime;

                float fadeInRate = Mathf.Clamp01(
                    m_fadeTimer / fadeTime
                );

                SetFadeAlpha(1.0f - fadeInRate);

                if (fadeInRate >= 1.0f)
                {
                    m_timer = 0.0f;
                    m_state = State.Display;
                }

                break;

            case State.Display:

                m_timer += Time.deltaTime;

                if (m_timer >= displayTime)
                {
                    m_fadeTimer = 0.0f;
                    m_state = State.FadeOut;
                }

                break;

            case State.FadeOut:

                m_fadeTimer += Time.deltaTime;

                float fadeOutRate = Mathf.Clamp01(
                    m_fadeTimer / fadeTime
                );

                SetFadeAlpha(fadeOutRate);

                if (fadeOutRate >= 1.0f)
                {
                    NextImage();
                }

                break;
        }
    }

    private void NextImage()
    {
        if (m_currentIndex < images.Length &&
            images[m_currentIndex] != null)
        {
            images[m_currentIndex].gameObject.SetActive(false);
        }

        if (m_currentIndex >= images.Length - 1)
        {
            SceneManager.LoadScene("ResultScene");
            return;
        }

        m_currentIndex++;

        if (images[m_currentIndex] != null)
        {
            images[m_currentIndex].gameObject.SetActive(true);
        }

        Debug.Log("Next Image : " + m_currentIndex);

        m_fadeTimer = 0.0f;
        m_timer = 0.0f;

        SetFadeAlpha(1.0f);

        m_state = State.FadeIn;
    }

    private void SetFadeAlpha(float alpha)
    {
        if (fadeImage == null)
        {
            return;
        }

        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }
}
