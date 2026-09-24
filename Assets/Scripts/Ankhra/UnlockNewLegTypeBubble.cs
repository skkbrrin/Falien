using System.Drawing;
using UnityEngine;

public class UnlockNewLegTypeBubble : MonoBehaviour
{
    [Header("Refs")]
    public SwitchLegManager SLM;
    public SpriteRenderer MyShowingSprite;

    public LegType myNewLegType;

    public Sprite StiffLegSprite;
    public Sprite BouncyLegSprite;
    public Sprite HeavyLegSprite;
    public Sprite TentacleLegSprite;

    public Animator blowAnimator;


    private void Awake()
    {
        SLM = FindFirstObjectByType<SwitchLegManager>();
    }

    public void Start()
    {
        LoadSpriteContext();
    }

    private void Update()
    {
        CheckKillBubble();
    }

    public void CheckKillBubble()
    {
        if (SLM.legTypeAllowPlayerSelect.Contains(myNewLegType))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (SLM == null)
            {
                SLM = FindFirstObjectByType<SwitchLegManager>();
            }
            SLM.UnlockNewParts(myNewLegType);
            DestroyEvent();
        }
    }

    public void LoadSpriteContext()
    {
        switch (myNewLegType)
        {
            case LegType.StiffLeg:
                MyShowingSprite.sprite = StiffLegSprite;
                break;

            case LegType.BouncyLeg:
                MyShowingSprite.sprite = BouncyLegSprite;
                break;

            case LegType.HeavyLeg:
                MyShowingSprite.sprite = HeavyLegSprite;
                break;

            case LegType.Tentacle:
                MyShowingSprite.sprite = TentacleLegSprite;
                break;
        }
    }



    public void DestroyEvent()
    {
        blowAnimator.SetTrigger("active");

        MyShowingSprite.sprite = null;

        Destroy(gameObject, 0.5f);
    }
}
