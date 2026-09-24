using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CameraTrackManager : MonoBehaviour
{
    [Header("Refs")]
    public CameraTracker cameraTracker;

    public List<ViewPoint> CameraSpots = new List<ViewPoint>();


    public void Awake()
    {
        if (cameraTracker == null)
        {
            cameraTracker = FindFirstObjectByType<CameraTracker>();
        }
    }
    
    public void LoadCameraTrackSpots(int cameraSpotIndex, float orthographicSize = 8f)
    {
        if (cameraSpotIndex < 0 || cameraSpotIndex >= CameraSpots.Count)
        {
            Debug.LogError("You can't assign a number to this function which less then 0 or greater then the count fo CameraSpots.");
            return;
        }

        cameraTracker.target = CameraSpots[cameraSpotIndex].transform;

        cameraTracker.SetOrthographicSize(orthographicSize);

    }
}
