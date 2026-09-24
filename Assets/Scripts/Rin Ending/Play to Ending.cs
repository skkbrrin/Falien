using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaytoEnding : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene("Ending");
    }
}
