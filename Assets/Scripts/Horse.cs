using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class Horse : MonoBehaviour
{
    MainInput Input;
    [SerializeField] float MaxSpeed;
    float Momentum;
    [SerializeField] float JumpHeight;
    [SerializeField] float NoCatJumpHeight;
    [SerializeField] float Acceleration;
    [SerializeField] float Decceleration;
    Rigidbody2D rb;
    [SerializeField] GameObject cat;
    Rigidbody2D CatRb;
    [SerializeField] Transform GroundCheck;
    [SerializeField] Vector2 GroundCheckRadius;
    [SerializeField] LayerMask Jumpable;
    [SerializeField] float ThrowPower;
    [SerializeField] int PointsNum;
    [SerializeField] float PointsDistance;
    [SerializeField] GameObject Pointprefab;
    [SerializeField] float VirualThrowPower;
    GameObject[] Points;
    FixedJoint2D Joint;
    [SerializeField] Animator Animator;
    [SerializeField] Sprite WithCat;
    [SerializeField] Sprite WithoutCat;
    [SerializeField] SpriteRenderer BodyRenderer;
    AudioSource source;
    bool _IsFacingRight;
    private void Start()
    {
        Points = new GameObject[PointsNum];
        for (int i = 0; i < PointsNum; i++)
        {
            Points[i] = Instantiate(Pointprefab, transform);
        }
        Joint = GetComponent<FixedJoint2D>();
        CatRb = cat.GetComponent<Rigidbody2D>();
        rb = this.GetComponent<Rigidbody2D>();
        Input = new MainInput();
        Input.Enable();
        GetComponent<FixedJoint2D>().connectedBody = CatRb;
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        float InputMove = Input.MoveHorse.Move.ReadValue<float>();
        if (InputMove < -.1)
        {
            IsFacingRight = false;
        }
        if (InputMove > .1)
        {
            IsFacingRight = true;
        }
        if (CheckForGround() && Mathf.Abs(InputMove) > .1 && !source.isPlaying)
        {
            source.Play();
            
        }
        if (!CheckForGround())
        {
            source.Pause();
            Animator.SetBool("IsInAir", true);
        }
        else
        {
            Animator.SetBool("IsInAir", false);
        }
        if (Mathf.Abs(InputMove) < .1)
        {
            source.Pause();
        }
        
        if (Input.MoveHorse.Jump.WasPerformedThisFrame() && CheckForGround())
        {
            Jump();
        }
        if (Input.MoveHorse.Throw.WasReleasedThisFrame() && !GameManager.IsCatAlone)
        {
            ThrowCat();
        }
        if (Input.MoveHorse.Throw.IsInProgress() && !GameManager.IsCatAlone)
        {
            IsThrowing = true;
            for (int i = 0; i < PointsNum; i++)
            {
                Points[i].transform.position = CalculateArch(i / PointsDistance);
            }
        }
        else
        {
            IsThrowing = false;
        }
        Animator.SetFloat("Speed", Mathf.Abs(Input.MoveHorse.Move.ReadValue<float>()));
        if (Input.MoveHorse.Switch.WasPerformedThisFrame() && GameManager.IsCatAlone)
        {
            GameManager.IsCat = true;
        }
    }

    private void FixedUpdate()
    {
        Momentum = rb.velocity.x;
        float InputMove = Input.MoveHorse.Move.ReadValue<float>();
        
        if (InputMove != 0)
        {
            float speedDif = InputMove * MaxSpeed - Momentum;
            float movementForce = Acceleration * speedDif;

            Momentum += movementForce;
            Momentum = Mathf.Clamp(Momentum, -MaxSpeed, MaxSpeed);
            rb.velocity = new Vector2(Momentum, rb.velocity.y);
        }
        else
        {
            if (CheckForGround())
            {
                float speedDif = InputMove * MaxSpeed - Momentum;
                float movementForce = Decceleration * speedDif;

                Momentum += movementForce;
                Momentum = Mathf.Clamp(Momentum, -MaxSpeed, MaxSpeed);
                rb.velocity = new Vector2(Momentum, rb.velocity.y);
            }
        }
    }
    void Jump()
    {
        rb.AddForce(new Vector2(0, GameManager.IsCatAlone ? NoCatJumpHeight : JumpHeight));
    }
    bool CheckForGround()
    {
        bool isOnGround;
        if (Physics2D.OverlapBox(GroundCheck.position, GroundCheckRadius, 0f, Jumpable))
        {
            isOnGround = true;
           
        }
        else
        {
            isOnGround = false;
            
        }
        return isOnGround;
    }
    void ThrowCat()
    {
        GameManager.IsCatAlone = true;
        BodyRenderer.sprite = WithoutCat;
        Vector2 ThrowDir = Camera.main.ScreenToWorldPoint(Input.MoveHorse.Aim.ReadValue<Vector2>()) - (Vector3)rb.position;
        CatRb.AddForce(ThrowPower * ThrowDir);
        GameManager.IsCat = true;
       
    }

    Vector2 CalculateArch(float t)
    {
        Vector2 CurrentPos;
        Vector2 ThrowDir = Camera.main.ScreenToWorldPoint(Input.MoveHorse.Aim.ReadValue<Vector2>()) - (Vector3)rb.position;
        
        CurrentPos = ((Vector2)transform.position - Joint.connectedAnchor + (ThrowDir * VirualThrowPower * t) + (Physics2D.gravity * CatRb.mass * (t*t)));
        
        return CurrentPos;
    }
    public void ChangeSpriteToCat()
    {
        BodyRenderer.sprite = WithCat;
    }
    bool IsThrowing
    {
        set
        {
            for (int i = 0; i < PointsNum; i++)
            {
                Points[i].SetActive(value);
            }
        }
    }
    bool IsFacingRight
    {
        set
        {
            if (value && !_IsFacingRight)
            {
                Animator.gameObject.transform.localScale = new Vector3(-.75f, .75f, .75f);
                _IsFacingRight = value;
            }
            if (!value && _IsFacingRight)
            {
                Animator.gameObject.transform.localScale = new Vector3(.75f, .75f, .75f);
                _IsFacingRight = value;
            }

        }
    }
}
    
