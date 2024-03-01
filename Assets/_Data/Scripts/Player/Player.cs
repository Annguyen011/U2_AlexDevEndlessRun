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

        [Header("# Jump infos")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float doubleJumpForce;
        private bool canDoubleJump;

        [Header("# Slide infos")]
        [SerializeField] private float slideSpeed;
        [SerializeField] private float slideTime;
        private float slideTimeCounter;
        private bool isSliding;

        [Header("# Collision check")]
        [SerializeField] private LayerMask whatisground;
        [SerializeField] private float groundCheckDistance;
        [Space]
        [SerializeField] private Transform wallCheck;
        [SerializeField] private Vector2 wallCheckSize;
        private bool isGrounded;
        private bool wallDetected;


        [Header("# Animator")]
        [SerializeField] private string moveAnim;
        [SerializeField] private string jumpAnim;
        [SerializeField] private string slideAnim;
        [SerializeField] private string isGroundAnim;

        // Components
        private Rigidbody2D rb;
        private Animator anim;

        #endregion

        #region [Unity methods]

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        private void Start()
        {

        }

        private void Update()
        {
            AnimatorCtrl();
            Movement();
            CollisionCheck();

            // Slide
            slideTimeCounter -= Time.deltaTime;

            if (isGrounded)
            {
                canDoubleJump = true;
            }

            CheckForSlide();
            CheckInput();
        }



        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x,
                transform.position.y - groundCheckDistance));
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
            //anim.SetBool("canClimb", canClimb);
            //anim.SetBool("isKnocked", isKnocked);

            if (rb.velocity.y < -20)
                anim.SetBool("canRoll", true);
        }

        private void CollisionCheck()
        {
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down,
                groundCheckDistance, whatisground);

            wallDetected = Physics2D.BoxCast(wallCheck.position,
                wallCheckSize, 0, Vector2.zero, 0, whatisground);
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
            if (!playerUnlock && wallDetected)
            {
                return;
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
        #endregion

        #region [Abilities]
        // Slide

        private void SlideButton()
        {
            slideTimeCounter = slideTime;
            isSliding = true;
        }

        private void CheckForSlide()
        {
            if(slideTimeCounter <= 0)
            {
                isSliding = false;
            }
        }

        #endregion
    }
}
