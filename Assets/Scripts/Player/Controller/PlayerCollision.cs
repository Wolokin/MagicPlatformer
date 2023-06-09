using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{

    public class PlayerCollision : MonoBehaviour
    {

        [Header("Layers")]
        public LayerMask groundLayer;

        [Space]

        public bool onGround;
        public bool onWall;
        public bool onRightWall;
        public bool onLeftWall;
        public int wallSide;

        [Space]

        [Header("Collision")]

        public float collisionRadius = 0.25f;
        public Vector2 bottomOffset, rightOffset, leftOffset;
        private Color debugCollisionColor = Color.red;

        private Rigidbody2D rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (rb.velocity.y <= 0)
            {
                onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
            } else
            {
                onGround = false;
            }

            onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
            onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
            onWall = onLeftWall || onRightWall;

            wallSide = onRightWall ? -1 : 1;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

            Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
            Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
            Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
        }
    }

}
