using UnityEngine;

public class TentacleLeg : leg_father
{
    [Header("Tentacle")]
    public float horizontalVelocity = 4f;
    public float verticalVelocity = 4f;

    public bool refuseMoveSticky = false;

    public override void HandleInput(in LegInputState input)
    {
        if (refuseMoveSticky)
        {
            return;
        }


        float horizontal = input.Horizontal;
        float vertical = input.Vertical;


        if (!Mathf.Approximately(horizontal, 0f))
        {
            SetControlPointVelocityX(
                horizontal * horizontalVelocity
            );
        }


        if (!Mathf.Approximately(vertical, 0f))
        {
            SetBodyVelocityY(
                vertical * verticalVelocity
            );
        }
    }
}