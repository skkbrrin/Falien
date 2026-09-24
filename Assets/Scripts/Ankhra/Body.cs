using UnityEngine;

public class Body : MonoBehaviour
{
    public Rigidbody2D myRb2D;

    public GameObject LeftSitBone;
    public GameObject RightSitBone;

    public Vector2 LegJoint_LeftAnchorOffset => LeftSitBone.transform.localPosition;
    public Vector2 LegJoint_RightAnchorOffset => RightSitBone.transform.localPosition;


}
