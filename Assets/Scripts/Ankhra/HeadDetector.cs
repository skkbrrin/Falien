using UnityEngine;

public class HeadDetector : MonoBehaviour
{
    [Header("Refs")]
    public PlayerController myPlayerController;
    public GameManager gameManager;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Player die!");

            SentPlayerDieInformationToGameManager();


           // myPlayerController.PlayerDie();
            //gameManager.PlayerLose();
        }
    }
    
    public void SentPlayerDieInformationToGameManager()
    {
        gameManager.PlayerDieCall();
    }

    public void ReceivePlayerDie()
    {
        myPlayerController.PlayerDie();
        gameManager.PlayerLose();
    }
}
