using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private string englishText;
    [SerializeField] private string japaneseText;
    [SerializeField] private string frenchText;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        switch (PlayerData.myLanguage)
        {
            case Language.Jp:
                text.text = japaneseText;
                break;

            case Language.En:
                text.text = englishText;
                break;

            case Language.Fr:
                text.text = frenchText;
                break;
        }
    }
}