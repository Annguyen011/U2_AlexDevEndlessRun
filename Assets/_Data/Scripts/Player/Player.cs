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
        private Animator animator;

        #endregion

        #region [Unity methods]

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
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
            animator.SetFloat(moveAnim, rb.velocity.x);
            animator.SetFloat(jumpAnim, rb.velocity.y);

            animator.SetBool(isGroundAnim, isGrounded);
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
