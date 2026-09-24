using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening.Core.Easing;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Refs")]
    public GameObject PlayerPrefab;
    public GameObject GameResultCanvas;

    [Tooltip("This will auto set when game start, you don't have to assign it in inspector")]
    public GameObject PlayerGameObject;

    public Animator cutSceneAnimator;

    public CameraTracker cameraTracker;

    [Header("PlayerDeathVoice")]
    public AudioSource playerDeathAudio;
    public AudioClip playerDeathClip;

    [Header("Revive")]
    public List<LevelSavePoint> ReviveSpots = new List<LevelSavePoint>();

    [ShowInInspector]
    public static int ReviveSpotIndex = 0;

    public int func_RevSpot = 0;
    [Button]
    public void funcForText_SetRevSpot()
    {
        ReviveSpotIndex = func_RevSpot;
    }


    [Header("Stats")]
    public float PlayTime = 0;

    public void Start()
    {
        GameInit();
    }

    float _pressFTime= 0; 

    public void Update()
    {
        PlayTime += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            RetryStage();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            _pressFTime = 0;
        }
        if (Input.GetKey(KeyCode.F))
        {
            _pressFTime += Time.deltaTime;
        }

        if (_pressFTime > 1f)
        {
            PlayerDieCall();
        }

    }

    static public void LoadFromMenu()
    {
        ReviveSpotIndex = 0;
        PlayerPrefs.SetInt("Left_LegType", 1);
        PlayerPrefs.SetInt("Right_LegType", 0);

        PlayerPrefs.SetInt("HasStiffLeg", 0);
        PlayerPrefs.SetInt("HasBouncyLeg", 0);
        PlayerPrefs.SetInt("HasHeavyLeg", 0);
        PlayerPrefs.SetInt("HasTentacle", 0);
    }


    public void GameInit()
    {
        //Revive at revive spots[revive spot index]

        if (ReviveSpots.Count == 0)
        {
            Debug.LogError("The ReviveSpots is empty");
            return;
        }

        if (ReviveSpotIndex < 0)
        {
            Debug.LogError("The index shouldn't less then revive spot index");
            ReviveSpotIndex = 0;
        }

        if (ReviveSpotIndex >= ReviveSpots.Count)
        {
            Debug.LogError("The Index shouldn't greater then revive spots count total");
            ReviveSpotIndex = 0;    
        }

        PlayerGameObject = Instantiate(PlayerPrefab, ReviveSpots[ReviveSpotIndex].transform.position, ReviveSpots[ReviveSpotIndex].transform.rotation);

        cameraTracker.target = PlayerGameObject.transform.GetChild(0).transform;
    }


    [Button]
    public void PlayerDieCall()
    {
        PlayerController PC = FindFirstObjectByType<PlayerController>();

        PC.PlayerDie();
        PlayerLose();
    }





    public void PlayerLose()
    {
        GameResultCanvas.SetActive(true);

        // PlayDeathSE
        playerDeathAudio.PlayOneShot(playerDeathClip);
    }

    public void PlayerWin()
    {

    }

    [Button]
    public void RetryStage()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        cutSceneAnimator.SetBool("CutOut",true);

        StartCoroutine(Delay(
            () =>
            {
                SceneManager.LoadScene(currentSceneIndex);
            }
            ,0.5f
            ));

    }

    IEnumerator Delay(Action func, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        func();
    }
}
