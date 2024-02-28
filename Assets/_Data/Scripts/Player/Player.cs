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
        public bool runBegun;
        [SerializeField] private float moveSpeed;

        [Header("# Jump infos")]
        [SerializeField] private float jumpForce;


        [Header("# Collision check")]
        [SerializeField] private LayerMask whatisground;
        [SerializeField] private float groundCheckDistance;
        private bool isGrounded;

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
            Movement();
            CollisionCheck();

            CheckInput();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
        }

        #endregion

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
            if(!runBegun)
            {
                return;
            }
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }
}
