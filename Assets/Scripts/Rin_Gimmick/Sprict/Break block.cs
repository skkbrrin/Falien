using UnityEngine;

public class Breakblock : MonoBehaviour
{
    public float RequiredSpeed = 5.0f;

    public LegType RequiredLegType = LegType.HeavyLeg;

    public PlayerController playerController;

    public GameObject breakPartsAnimationObject;

    private void Update()
    {
        if (playerController == null)
        {
            playerController = FindFirstObjectByType<PlayerController>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (playerController == null)
        {
            //playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController = FindFirstObjectByType<PlayerController>();
        }

        Rigidbody2D playerRb = playerController.myBodyRb2D;

        float speed = playerRb.linearVelocity.magnitude;

        Debug.Log("Player速度: " + speed);
        Debug.Log("左足: " + playerController.Left_LegType);
        Debug.Log("右足: " + playerController.Right_LegType);
        Debug.Log("必要速度: " + RequiredSpeed);

        if (speed < RequiredSpeed)
        {
            Debug.Log("速度不足");
            return;
        }

        if (playerController.Left_LegType == LegType.HeavyLeg ||
            playerController.Right_LegType == LegType.HeavyLeg)
        {
            Debug.Log("ブロック破壊！");
            BreakBlock();
        }
        else
        {
            Debug.Log("HeavyLegではない");
        }
    }

    private void BreakBlock()
    {
        GameObject obj = Instantiate(breakPartsAnimationObject, transform.position, transform.rotation);

        obj.transform.localScale = transform.localScale;

        Destroy(gameObject);
    }
}
