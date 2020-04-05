using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Animator myAnimator;

    public float moveSpeed = 5f;
    public float jumpHeight = 5f;
    public bool isGrounded = false;

    private bool facingRight;
    void Start()
    {
        facingRight = true;
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Jump();
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;

        Flip(horizontal);

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
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
