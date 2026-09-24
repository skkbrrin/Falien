using UnityEngine;

public class randomShitController : MonoBehaviour
{
    public Rigidbody2D leftControlPoint;
    public Rigidbody2D rightControlPoint;

    public float VelocityGiving = 10;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Left_GoLeft();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Left_GoRight();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Right_GoLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Right_GoRight();
        }
    }

    public void Left_GoLeft()
    {
        leftControlPoint.linearVelocityX = -VelocityGiving;
    }

    public void Left_GoRight()
    {
        leftControlPoint.linearVelocityX += VelocityGiving;
    }

    public void Left_GoUp()
    {
        leftControlPoint.linearVelocityY += VelocityGiving;
    }

    public void Left_GoDown()
    {
        leftControlPoint.linearVelocityY -= VelocityGiving;
    }

    public void Right_GoLeft()
    {
        rightControlPoint.linearVelocityX -= VelocityGiving;
    }
    public void Right_GoRight()
    {
        rightControlPoint.linearVelocityX += VelocityGiving;
    }

    public void Right_GoUp()
    {
        rightControlPoint.linearVelocityY += VelocityGiving;
    }

    public void Right_GoDown()
    {
        rightControlPoint.linearVelocityY -= VelocityGiving;
    }

}
