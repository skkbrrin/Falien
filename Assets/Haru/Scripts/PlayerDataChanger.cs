using UnityEngine;

public class PlayerDataChanger : MonoBehaviour
{
    public void ChangeLanguageToJP()
    {
        PlayerData.SetLanguageToJp();
    }
    public void ChangeLanguageToEn()
    {
        PlayerData.SetLanguageToEn();
    }
        public void ChangeLanguageToFr()
    {
        PlayerData.SetLanguageToFr();
    }
}
