using UnityEngine;

public static class PlayerData
{
    public static Language myLanguage = Language.En;

    static PlayerData()
    {
        SetLanguage(Language.En);
    }

    public static void SetLanguageToEn()
    {
        SetLanguage(Language.En);
    }

    public static void SetLanguageToJp()
    {
        SetLanguage(Language.Jp);
    }

    public static void SetLanguageToFr()
    {
        SetLanguage(Language.Fr);
    }

    private static void SetLanguage(Language lang)
    {
        myLanguage = lang;
    }
}