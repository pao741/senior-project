using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    public float moveSpeed = 5f;

    [Space]
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public Camera cam;
    public Transform playerGFX;

    [Space]
    [Header("Dash")]
    public float dashCooldown = 1f;
    private float nextDashTimer;
    public float dashSpeed = 250f;
    
    private bool isDashing;
    private float currentDashSpeed;
    private Vector2 dashDirection;

    private float currentAttackMovementSpeed;
    private Vector2 attackDirection;

    Vector2 movement;
    Vector2 mousePos;
    Vector2 lookDir;

    Player player;

    private enum State
    {
        Normal,
        Dash,
        Attack,
    }

    private State state;

    // Start is called before the first frame update
    void Awake()
    {
        state = State.Normal;
    }

    void Start()
    {
        player = GetComponent<Player>();
        Debug.Log(player);
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isPaused || Player.isDead)
        {
            return;
        }
        switch (state)
        {
            case State.Normal:
                // Input
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");

                mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                lookDir = mousePos - rb.position;

                // Animator

                animator.SetFloat("MouseHorizontal", lookDir.x);
                animator.SetFloat("MouseVertical", lookDir.y);

                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.sqrMagnitude);

                if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
                {
                    animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
                    animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
                }

                if (Input.GetButtonDown("Jump") && Time.time > nextDashTimer)
                {
                    if (movement.x != 0 || movement.y != 0)
                    {
                        player.setInvulnerable(true);
                        dashDirection = new Vector2(movement.x, movement.y).normalized;
                        currentDashSpeed = 20f;
                        state = State.Dash;
                    }
                }

                /*if (Input.GetButtonDown("Fire1"))
                {
                    attackDirection = lookDir;
                    currentAttackMovementSpeed = 20f;
                    state = State.Attack;
                }*/
                break;

            case State.Dash:
                Dash();
                break;

            case State.Attack:
                Attack();
                break;
        }
    }

    void FixedUpdate()
    {
        switch(state)
        {
            case State.Normal:
                rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
                break;

            case State.Dash:
                rb.velocity = dashDirection.normalized * currentDashSpeed;
                break;

            case State.Attack:
                /*rb.velocity = attackDirection.normalized * currentAttackMovementSpeed;*/
                break;

        }

        /*setAnimation(movement);*/
    }

    void Dash()
    {
        float DashSpeedDropMultiplier = 5f;
        currentDashSpeed -= currentDashSpeed * DashSpeedDropMultiplier * Time.deltaTime;

        float dashSpeedMinimum = 10f;
        /*float dashSpeedMinimumIFrame = 12f;

        if (currentAttackMovementSpeed < dashSpeedMinimumIFrame)
        {
            player.setInvulnerable(false);
        }*/

        if (currentDashSpeed < dashSpeedMinimum)
        {
            player.setInvulnerable(false);

            state = State.Normal;
            Invoke("ResetDash", dashCooldown);
            nextDashTimer = Time.time + dashCooldown;
        }
    }

    public void SetAttackState()
    {
        /*attackDirection = lookDir;
        currentAttackMovementSpeed = 10f;
        state = State.Attack;*/
    }

    void Attack()
    {

        /*float AttackMovementSpeedDropMultiplier = 10f;
        currentAttackMovementSpeed -= currentAttackMovementSpeed * AttackMovementSpeedDropMultiplier * Time.deltaTime;

        float attackMovementSpeedMinimum = 5f;
        
        if (currentAttackMovementSpeed < attackMovementSpeedMinimum)
        {
            state = State.Normal;
            *//*nextDashTimer = Time.time + dashCooldown;*//*
        }*/
    }

    void ResetDash()
    {
        isDashing = false;
        /*animator.Play("Idle");*/
    }

    void setAnimation(Vector2 movement)
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 ||Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
        }

        /*if (force.x >= 0.01f)
        {
            playerGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            playerGFX.localScale = new Vector3(-1f, 1f, 1f);
        }*/
    }

    public void Knockback(float knockback)
    {

    }
}
