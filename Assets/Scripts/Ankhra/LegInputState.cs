public struct LegInputState
{
    public bool Left;
    public bool Right;
    public bool Up;
    public bool Down;

    public float Horizontal
    {
        get
        {
            float value = 0f;

            if (Left)
                value -= 1f;

            if (Right)
                value += 1f;

            return value;
        }
    }

    public float Vertical
    {
        get
        {
            float value = 0f;

            if (Down)
                value -= 1f;

            if (Up)
                value += 1f;

            return value;
        }
    }
}