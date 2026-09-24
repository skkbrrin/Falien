using UnityEngine;

public class DeathSpike : MonoBehaviour
{
    public GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player dead");

            gameManager.PlayerDieCall();
        }
    }
}