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
        [SerializeField] private float moveSpeed;

        [Header("# Jump infos")]
        [SerializeField] private float jumpForce;


        [Header("# Collision check")]

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
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            if(Input.GetButton("Jump"))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        #endregion
    }
}
