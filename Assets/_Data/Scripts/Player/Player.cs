using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U2
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        #region [Abilities]
        [Header("# Move infos")]
        public bool playerUnlock;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float speedMuiltipler;
        [SerializeField] private float minstoneIncreaser;
        private float speedMinstone;
        private float defaultSpeed;
        private float defaultMilestoneIncrease;

        [Space]

        [Header("# Jump infos")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float doubleJumpForce;
        private bool canDoubleJump;

        [Space]

        [Header("# Slide infos")]
        [SerializeField] private float slideSpeed;
        [SerializeField] private float slideTime;
        [SerializeField] private float slideCoolDon;
        private float slideTimeCounter;
        private float slideCoolDonCounter;
        private bool isSliding;

        [Space]

        [Header("# Ledge infos")]
        public bool ledgeDetected;
        [SerializeField] private Vector2 offset1 = new Vector2(-.5f, -1.23f);
        [SerializeField] private Vector2 offset2 = new Vector2(.57f, 1.33f);
        private Vector2 startLedgeOffset;
        private Vector2 endLedgeOffset;
        private bool canGrabLedge = true;
        private bool canClimb;

        [Space]

        [Header("# Knock")]
        [SerializeField] private Vector2 knockbackDir = new Vector2(-12, 7);
        [SerializeField] private int timeForInvincibility = 7;
        private bool isKnocked;
        private bool canBeKnocked = true;

        [Space]

        [Header("# Collision check")]
        public LayerMask whatisground;
        [SerializeField] private float groundCheckDistance;
        [Space]
        [SerializeField] private Transform wallCheck;
        [SerializeField] private Vector2 wallCheckSize;
        [Space]
        [SerializeField] private float ceilingCheckDistance;
        private bool isGrounded;
        private bool wallDetected;
        private bool ceilingDetected;

        [Space]

        [Header("# Animator")]
        [SerializeField] private string moveAnim;
        [SerializeField] private string jumpAnim;
        [SerializeField] private string slideAnim;
        [SerializeField] private string isGroundAnim;

        // Components
        private Rigidbody2D rb;
        private Animator anim;
        private SpriteRenderer sprite;

        #endregion

        #region [Unity methods]

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            speedMinstone = minstoneIncreaser;

            defaultMilestoneIncrease = minstoneIncreaser;
            defaultSpeed = moveSpeed;
        }

        private void Update()
        {
            AnimatorCtrl();
            if (isKnocked)
            {
                return;
            }
            Movement();
            CollisionCheck();

            // TimeCounter
            slideTimeCounter -= Time.deltaTime;
            slideCoolDonCounter -= Time.deltaTime;

            if (isGrounded)
            {
                canDoubleJump = true;
            }

            CheckForLedge();
            CheckForSlide();
            CheckInput();
        }



        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x,
                transform.position.y - groundCheckDistance));

            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x,
                transform.position.y + ceilingCheckDistance));

            Gizmos.DrawWireCube(wallCheck.position, wallCheckSize);
        }

        #endregion

        private void AnimatorCtrl()
        {
            anim.SetFloat("xVelocity", rb.velocity.x);
            anim.SetFloat("yVelocity", rb.velocity.y);

            anim.SetBool("isGrounded", isGrounded);
            anim.SetBool("canDoubleJump", canDoubleJump);
            anim.SetBool("isSliding", isSliding);
            anim.SetBool("canClimb", canClimb);
            anim.SetBool("isKnocked", isKnocked);

            if (rb.velocity.y < -20)
                anim.SetBool("canRoll", true);
        }

        private void CollisionCheck()
        {
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down,
                groundCheckDistance, whatisground);

            wallDetected = Physics2D.BoxCast(wallCheck.position,
                wallCheckSize, 0, Vector2.zero, 0, whatisground);

            ceilingDetected = Physics2D.Raycast(transform.position, Vector2.up,
                ceilingCheckDistance, whatisground);
        }

        private void CheckInput()
        {
            if (Input.GetButtonDown("Jump"))
            {
                JumpButton();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SlideButton();
            }
        }

        #region [Basic move]

        private void JumpButton()
        {
            if (isSliding)
            {
                return;
            }

            if (isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if (canDoubleJump)
            {
                canDoubleJump = false;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        private void Movement()
        {
            SpeedCtrl();

            if (playerUnlock)
            {
                return;
            }

            if (wallDetected)
            {
                SpeedReset();
            }

            if (isSliding)
            {

                rb.velocity = new Vector2(slideSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
        }

        private void SpeedReset()
        {
            moveSpeed = defaultSpeed; ;
            minstoneIncreaser = defaultMilestoneIncrease;
        }

        private void SpeedCtrl()
        {
            if (moveSpeed == maxSpeed)
                return;

            if (transform.position.x > speedMinstone)
            {
                speedMinstone = speedMinstone + minstoneIncreaser;

                moveSpeed *= speedMuiltipler;
                minstoneIncreaser *= speedMuiltipler;

                if (moveSpeed > maxSpeed)
                {
                    moveSpeed = maxSpeed;
                }
            }
        }
        #endregion

        #region [Abilities]
        // Slide

        private void SlideButton()
        {
            if (slideCoolDonCounter > 0)
            {
                return;
            }
            slideCoolDonCounter = slideCoolDon;
            slideTimeCounter = slideTime;
            isSliding = true;
        }

        private void CheckForSlide()
        {
            if (slideTimeCounter <= 0 && !ceilingDetected)
            {
                isSliding = false;
            }
        }

        // Ledge
        private void CheckForLedge()
        {
            if (ledgeDetected && canGrabLedge)
            {
                rb.gravityScale = 0f;

                canGrabLedge = false;

                Vector2 ledgePos = GetComponentInChildren<LedgeDetection>().transform.position;

                startLedgeOffset = ledgePos + offset1;
                endLedgeOffset = ledgePos + offset2;

                canClimb = true;
            }

            if (canClimb)
            {
                transform.position = startLedgeOffset;
            }
        }

        private void LedgeClimbOver()
        {
            canClimb = false;
            transform.position = endLedgeOffset;
            Invoke(nameof(AllowLedgeCrab), .1f);
        }

        private void AllowLedgeCrab()
        {
            canGrabLedge = true;
        }

        // Knock back
        private void Knockback()
        {
            if (!canBeKnocked)
            {
                return;
            }
            StartCoroutine(Invicibility());
            isKnocked = true;
            rb.velocity = knockbackDir;
        }

        private void CancelKnockback()
        {
            isKnocked = false;
        }

        // Invicibility
        private IEnumerator Invicibility()
        {
            Color originalColor = sprite.color;
            Color newColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, .5f);

            canBeKnocked = false;

            for (int i = 0; i < timeForInvincibility; i++)
            {
                sprite.color = newColor;

                yield return new WaitForSeconds(.1f);

                sprite.color = originalColor;
            }

            canBeKnocked = true;
        }

        #endregion
    }
}
