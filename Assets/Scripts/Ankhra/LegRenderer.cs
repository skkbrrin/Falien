using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LegRenderer : MonoBehaviour
{

    [SerializeField] public Transform pointA;
    [SerializeField] public Transform pointB;

    [SerializeField] private float width = 1f;

    [SerializeField] private float imageLength = 1f;

    private LineRenderer line;
    public Material lineMaterial;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();

        line.positionCount = 2;
        line.useWorldSpace = true;

        //line.textureMode = LineTextureMode.Stretch;

        //lineMaterial = line.material;
        line.material = lineMaterial;
    }

    private void LateUpdate()
    {
        if (pointA == null || pointB == null)
            return;

        Vector3 a = pointA.position;
        Vector3 b = pointB.position;

        line.SetPosition(0, a);
        line.SetPosition(1, b);

        line.startWidth = width;
        line.endWidth = width;

        float distance = Vector3.Distance(a, b);

        float tileCount = distance / imageLength;

        if (lineMaterial != null)
        {
            lineMaterial.mainTextureScale =
                new Vector2(tileCount, 1f);
        }
        else
        {
            lineMaterial = line.material;
        }
    }
}
