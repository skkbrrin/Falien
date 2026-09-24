using System.IO;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SaveSystem : MonoBehaviour
{
    public static SaveFile Instance;

    [Header("Save File Debug")]
    [SerializeField] private bool SFSync;
    public SaveFile SFShowCase;

    public static string saveFilePath = "SaveFile0";
    private void Awake()
    {
        Debug.Log("Loading Save System");
        LoadSaveFile();
    }


    private void Update()
    {
        if (SFSync)
        {
            SFShowCase = Instance;
        }
    }


    public static void SaveSaveFile()
    {
        string fullPath =
            Path.Combine(
                Application.persistentDataPath,
                saveFilePath
            );

        string json =
            JsonUtility.ToJson(Instance, true);

        File.WriteAllText(
            fullPath,
            json
        );
    }


    public static void LoadSaveFile()
    {
        string fullPath =
            Path.Combine(
                Application.persistentDataPath,
                saveFilePath
            );

        if (!File.Exists(fullPath))
        {
            Debug.Log(
                "Save file not found. Creating a new save file."
            );

            ResetSaveFile();
            return;
        }

        string json =
            File.ReadAllText(fullPath);

        SaveFile loadedSave =
            JsonUtility.FromJson<SaveFile>(json);

        if (loadedSave == null)
        {
            Debug.LogWarning(
                "Save file could not be loaded. Creating a new save file."
            );

            ResetSaveFile();
            return;
        }

        Instance = loadedSave;
    }


    public static void ResetSaveFile()
    {
        Instance = new SaveFile();

        PlayerPrefs.SetInt("HasStiffLeg", 0);
        PlayerPrefs.SetInt("HasBouncyLeg", 0);
        PlayerPrefs.SetInt("HasHeavyLeg", 0);
        PlayerPrefs.SetInt("HasTentacle", 0);

        SaveSaveFile();
    }
}


[System.Serializable]
public class SaveFile
{
    public int logTimes = 0;
}