using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class leg_father : MonoBehaviour
{
    [Header("Refs")]
    public Body myBody;
    public PlayerController myPlayerController;

    [Header("Calculations")]
    public Rigidbody2D myControlPoint;
    public DistanceJoint2D myConnectionJoint;
    public float myConnectDistance = 1f;

    [HideInInspector]
    public List<Rigidbody2D> jointList;

    public bool IsLeft { get; private set; }

    protected Rigidbody2D BodyRb
    {
        get
        {
            if (myBody == null)
                return null;

            return myBody.myRb2D;
        }
    }


    protected virtual void Awake()
    {
        if (myBody == null)
        {
            myBody = FindFirstObjectByType<Body>();
        }

        if (myPlayerController == null)
        {
            myPlayerController = FindFirstObjectByType<PlayerController>();
        }
    }


    public virtual void LegInit(bool isLeft)
    {
        IsLeft = isLeft;

        if (myPlayerController == null)
        {
            myPlayerController = FindFirstObjectByType<PlayerController>();
        }

        if (myBody == null)
        {
            myBody = FindFirstObjectByType<Body>();
        }

        if (myPlayerController == null)
        {
            Debug.LogError($"{name}: PlayerController not found.");
            return;
        }

        if (myBody == null)
        {
            Debug.LogError($"{name}: Body not found.");
            return;
        }

        if (myControlPoint == null)
        {
            Debug.LogError($"{name}: Control Point not assigned.");
            return;
        }

        if (myConnectionJoint == null)
        {
            Debug.LogError($"{name}: Connection Joint not assigned.");
            return;
        }


        myConnectionJoint.connectedBody = myBody.myRb2D;

        myConnectionJoint.autoConfigureConnectedAnchor = false;
        myConnectionJoint.autoConfigureDistance = false;

        if (IsLeft)
        {
            myConnectionJoint.connectedAnchor =
                myBody.LegJoint_LeftAnchorOffset;
        }
        else
        {
            myConnectionJoint.connectedAnchor =
                myBody.LegJoint_RightAnchorOffset;
        }

        SetConnectDistance(myConnectDistance);


        LegRenderer legRenderer =
            myConnectionJoint.GetComponent<LegRenderer>();

        if (legRenderer != null)
        {
            legRenderer.pointB =
                IsLeft
                    ? myBody.LeftSitBone.transform
                    : myBody.RightSitBone.transform;
        }


        myPlayerController.RegisterLeg(this, IsLeft);
    }


    public virtual void HandleInput(in LegInputState input)
    {
    }
    protected void AccelerateControlPointRotation(
    float direction,
    float acceleration)
    {
        if (myControlPoint == null)
            return;

        if (Mathf.Approximately(direction, 0f))
            return;

        Vector2 anchorPosition = GetBodyAnchorPosition();

        Vector2 radius =
            myControlPoint.position - anchorPosition;

        if (radius.sqrMagnitude <= Mathf.Epsilon)
            return;

        Vector2 radialDirection = radius.normalized;

        Vector2 tangentDirection = new Vector2(
            -radialDirection.y,
            radialDirection.x
        );

        myControlPoint.linearVelocity +=
            tangentDirection *
            direction *
            acceleration *
            Time.deltaTime;
    }

    protected void AccelerateControlPointX(
        float direction,
        float acceleration)
    {
        if (myControlPoint == null)
            return;

        if (Mathf.Approximately(direction, 0f))
            return;

        myControlPoint.linearVelocityX +=
            direction *
            acceleration *
            Time.deltaTime;
    }


    protected void AccelerateBodyY(
        float direction,
        float acceleration)
    {
        if (BodyRb == null)
            return;

        if (Mathf.Approximately(direction, 0f))
            return;

        BodyRb.linearVelocityY +=
            direction *
            acceleration *
            Time.deltaTime;
    }


    protected void SetControlPointVelocityX(
        float velocity)
    {
        if (myControlPoint == null)
            return;

        myControlPoint.linearVelocityX = velocity;
    }


    protected void SetBodyVelocityY(
        float velocity)
    {
        if (BodyRb == null)
            return;

        BodyRb.linearVelocityY = velocity;
    }


    protected Vector2 GetBodyAnchorPosition()
    {
        if (myBody == null)
        {
            return BodyRb != null
                ? BodyRb.worldCenterOfMass
                : Vector2.zero;
        }

        if (IsLeft)
        {
            if (myBody.LeftSitBone != null)
            {
                return myBody.LeftSitBone.transform.position;
            }
        }
        else
        {
            if (myBody.RightSitBone != null)
            {
                return myBody.RightSitBone.transform.position;
            }
        }

        return BodyRb != null
            ? BodyRb.worldCenterOfMass
            : Vector2.zero;
    }


    protected void SetConnectDistance(float distance)
    {
        myConnectDistance = Mathf.Max(0f, distance);

        if (myConnectionJoint != null)
        {
            myConnectionJoint.distance = myConnectDistance;
        }
    }


    [Button]
    public void LegSetLeft()
    {
        Debug.Log("³]©w¥ªÃE");
        LegInit(true);
    }


    [Button]
    public void LegSetRight()
    {
        LegInit(false);
    }
}