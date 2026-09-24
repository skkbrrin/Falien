using UnityEngine;

public class Spike : MonoBehaviour
{
    public GameManager gamemanager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gamemanager.PlayerDieCall();
        }
    }
}
