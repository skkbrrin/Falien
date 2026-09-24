using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class DemoCamera : MonoBehaviour
{
    // カメラの移動速度
    public float moveSpeed = 5.0f;

    private Vector3 CameraStartPos;
    private Vector3 CameraNowPos;


    private void Start()
    {
        CameraStartPos = Camera.main.transform.position;
        CameraNowPos = CameraStartPos;
    }

    void Update()
    {
        transform.position = CameraNowPos;
        float move = 0.0f;
        float move_2 = 0.0f;

        // 左右キー
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move = -1.0f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            move = 1.0f;
        }

        // 上下
        if (Input.GetKey(KeyCode.UpArrow))
        {
            move_2 = 1.0f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            move_2 = -1.0f;
        }

        // カメラを左右に移動
        CameraNowPos += new Vector3(move * moveSpeed * Time.deltaTime, move_2 * moveSpeed * Time.deltaTime, 0.0f);
    }
}
