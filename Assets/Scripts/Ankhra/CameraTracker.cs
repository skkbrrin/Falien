using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class CameraTracker : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] public Transform target;

    [Header("Follow Settings")]
    [SerializeField] private Vector2 offset = Vector2.zero;

    [SerializeField]
    [Range(0.01f, 1f)]
    private float smoothTime = 0.15f;

    [Header("Camera Settings")]
    [SerializeField]
    private float targetOrthographicSize = 5f;

    private Camera cam;

    private Vector3 velocity;
    private float orthographicSizeVelocity;

    private void Awake()
    {
        cam = FindFirstObjectByType<Camera>();

        if (cam != null)
        {
            targetOrthographicSize = cam.orthographicSize;
        }
    }

    private void Start()
    {
        InitCamera();
    }

    [Button]
    public void InitCamera()
    {
        if (target == null)
            return;

        Vector3 position = GetTargetPosition();
        transform.position = position;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = GetTargetPosition();

            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref velocity,
                smoothTime
            );
        }

        UpdateOrthographicSize();
    }

    private void UpdateOrthographicSize()
    {
        if (cam == null)
            return;

        cam.orthographicSize = Mathf.SmoothDamp(
            cam.orthographicSize,
            targetOrthographicSize,
            ref orthographicSizeVelocity,
            smoothTime
        );
    }

    private Vector3 GetTargetPosition()
    {
        return new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z
        );
    }

    public void SetOrthographicSize(float size)
    {
        targetOrthographicSize = size;
    }
}