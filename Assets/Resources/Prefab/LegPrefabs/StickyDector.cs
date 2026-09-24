using UnityEngine;

public class StickyDector : MonoBehaviour
{
    public TentacleLeg myTentacleLeg;

    public float cooldown = 2f;
    public float cal_cooldown = 2f;

    public float stickyCooldown = 0.2f;
    public float cal_stickyCooldown = 0f;

    public void MakeSticky()
    {
        cal_cooldown = cooldown;
        cal_stickyCooldown = stickyCooldown;

        myTentacleLeg.refuseMoveSticky = true;  
    }

    public void ReleaseSticky()
    {
        myTentacleLeg.refuseMoveSticky = false;
    }

    private void Update()
    {
        cal_cooldown -= Time.deltaTime;
        cal_stickyCooldown -= Time.deltaTime;

        if (cal_stickyCooldown >= 0)
        {
            return;
        }

        if (myTentacleLeg.IsLeft)
        {
            if (myTentacleLeg.myPlayerController.isInputting_L)
            {
                //Release
                ReleaseSticky();
            }
        }
        else
        {
            if (myTentacleLeg.myPlayerController.isInputting_R)
            {
                ReleaseSticky();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (cal_cooldown > 0)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            MakeSticky();
        }
    }
}
