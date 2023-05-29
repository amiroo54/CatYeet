using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Cat : MonoBehaviour
{
    MainInput Input;
    Rigidbody2D rb;
    [SerializeField] float MaxSpeed;
    float Momentum;
    [SerializeField] float JumpHeight;
    [SerializeField] float Acceleration;
    [SerializeField] float Decceleration;
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask Jumpable;
    [SerializeField] Vector2 GroundCheckRadius;
    GameObject Donkey;
    Animator Animator;
    bool _IsFacingRight;
    private void Start()
    {
        Donkey = GameManager.Donkey;
        Input = new MainInput();
        Input.Enable();
        rb = this.GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
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

        if (Input.MoveHorse.Jump.WasPerformedThisFrame() && CheckForGround())
        {
            Jump();
        }
        if (!CheckForGround())
        {
            Animator.SetBool("IsInAir", true);
        }
        else
        {
            Animator.SetBool("IsInAir", false);
        }
        Animator.SetFloat("Speed", Mathf.Abs(Input.MoveHorse.Move.ReadValue<float>()));
        if (Input.MoveHorse.Switch.WasPerformedThisFrame())
        {
            GameManager.IsCat = false;
        }
    }
    private void FixedUpdate()
    {
        Momentum = rb.velocity.x;


        if (Input.MoveHorse.Move.ReadValue<float>() != 0)
        {
            float speedDif = Input.MoveHorse.Move.ReadValue<float>() * MaxSpeed - Momentum;
            float movementForce = Acceleration * speedDif;

            Momentum += movementForce;
            Momentum = Mathf.Clamp(Momentum, -MaxSpeed, MaxSpeed);

            rb.velocity = new Vector2(Momentum, rb.velocity.y);
        }
        else
        {
            if (CheckForGround())
            {
                float speedDif = Input.MoveHorse.Move.ReadValue<float>() * MaxSpeed - Momentum;
                float movementForce = Decceleration * speedDif;

                Momentum += movementForce;
                Momentum = Mathf.Clamp(Momentum, -MaxSpeed, MaxSpeed);

                rb.velocity = new Vector2(Momentum, rb.velocity.y);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Donkey"))
        {
            GrabCat();
            
        }
    }
    
    void Jump()
    {
        rb.AddForce(new Vector2(0, JumpHeight));
    }
    public void GrabCat()
    {
        GameManager.IsCatAlone = false;
        GameManager.IsCat = false;
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
    
    bool IsFacingRight
    {
        set
        {
            if (value && !_IsFacingRight)
            {
                Animator.gameObject.transform.localScale = new Vector3(.75f, .75f, .75f);
                _IsFacingRight = value;
            }
            if (!value && _IsFacingRight)
            {
                Animator.gameObject.transform.localScale = new Vector3(-.75f, .75f, .75f);
                _IsFacingRight = value;
            }

        }
    }
}
