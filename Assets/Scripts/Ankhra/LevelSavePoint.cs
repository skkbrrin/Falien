using Sirenix.OdinInspector;
using UnityEngine;

public class LevelSavePoint : MonoBehaviour
{
    [Header("Refs")]
    public GameManager gameManager;

    public ViewPoint myLevelViewPoint;

    public int MyselfActiveID => gameManager.ReviveSpots.IndexOf(this);

    public void Awake()
    {
        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Active the spot       

            if (FindAnyObjectByType<PlayerController>().isPlayerDie)
            {
                return;
            }


            if (GameManager.ReviveSpotIndex <= MyselfActiveID)
            {
                GameManager.ReviveSpotIndex = MyselfActiveID;
            }
            else
            {
                //Don't do anything
            }

            //Debug.Log("New Save Spot Arrive!");

            SetViewPoint();
        }
    }

    public void SetViewPoint()
    {
        if (myLevelViewPoint == null) return;

        myLevelViewPoint.SetMeAsViewPoint();
    }
}
