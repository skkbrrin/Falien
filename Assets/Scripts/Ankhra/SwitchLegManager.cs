using Sirenix.OdinInspector;
//using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SwitchLegManager : MonoBehaviour
{
    [Header("Refs")]
    public PlayerController playerController;
    public GameObject LegSelectCanvas;
    
    public LegType Set_L_LegType;
    public LegType Set_R_LegType;

    [Header("UI")]
    public Image L_Leg_Image;
    public Image R_Leg_Image;

    [Header("Legs")]
    public Sprite StiffLegSprite;
    public Sprite HeavyLegSprite;
    public Sprite TentacleLegSprite;
    public Sprite BouncyLegSprite;

    public List<LegType> legTypeAllowPlayerSelect;
    public int currentLegIndex_L = 0;
    public int currentLegIndex_R = 0;

    private void Start()
    {
        LoadTheLegPlayerHave();
    }

    [Button]
    public void ResetAll()
    {
        PlayerPrefs.SetInt("HasStiffLeg", 0);
        PlayerPrefs.SetInt("HasBouncyLeg", 0);
        PlayerPrefs.SetInt("HasHeavyLeg", 0);
        PlayerPrefs.SetInt("HasTentacle", 0);

        PlayerPrefs.SetInt("Left_LegType", 0);
        PlayerPrefs.SetInt("Left_LegType", 1);
    }

    public void LoadTheLegPlayerHave()
    {

        if (PlayerPrefs.GetInt("HasStiffLeg") > 0)
        {
            UnlockNewParts(LegType.StiffLeg);
        }

        if (PlayerPrefs.GetInt("HasBouncyLeg") > 0)
        {
            UnlockNewParts(LegType.BouncyLeg);
        }

        if (PlayerPrefs.GetInt("HasHeavyLeg") > 0)
        {
            UnlockNewParts(LegType.HeavyLeg);
        }

        if (PlayerPrefs.GetInt("HasTentacle") > 0)
        {
            UnlockNewParts(LegType.Tentacle);
        }
    }


    public void ShowingInit()
    {
        Set_L_LegType = playerController.Left_LegType;
        Set_R_LegType = playerController.Right_LegType;

        currentLegIndex_L = legTypeAllowPlayerSelect.IndexOf(Set_L_LegType);
        currentLegIndex_R = legTypeAllowPlayerSelect.IndexOf(Set_R_LegType);

        ShowImage();
    }

    public void ShowImage()
    {
        SetLegImage(L_Leg_Image, Set_L_LegType);
        SetLegImage(R_Leg_Image, Set_R_LegType);
    }
    public void SetLegImage(Image targetImage, LegType legType)
    {
        switch (legType)
        {
            case LegType.Tentacle:
                targetImage.sprite = TentacleLegSprite;
                break;

            case LegType.StiffLeg:
                targetImage.sprite = StiffLegSprite;
                break;

            case LegType.HeavyLeg:
                targetImage.sprite = HeavyLegSprite;
                break;

            case LegType.BouncyLeg:
                targetImage.sprite = BouncyLegSprite;
                break;
        }
    }

    public void L_button_add()
    {
        if (legTypeAllowPlayerSelect.Count == 0)
            return;

        currentLegIndex_L++;

        if (currentLegIndex_L >= legTypeAllowPlayerSelect.Count)
        {
            currentLegIndex_L = 0;
        }

        Set_L_LegType = legTypeAllowPlayerSelect[currentLegIndex_L];

        ShowImage();
    }

    public void L_button_minus()
    {
        if (legTypeAllowPlayerSelect.Count == 0)
            return;

        currentLegIndex_L--;

        if (currentLegIndex_L < 0)
        {
            currentLegIndex_L = legTypeAllowPlayerSelect.Count - 1;
        }

        Set_L_LegType = legTypeAllowPlayerSelect[currentLegIndex_L];

        ShowImage();
    }

    public void R_button_add()
    {
        if (legTypeAllowPlayerSelect.Count == 0)
            return;

        currentLegIndex_R++;

        if (currentLegIndex_R >= legTypeAllowPlayerSelect.Count)
        {
            currentLegIndex_R = 0;
        }

        Set_R_LegType = legTypeAllowPlayerSelect[currentLegIndex_R];

        ShowImage();
    }

    public void R_button_minus()
    {
        if (legTypeAllowPlayerSelect.Count == 0)
            return;

        currentLegIndex_R--;

        if (currentLegIndex_R < 0)
        {
            currentLegIndex_R = legTypeAllowPlayerSelect.Count - 1;
        }

        Set_R_LegType = legTypeAllowPlayerSelect[currentLegIndex_R];

        ShowImage();
    }

    public void UnlockNewParts(LegType newLegType)
    {
        if (!legTypeAllowPlayerSelect.Contains(newLegType))
        {
            legTypeAllowPlayerSelect.Add(newLegType);
        }

        int n = 0;

        switch (newLegType)
        {
            case LegType.StiffLeg:
                PlayerPrefs.SetInt("HasStiffLeg", 1);
                break;
            case LegType.HeavyLeg:
                PlayerPrefs.SetInt("HasHeavyLeg", 1);
                break;
            case LegType.BouncyLeg:
                PlayerPrefs.SetInt("HasBouncyLeg", 1);
                break;
            case LegType.Tentacle:
                PlayerPrefs.SetInt("HasTentacle", 1);
                break;
        }

        PlayerPrefs.Save();
    }


    bool _isLegCanvasActive = false;
    public void OpenLegCanvas()
    {
        LegSelectCanvas.SetActive(true);
        _isLegCanvasActive = true;
        ShowingInit();
        Time.timeScale = 0;
    }

    public void CloseLegCanvas()
    {
        _isLegCanvasActive = false;
        Time.timeScale = 1;

        PlayerPrefs.SetInt("Left_LegType", (int)Set_L_LegType);
        PlayerPrefs.SetInt("Right_LegType", (int)Set_R_LegType);

        playerController.Left_LegType = Set_L_LegType;
        playerController.Right_LegType = Set_R_LegType;

        PlayerPrefs.Save();

        playerController.InitLeg(Set_L_LegType, Set_R_LegType);

        LegSelectCanvas.SetActive(false);
    }

    public void Update()
    {
        if (playerController == null)
        {
            playerController = FindFirstObjectByType<PlayerController>();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            //Open the panel and time stop;
            if (_isLegCanvasActive)
            {
                CloseLegCanvas();
            }
            else
            {
                OpenLegCanvas();
            }
        }
    }
}
