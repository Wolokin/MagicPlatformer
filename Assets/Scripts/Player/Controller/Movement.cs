using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

// using DG.Tweening;

namespace Controller {

public class Movement : MonoBehaviour
{
    private PlayerCollision coll;
    [HideInInspector]
    public Rigidbody2D rb;
    // private AnimationScript anim;

    private InputActionAsset inputAsset;
    private InputActionMap player;

    [Space]
    [Header("Stats")]
    public float wallJumpLerp = 10;


    [Space]
    [Header("State")]
    public bool canMove;
    public bool isWallGrabing;
    public bool isJumping;
    public bool isWallJumping;
    public bool isWallSliding;
    public bool isDashing;
    private bool isGroundTouching;
    public int side = 1;

    [Space]
    [Header("Polish")]
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        coll = GetComponent<PlayerCollision>();
        rb = GetComponent<Rigidbody2D>();

        //playerActionsAsset = new ThirdPersonActionsAsset();
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
    }

    private void OnEnable()
    {
        player.Enable();
    }

    private void OnDisable()
    {
        player.Disable();
    }

    private void Update()
    {
        if (coll.onGround && !isDashing)
        {
            isWallJumping = false;
            GetComponent<BetterJumping>().enabled = true;
        }

        if (isWallGrabing && !isDashing && false)
        {
            rb.gravityScale = 0;
            //if (x < -.2f || .2f < x)
            //    rb.velocity = new Vector2(rb.velocity.x, 0);

            //float speedModifier = y > 0 ? .5f : 1;

            //rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
            }
        else
        {
            rb.gravityScale = 3;
        }

        

        if (!coll.onWall)
            isWallSliding = false;

        if (coll.onGround && !isGroundTouching)
        {
            isDashing = false;
            isGroundTouching = true;
        }

            
    }



    public void Push(Vector2 dir, float speed, float immobileTimeSec)
    {
        //rb.velocity = Vector2.zero;

        rb.velocity += dir.normalized * speed;
        StartCoroutine(PushWait(immobileTimeSec));
    }

    IEnumerator PushWait(float timeSec)
    {
        rb.gravityScale = 0;
        GetComponent<BetterJumping>().enabled = false;
        isWallJumping = true;
        isDashing = true;

        yield return new WaitForSeconds(timeSec);

        rb.gravityScale = 3;
        GetComponent<BetterJumping>().enabled = true;
        isWallJumping = false;
        isDashing = false;
    }

    public void Jump(Vector2 dir, float jumpForce, bool wall)
    {
        // slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        // ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
        isJumping = true;

        // particle.Play();
    }

        public void WallJump(float jumpForce)
    {
        isWallJumping = true;
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            // anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.1f + wallDir / 1.5f), jumpForce, true);
    }

    public void WallSlide(float slideSpeed)
    {
        isWallSliding = true;
        if (isWallJumping) return;
        if(coll.wallSide != side)
        //  anim.Flip(side * -1);

        if (!canMove)
            return;

        bool pushingWall = false;
        if((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
    }

    public void Walk(Vector2 dir, float speed)
    {
        if (!canMove)
            return;

        if (isWallGrabing)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (coll.onGround)
        {
            isWallJumping = false;
            isJumping = false;
        }

        if (!isWallJumping)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }

        if (isWallGrabing || isWallSliding || !canMove)
            return;

        if (dir.x > 0)
        {
            side = 1;
            // anim.Flip(side);
        }
        else if (dir.x < 0)
        {
            side = -1;
            // anim.Flip(side);
        }
    }



    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

    void WallParticle(float vertical)
    {
        var main = slideParticle.main;

        if (isWallSliding || (isWallGrabing && vertical < 0))
        {
            slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
            main.startColor = Color.white;
        }
        else
        {
            main.startColor = Color.clear;
        }
    }

    int ParticleSide()
    {
        int particleSide = coll.onRightWall ? 1 : -1;
        return particleSide;
    }

    public void GrabWall()
    {
        isWallGrabing = true;
        isWallSliding = false;
    }

    public void ReleaseWall()
    {
        isWallGrabing = false;
        isWallSliding = false;
    }

    public void ResetPosition()
    {
        rb.position = Vector2.zero;
    }
}
}
