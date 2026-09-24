using UnityEngine;

public class StiffLeg : leg_father
{
    [Header("Stiff Leg")]
    public float rotationAcceleration = 2f;

    public override void HandleInput(in LegInputState input)
    {
        AccelerateControlPointRotation(
            input.Horizontal,
            rotationAcceleration
        );
    }
}