using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Animator myAnimator;
    private Rigidbody2D rb;

    public float moveSpeed = 5f;
    public float jumpHeight = 5f;
    public bool isGrounded = false;

    private bool facingRight;
    void Start()
    {
        facingRight = true;
        myAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Jump();
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;
        Debug.Log( rb.velocity.y+"");

        Flip(horizontal);

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if (rb.velocity.y == 0)
        {
            myAnimator.SetBool("isJumping", false);
            myAnimator.SetBool("isFalling", false);
            myAnimator.SetBool("isGrounded", true);

        }

        if (rb.velocity.y > 0)
        {
            myAnimator.SetBool("isJumping", true);
            myAnimator.SetBool("isSkipping", false);
            myAnimator.SetBool("isGrounded", false);
        }

        bool isSkip = myAnimator.GetBool("isGrounded");

        if (rb.velocity.y < 0 &&  !isSkip)
        {
            myAnimator.SetBool("isJumping", false);
            myAnimator.SetBool("isFalling", true);
            myAnimator.SetBool("isGrounded", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }


    }

    void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }
}
