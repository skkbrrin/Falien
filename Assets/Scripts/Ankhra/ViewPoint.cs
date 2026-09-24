using UnityEngine;

public class ViewPoint : MonoBehaviour
{
    [Header("Refs")]
    public CameraTrackManager cameraTrackManager;
    public int myIndex => cameraTrackManager.CameraSpots.IndexOf(this);

    public float OrthographicSize = 8f;

    private void Awake()
    {
        cameraTrackManager = FindFirstObjectByType<CameraTrackManager>();
    }
    public void SetMeAsViewPoint()
    {
        cameraTrackManager.LoadCameraTrackSpots(myIndex, OrthographicSize);
    }


}
