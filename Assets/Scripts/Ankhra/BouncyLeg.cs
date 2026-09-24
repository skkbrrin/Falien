using UnityEngine;
using Sirenix.OdinInspector;

public class BouncyLeg : leg_father
{
    [Header("Movement")]
    public float horizontalAcceleration = 2f;
    public float upwardAcceleration = 2f;

    [Header("Bounce")]
    public float max_BounceVelocity = 10f;
    public float BounceChargeMaxTime = 1f;

    [ShowInInspector, ReadOnly]
    private float _cal_BounceChargeMaxTime;

    private float _restConnectDistance;

    private bool _wasDownHeld;
    private bool _isCharging;

    [SerializeField] private int allowBounceCount = 1;
    public int cal_allowBounceCount = 1;

    public Vector2 AimDirection;
    float _aim_direction;
    public Animator myPointerAnimation;
    public bool isAiming;
    public GameObject hintObject;
    public float hintDistance = 1;

    private void Update()
    {
        Vector2 bodyAnchorPosition =
            GetBodyAnchorPosition();

        Vector2 legPosition =
            myControlPoint.worldCenterOfMass;

        AimDirection = bodyAnchorPosition - legPosition;

        _aim_direction = Mathf.Atan2(AimDirection.y, AimDirection.x) * Mathf.Rad2Deg;

        myPointerAnimation.SetBool("active", isAiming);

        hintObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _aim_direction));
    }



    public void ResetTheBounceCount()
    {
        cal_allowBounceCount = allowBounceCount;
    }

    public override void LegInit(bool isLeft)
    {
        base.LegInit(isLeft);

        _restConnectDistance = myConnectDistance;

        _cal_BounceChargeMaxTime = 0f;
        _wasDownHeld = false;
        _isCharging = false;
    }


    public override void HandleInput(in LegInputState input)
    {
        HandleHorizontal(input);
        HandleUp(input);
        HandleBounce(input);
    }


    private void HandleHorizontal(in LegInputState input)
    {
        AccelerateControlPointX(
            input.Horizontal,
            horizontalAcceleration
        );
    }


    private void HandleUp(in LegInputState input)
    {
        if (!input.Up)
            return;

        if (input.Down)
            return;

        AccelerateBodyY(
            1f,
            upwardAcceleration
        );
    }


    private void HandleBounce(in LegInputState input)
    {
        if (input.Down)
        {
            if (!_wasDownHeld)
            {
                StartBounceCharge();
            }

            ChargeBounce();
        }
        else
        {
            if (_wasDownHeld)
            {
                ReleaseBounce();
            }
        }

        _wasDownHeld = input.Down;
    }


    private void StartBounceCharge()
    {
        _isCharging = true;

        _cal_BounceChargeMaxTime = 0f;

        _restConnectDistance =
            myConnectDistance;
    }


    private void ChargeBounce()
    {
        if (!_isCharging)
            return;

        if (BounceChargeMaxTime <= 0f)
        {
            _cal_BounceChargeMaxTime = 0f;

            SetConnectDistance(0f);

            return;
        }

        isAiming = true;

        _cal_BounceChargeMaxTime +=
            Time.deltaTime;

        _cal_BounceChargeMaxTime =
            Mathf.Min(
                _cal_BounceChargeMaxTime,
                BounceChargeMaxTime
            );


        float chargeRatio =
            _cal_BounceChargeMaxTime /
            BounceChargeMaxTime;


        float chargedDistance =
            Mathf.Lerp(
                _restConnectDistance,
                0f,
                chargeRatio
            );


        SetConnectDistance(chargedDistance);
    }


    private void ReleaseBounce()
    {
        if (!_isCharging)
            return;

        isAiming = false;

        float chargeRatio;

        if (BounceChargeMaxTime <= 0f)
        {
            chargeRatio = 1f;
        }
        else
        {
            chargeRatio =
                Mathf.Clamp01(
                    _cal_BounceChargeMaxTime /
                    BounceChargeMaxTime
                );
        }


        float bounceVelocity =
            max_BounceVelocity *
            chargeRatio;


        ApplyBounceVelocity(bounceVelocity);


        SetConnectDistance(
            _restConnectDistance
        );


        _cal_BounceChargeMaxTime = 0f;

        _isCharging = false;
    }


    private void ApplyBounceVelocity(float bounceVelocity)
    {
        if (BodyRb == null)
            return;

        if (myControlPoint == null)
            return;


        Vector2 bodyAnchorPosition =
            GetBodyAnchorPosition();

        Vector2 legPosition =
            myControlPoint.worldCenterOfMass;


        Vector2 launchDirection =
            bodyAnchorPosition -
            legPosition;


        if (launchDirection.sqrMagnitude <
            0.000001f)
        {
            return;
        }


        launchDirection.Normalize();


        BodyRb.linearVelocity +=
            launchDirection *
            bounceVelocity;
    }


    private void OnDisable()
    {
        if (!_isCharging)
            return;

        SetConnectDistance(
            _restConnectDistance
        );

        _cal_BounceChargeMaxTime = 0f;

        _isCharging = false;
        _wasDownHeld = false;
    }
}