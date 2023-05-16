 using UnityEngine;
using System.Collections;
using System;

namespace Controller
{
    public class MovementController : MonoBehaviour
    {
        private Input input;
        private Movement movement;
        private PlayerCollision coll;
        private Vector2 lastLookDirection = Vector2.left;

        private SpellSource spellSource;

        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private Vector2 dir = Vector2.zero;

        [Space]
        [Header("Stats")]
        public float speed = 10;
        public float jumpForce = 10;
        public float slideSpeed = 5;
        public float dashSpeed = 20;

        [Space]
        [Header("Limits")]
        public int jumpLimit = 1;
        public int dashLimit = 1;

        [Space]
        [Header("State")]
        public int dashed = 0;
        public int jumped = 0;
        private bool hasTouchedGround = false;


        private void OnEnable()
        {
            input = GetComponent<Input>();
            movement = GetComponent<Movement>();
            coll = GetComponent<PlayerCollision>();
            spellSource = GetComponentInChildren<SpellSource>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private Vector2 SnapDirectionTo8Cardinals(Vector2 dir)
        {
            dir.Normalize();
            if (dir.x > 0.25f && dir.y > 0.25f)
                return new Vector2(1, 1);
            if (dir.x > 0.25f && dir.y < -0.25f)
                return new Vector2(1, -1);
            if (dir.x < -0.25f && dir.y > 0.25f)
                return new Vector2(-1, 1);
            if (dir.x < -0.25f && dir.y < -0.25f)
                return new Vector2(-1, -1);
            if (dir.x > 0.5f)
                return Vector2.right;
            if (dir.x < -0.5f)
                return Vector2.left;
            if (dir.y > 0.5f)
                return Vector2.up;
            if (dir.y < -0.5f)
                return Vector2.down;
            return Vector2.zero;
        }


        void Update()
        {
            dir = input.move.ReadValue<Vector2>();
            var dirRaw = input.moveRaw;

            if(dir != Vector2.zero)
            {
                lastLookDirection = SnapDirectionTo8Cardinals(dir);
            }

            movement.Walk(dir, speed);

            if(input.resetPosition.WasReleasedThisFrame())
            {
                movement.ResetPosition();
            }

            if (coll.onWall && input.grab.IsPressed() && movement.canMove)
            {
                // if(side != coll.wallSide)
                //     anim.Flip(side*-1);
                movement.GrabWall();
                
            }
            else
            {
                movement.ReleaseWall();
            }


            if (coll.onWall && !coll.onGround && !movement.isWallJumping)
            {
                if (dir.x != 0 && !movement.isWallGrabing)
                {
                    movement.WallSlide(slideSpeed);
                }
            }


            if (input.jump.WasPressedThisFrame())
            {
                // anim.SetTrigger("jump");

                if (coll.onGround && jumped < jumpLimit)
                    Jump();

                if (coll.onWall && !coll.onGround)
                    WallJump();
            }

            if (input.dash.WasPressedThisFrame() && dashed < dashLimit)
            {
                if (dirRaw.x != 0 || dirRaw.y != 0)
                    Dash(input.moveRaw);
            }

            if (input.cast.WasPressedThisFrame())
            {
                spellSource.CastSpell(lastLookDirection);
            }

            if (coll.onGround)
            {
                dashed = 0;
                jumped = 0;

                if (!hasTouchedGround)
                    GroundTouch();
            } else
            {
                hasTouchedGround = false;
            }

            UpdateAnimationState();
        }

        

        // Use this for initialization
        void Start()
        {

        }



        void GroundTouch()
        {
            hasTouchedGround = true;
            // side = anim.sr.flipX ? -1 : 1;
            // jumpParticle.Play();
        }

        public void Dash(Vector2 dir)
        {
            // Camera.main.transform.DOComplete();
            // Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
            // FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));

            // anim.SetTrigger("dash");

            dashed++;
            float immobileTimeSec = 0.3f;
            movement.Push(dir, dashSpeed, immobileTimeSec);
            StartCoroutine(DashWait(immobileTimeSec));
        }

        IEnumerator DashWait(float timeSec)
        {
            // FindObjectOfType<GhostTrail>().ShowGhost();
            StartCoroutine(GroundDash());
            // DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

            // dashParticle.Play();

            yield return new WaitForSeconds(timeSec);

            // dashParticle.Stop();
        }

        IEnumerator GroundDash()
        {
            yield return new WaitForSeconds(.15f);
            if (coll.onGround)
                dashed = 0;
        }

        private void Jump()
        {
            jumped++;
            movement.Jump(Vector2.up, jumpForce, false);
        }
        private void WallJump()
        {
            movement.WallJump(jumpForce);
        }

        private void UpdateAnimationState()
        {
            // Running
            if(dir.x > 0)
            {
                animator.SetBool("Running", true);
                spriteRenderer.flipX = false;
            }
            else if (dir.x < 0) { 
                animator.SetBool("Running", true);
                spriteRenderer.flipX = true;
            }
            else
            {
                animator.SetBool("Running", false);
            }

            // Jumping
            if (movement.isJumping)
            {
                animator.SetBool("Jumping", true);
            }
            else
            {
                animator.SetBool("Jumping", false);
            }

            // Falling
            if (movement.rb.velocity.y < -3)
            {
                animator.SetBool("Falling", true);
            }
            else
            {
                animator.SetBool("Falling", false);
            }

            // Wall Grab
            if (movement.isWallSliding || movement.isWallGrabing)
            {
                animator.SetBool("WallGrab", true);
            }
            else
            {
                animator.SetBool("WallGrab", false);
            }
        }
    }

}
