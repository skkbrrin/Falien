using UnityEngine;

public class SpriteLoopAnimation : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float frameRate = 10.0f;
    [SerializeField] private bool playOnStart = true;

    private SpriteRenderer spriteRenderer;

    private int currentFrame = 0;
    private float timer = 0.0f;
    private bool isPlaying = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (playOnStart)
        {
            Play();
        }
    }

    private void Update()
    {
        if (!isPlaying)
            return;

        if (sprites == null || sprites.Length == 0)
            return;

        if (frameRate <= 0.0f)
            return;

        timer += Time.deltaTime;

        float frameTime = 1.0f / frameRate;

        if (timer >= frameTime)
        {
            timer -= frameTime;

            currentFrame++;

            if (currentFrame >= sprites.Length)
            {
                currentFrame = 0;
            }

            spriteRenderer.sprite = sprites[currentFrame];
        }
    }

    public void Play()
    {
        if (sprites == null || sprites.Length == 0)
            return;

        isPlaying = true;
    }

    public void Stop()
    {
        isPlaying = false;

        currentFrame = 0;
        timer = 0.0f;

        if (sprites != null && sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[0];
        }
    }

    public void Pause()
    {
        isPlaying = false;
    }

    public void Resume()
    {
        isPlaying = true;
    }
}