using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;

    private enum MovementState { idle, running,jumping,falling }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {

        if (rb.bodyType == RigidbodyType2D.Static)
            return;

        dirX = Input.GetAxisRaw("Horizontal");

        // ✅ 修改为 velocity 而不是 linearVelocity
        rb.linearVelocity = new Vector2(dirX * moveSpeed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (rb.bodyType == RigidbodyType2D.Static)
            return;

        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.linearVelocityY > .4f)
        {
            state = MovementState.jumping;

        }

        else if (rb.linearVelocityY < -.1f)
        {
            state = MovementState.falling;
        } 
        anim.SetInteger("state",(int)state);
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    
}
