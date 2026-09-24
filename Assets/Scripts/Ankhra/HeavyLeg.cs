//using Sirenix.OdinInspector.Editor.Examples;
using UnityEngine;

public class HeavyLeg : leg_father
{
    [Header("Heavy Leg")]
    public float rotationAcceleration = 1f;

    public bool isInHighSpeed = false;
    public float highSpeedStander = 3f;

    public override void HandleInput(in LegInputState input)
    {
        AccelerateControlPointRotation(
            input.Horizontal,
            rotationAcceleration
        );
    }

    private void Update()
    {
        isInHighSpeed = (myControlPoint.linearVelocityX * myControlPoint.linearVelocityY) > highSpeedStander;
    }

}