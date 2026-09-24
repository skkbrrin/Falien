using UnityEngine;
using TMPro;

public class LoadingTip : MonoBehaviour
{
    public TMP_Text tipText;

    string[,] tips =
    {
        // English = 0
        {
            "Helpful Tip: Watch your step!",
            "Helpful Tip: Don't hit your head!",
            "Helpful Tip: Different legs have different abilities.",
            "Helpful Tip: Be careful when moving!",
            "Helpful Tip: Work together with your partner!"
        },

      
        // Japanese = 2
        {
            "ヒント 'あしもとに きをつけよう'",
            "ヒント 'あたまを ぶつけないように'",
            "ヒント 'あしによって のうりょくが ちがいます'",
            "ヒント 'いどうするときは きをつけよう'",
            "ヒント 'パートナーと きょうりょくしよう'"
        },

       

        // French = 4
        {
            "Astuce : Faites attention où vous marchez !",
            "Astuce : Ne vous cognez pas la tête !",
            "Astuce : Chaque jambe possède des capacités différentes.",
            "Astuce : Faites attention lorsque vous vous déplacez !",
            "Astuce : Travaillez avec votre partenaire !"
        },

       
        
    };

    int currentTip = 0;

    void Start()
    {
        ShowTip();
    }

    public void ShowTip()
    {
        int language = (int)PlayerData.myLanguage;

        tipText.text = tips[language, currentTip];
    }

    public void NextTip()
    {
        currentTip++;

        if (currentTip >= tips.GetLength(1))
        {
            currentTip = 0;
        }

        ShowTip();
    }
}