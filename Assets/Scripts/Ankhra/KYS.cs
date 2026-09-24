using UnityEngine;

public class KYS : MonoBehaviour
{
    public float KYSTime = 5f;

    public void Start()
    {
        Destroy(gameObject, KYSTime);
    }
}
