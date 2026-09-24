using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Control Points")]
    public Rigidbody2D leftControlPoint;
    public Rigidbody2D rightControlPoint;

    public Rigidbody2D myBodyRb2D;


    [Header("Camera")]
    [SerializeField]
    private Camera targetCamera;


    [Header("Legs Type")]
    [EnumToggleButtons]
    public LegType Left_LegType;

    public GameObject CurrentLeftLegObject;


    [EnumToggleButtons]
    public LegType Right_LegType;

    public GameObject CurrentRightLegObject;


    [Header("Legs Prefab")]
    public GameObject stiffLeg;
    public GameObject tentacle;
    public GameObject heavyLeg;
    public GameObject bouncyLeg;


    private leg_father _currentLeftLeg;
    private leg_father _currentRightLeg;

    private LegInputState _leftInput;
    private LegInputState _rightInput;

    private bool _mouse0Held;
    private Vector2 _mouseScreenPosition;


    [Header("Calculation")]
    public bool isPlayerDie = false;
    [Header("Input State")]
    public bool isInputting_L;
    public bool isInputting_R;
    [Button]
    public void DO_PlayerInit()
    {
        PlayerInit();
    }


    private void Awake()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }


    }


    private void Start()
    {
        PlayerInit();
    }


    private void Update()
    {
        if (isPlayerDie)
        {
            isInputting_L = false;
            isInputting_R = false;
            return;
        }

        ReadKeyboardInput();
        ReadMouseInput();

        UpdateInputState();
    }
    private void UpdateInputState()
    {
        isInputting_L =
            _leftInput.Left ||
            _leftInput.Right ||
            _leftInput.Up ||
            _leftInput.Down;

        isInputting_R =
            _rightInput.Left ||
            _rightInput.Right ||
            _rightInput.Up ||
            _rightInput.Down;
    }
    private void FixedUpdate()
    {
        _currentLeftLeg?.HandleInput(
            _leftInput
        );

        _currentRightLeg?.HandleInput(
            _rightInput
        );
    }


    private void ReadKeyboardInput()
    {
        _leftInput = new LegInputState
        {
            Left = Input.GetKey(KeyCode.A),
            Right = Input.GetKey(KeyCode.D),
            Up = Input.GetKey(KeyCode.W),
            Down = Input.GetKey(KeyCode.S)
        };


        _rightInput = new LegInputState
        {
            Left = Input.GetKey(KeyCode.LeftArrow),
            Right = Input.GetKey(KeyCode.RightArrow),
            Up = Input.GetKey(KeyCode.UpArrow),
            Down = Input.GetKey(KeyCode.DownArrow)
        };
    }


    private void ReadMouseInput()
    {
        _mouse0Held =
            Input.GetMouseButton(0);

        _mouseScreenPosition =
            Input.mousePosition;


        if (!_mouse0Held)
            return;


        ApplyMouseDirectionToRightInput();
    }


    private void ApplyMouseDirectionToRightInput()
    {
        if (rightControlPoint == null)
            return;


        Camera cam =
            targetCamera != null
                ? targetCamera
                : Camera.main;


        if (cam == null)
            return;


        Vector2 targetPosition =
            rightControlPoint.worldCenterOfMass;


        Vector2 mouseWorldPosition =
            cam.ScreenToWorldPoint(
                _mouseScreenPosition
            );


        Vector2 direction =
            mouseWorldPosition -
            targetPosition;


        if (direction.sqrMagnitude <
            0.000001f)
        {
            return;
        }


        direction.Normalize();


        Vector3 cameraLocalDirection =
            cam.transform.InverseTransformDirection(
                direction
            );


        if (Mathf.Abs(cameraLocalDirection.x) >
            Mathf.Abs(cameraLocalDirection.y))
        {
            if (cameraLocalDirection.x > 0f)
            {
                _rightInput.Right = true;
            }
            else
            {
                _rightInput.Left = true;
            }
        }
        else
        {
            if (cameraLocalDirection.y > 0f)
            {
                _rightInput.Up = true;
            }
            else
            {
                _rightInput.Down = true;
            }
        }
    }


    public void PlayerInit()
    {
        //Load prevoius leg setting
        Left_LegType = (LegType)PlayerPrefs.GetInt("Left_LegType");
        Right_LegType = (LegType)PlayerPrefs.GetInt("Right_LegType");


        InitLeg(
            Left_LegType,
            Right_LegType
        );
    }


    public void InitLeg(
        LegType leftLegData,
        LegType rightLegData)
    {
        if (CurrentLeftLegObject != null)
        {
            Destroy(
                CurrentLeftLegObject
            );
        }


        if (CurrentRightLegObject != null)
        {
            Destroy(
                CurrentRightLegObject
            );
        }


        _currentLeftLeg = null;
        _currentRightLeg = null;

        leftControlPoint = null;
        rightControlPoint = null;


        LoadLeg(
            leftLegData,
            true
        );

        LoadLeg(
            rightLegData,
            false
        );
    }


    public void LoadLeg(
        LegType legType,
        bool isLeft)
    {
        GameObject spawnTargetLegType =
            GetLegPrefab(legType);


        if (spawnTargetLegType == null)
        {
            Debug.LogError(
                $"Leg prefab not found for {legType}."
            );

            return;
        }


        GameObject newLegObject =
            Instantiate(
                spawnTargetLegType,
                myBodyRb2D.transform.position,
                myBodyRb2D.transform.rotation
            );


        leg_father leg =
            newLegObject.GetComponent<leg_father>();


        if (leg == null)
        {
            Debug.LogError(
                $"{newLegObject.name} does not contain leg_father."
            );

            Destroy(newLegObject);

            return;
        }


        if (isLeft)
        {
            CurrentLeftLegObject =
                newLegObject;
        }
        else
        {
            CurrentRightLegObject =
                newLegObject;
        }


        leg.LegInit(isLeft);
    }


    private GameObject GetLegPrefab(
        LegType legType)
    {
        switch (legType)
        {
            case LegType.StiffLeg:
                return stiffLeg;

            case LegType.BouncyLeg:
                return bouncyLeg;

            case LegType.HeavyLeg:
                return heavyLeg;

            case LegType.Tentacle:
                return tentacle;

            default:
                return stiffLeg;
        }
    }


    public void RegisterLeg(
        leg_father leg,
        bool isLeft)
    {
        if (leg == null)
            return;


        if (isLeft)
        {
            _currentLeftLeg = leg;

            leftControlPoint =
                leg.myControlPoint;
        }
        else
        {
            _currentRightLeg = leg;

            rightControlPoint =
                leg.myControlPoint;
        }
    }


    public void PlayerDie()
    {
        isPlayerDie = true;
    }
}

[System.Serializable]
public enum LegType
{
    StiffLeg = 0 ,
    BouncyLeg = 1,
    HeavyLeg = 2,
    Tentacle =3
}