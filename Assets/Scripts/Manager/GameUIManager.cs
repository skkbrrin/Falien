using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header("Refs")]
    public GameManager gameManager;

    [Header("UI")]
    public Text myTimerText;
    public GameObject LegSelectCanvas;
    
    public void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void Update()
    {
        float time = gameManager.PlayTime;

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 10f) % 10f);

        myTimerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:0}";
    }

    public void SyncTimerInformation()
    {

    }
}
