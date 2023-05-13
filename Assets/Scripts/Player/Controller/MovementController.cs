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
        private Vector2 lastInput = Vector2.left;
        private SpellSource spellSource;

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
        }


        void Update()
        {
            Vector2 dir = input.move.ReadValue<Vector2>();
            var dirRaw = input.moveRaw;
            //print(dir);

            if(dir != Vector2.zero)
            {
                lastInput = dir;
            }

            movement.Walk(dir, speed);
            // anim.SetHorizontalMovement(x, y, rb.velocity.y);

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
                spellSource.CastSpell(lastInput);
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
            Debug.Log("Wall jumped");
            movement.WallJump(jumpForce);
        }
    }

}
