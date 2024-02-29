using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U2
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent (typeof(BoxCollider2D))]
    [RequireComponent (typeof(Animator))]
    public class Player : MonoBehaviour
    {
        #region [Abilities]
        [Header("# Move infos")]
        public bool playerUnlock;
        [SerializeField] private float moveSpeed;

        [Header("# Jump infos")]
        [SerializeField] private float jumpForce;


        [Header("# Collision check")]
        [SerializeField] private LayerMask whatisground;
        [SerializeField] private float groundCheckDistance;
        private bool isGrounded;

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

            CheckInput();
        }

       

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
        }

        #endregion

        private void AnimatorCtrl()
        {
            anim.SetFloat("xVelocity", rb.velocity.x);
            anim.SetFloat("yVelocity", rb.velocity.y);

            anim.SetBool("isGrounded", isGrounded);
            //anim.SetBool("canDoubleJump", canDoubleJump);
            //anim.SetBool("isSliding", isSliding);
            //anim.SetBool("canClimb", canClimb);
            //anim.SetBool("isKnocked", isKnocked);

            if (rb.velocity.y < -20)
                anim.SetBool("canRoll", true);
        }

        private void CollisionCheck()
        {
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatisground);
        }

        private void CheckInput()
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        private void Movement()
        {
            if(!playerUnlock)
            {
                return;
            }
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }
}
