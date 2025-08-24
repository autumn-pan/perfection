using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    // Walkspeed
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    public float jumpForce = 4;
    public float dashForce = 25;
    private bool canDash = true;
    private float scale = 1.0f;
    private bool nuclearWarheadActive = false;
    private int nuclearWarheadTimer;
    private int nuclearWarheadLimit = 50;
    public SpriteRenderer spriteRenderer;
    public float dashDuration = 0.5f;
    private Vector2 lastInputDirection;
    public bool isGrounded = false;
    public bool canJump = true;
    public LayerMask groundLayer;

    public Transform groundBase;



    public GameObject inkParticles;



    public PressureHandler pressureHandler;

    public GameObject breathingParticles;


    public enum Animation
    {
        Running,
        Attacking,
        Jumping,
        Idle
    }

    Animation activeAnimation;

    public Sprite[] running;
    public Sprite[] attacking1;
    public Sprite[] attacking2;
    public Sprite[] jumping;
    public Sprite[] idle;
    public Sprite[] land1;

    public float attackCooldown = 1;
    public float maxAttackCooldown = 1;
    public bool canAttack;

    public bool canMove = true;

    private bool isDashing = false;
    private float lastJumpTime = 0;
    public Vector2 range;
    public bool breathing = false;

    public float damage = 100;
    public bool isLanding = false;


    public AudioSource footstep;
    public AudioSource dash;
    public AudioSource attack;
    public AudioSource breathe;

    public float footstepCooldown = 0.7f;
    public float footstepMaxCooldown = 0.7f;
    public bool canStep = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (breathing)
        {
            breathingParticles.SetActive(true);
        }
        else
        {
            breathingParticles.SetActive(false);
        }

        if (moveInput.magnitude > 0)
        {
            lastInputDirection = moveInput;
            if (canStep)
            {
                footstep.Play();
                canStep = false;
            }
        }

        if (nuclearWarheadActive)
        {
            scale *= 1.05f;
            transform.localScale = new Vector3(scale, scale, scale);
            nuclearWarheadTimer++;
            if (nuclearWarheadTimer > nuclearWarheadLimit)
            {
                scale = 1.0f;
                nuclearWarheadActive = false;
                transform.localScale = new Vector3(0.65f, 0.55f, 1f);
            }
        }

        Breathe();



        isGrounded = Physics2D.OverlapCircle(groundBase.position, 0.2f, groundLayer);

        if (isGrounded)
        {
            canJump = true;
        }


        if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            activeAnimation = Animation.Running;
            StartCoroutine(AnimationHandler());
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (activeAnimation == Animation.Running)
                activeAnimation = Animation.Idle;
            StartCoroutine(AnimationHandler());
        }



        if(!canAttack)
            attackCooldown -= Time.deltaTime;

        if (attackCooldown <= 0)
        {
            canAttack = true;
            attackCooldown = maxAttackCooldown;
        }

        if(!canStep)
            footstepCooldown -= Time.deltaTime;

        if (footstepCooldown <= 0)
        {
            canStep = true;
            footstepCooldown = footstepMaxCooldown;
        }


        Jump();
        Crouch();
    }

    void FixedUpdate()
    {
        if (rb.linearVelocityX <= moveSpeed * 1.2f && !isDashing && canMove)
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocityY);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!canJump)
                return;

            canJump = false;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            activeAnimation = Animation.Jumping;
            StartCoroutine(AnimationHandler());
        }
    }

    public void OnNuclearWarhead(InputAction.CallbackContext context)
    {
        nuclearWarheadTimer = 0;
        nuclearWarheadActive = true;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        Debug.Log("Dashed");
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {

        dash.Play();

        inkParticles.GetComponent<ParticleSystem>().Play();
        isDashing = true;
        rb.AddForce(dashForce * lastInputDirection, ForceMode2D.Impulse);
        rb.gravityScale = 0;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        rb.gravityScale = 1;
        rb.linearVelocity = Vector2.zero;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!canAttack)
            return;

        activeAnimation = Animation.Attacking;
        attack.Play();

        StartCoroutine(AnimationHandler());
        
        canAttack = false;
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, range, 0f, new Vector2(moveInput.x, 0), 99);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.GetComponent<EnemyHealthHandler>())
            {
                pressureHandler.AddPressure(1);
                hits[i].collider.gameObject.GetComponent<EnemyHealthHandler>().Damage(damage);
            }
        }
    }

    public void Breathe()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            breathing = true;
            breathe.Play();

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            breathing = false;
            breathe.Stop();
        }

        if (breathing)
        {
            pressureHandler.subtractPressure(2.5f * Time.deltaTime);
        }
    }


    public bool crouching;
    public void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            crouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            crouching = false;
        }

        if (crouching)
        {
            transform.localScale = new Vector3(0.5f, 0.25f, 1);
        }
        else
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
    }

    public void OnSkip()
    {
        if (Time.timeSinceLevelLoad > 0.1)
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index + 1);
        }
    }

    // Animation handler;
    IEnumerator AnimationHandler()
    {
        Sprite[] spriteMap;
        Animation setAnimation = activeAnimation;


        if (activeAnimation == Animation.Attacking)
        {
            spriteMap = attacking1;
        }
        else if (activeAnimation == Animation.Jumping)
        {
            spriteMap = jumping;
        }
        else if (activeAnimation == Animation.Running)
        {
            spriteMap = running;
        }
        else
        {
            spriteMap = idle;
        }

        if (isLanding)
            yield break;
            

        while (true)
        {
            for (int i = 0; i < spriteMap.Length; i++)
            {
                spriteRenderer.sprite = spriteMap[i];
                if (setAnimation != Animation.Idle)
                {
                    if (setAnimation == Animation.Jumping)
                    {
                        yield return new WaitForSeconds(0.06f);
                    }
                    else
                    {
                        yield return new WaitForSeconds(0.12f);
                    }
                }
                else
                    yield return new WaitForSeconds(0.4f);

                if (activeAnimation != setAnimation)
                {
                    yield break;
                }
            }

            if (setAnimation == Animation.Jumping)
            {
                activeAnimation = Animation.Jumping;
                StartCoroutine(fall());
                yield break;
            }

            if (setAnimation == Animation.Attacking)
            {
                activeAnimation = Animation.Idle;
                StartCoroutine(AnimationHandler());
                yield break;
            }

        }

    }

    IEnumerator fall()
    {
        isLanding = true;


        while (!isGrounded)
        {
            spriteRenderer.sprite = land1[0];
            if (activeAnimation == Animation.Attacking)
            {
                isLanding = false;
                yield break;
            }
            yield return null;
        }

        for (int i = 1; i < land1.Length; i++)
        {
            spriteRenderer.sprite = land1[i];
            yield return new WaitForSeconds(0.12f);
            if (activeAnimation == Animation.Attacking)
            {
                isLanding = false;
                yield break;
            }
        }

        if (activeAnimation == Animation.Jumping)
        {
            activeAnimation = Animation.Idle;
        }

        if (moveInput.magnitude != 0)
        {
            activeAnimation = Animation.Running;
        }

        isLanding = false;
        StartCoroutine(AnimationHandler());
    }
}


