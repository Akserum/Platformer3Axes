using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float movementSpeed = 5f;
    float jumpForce = 5f;
    [SerializeField]
    Animator animator;
    [SerializeField]
    Transform characterSprite;


    private float distanceRaycast = 1f;
    [SerializeField]
    LayerMask groundLayerMask;
    [SerializeField]
    bool isGrounded = true;
    bool canJump = false;
    [SerializeField] Transform checkGrounded;
    bool jumpButtonPressed = false;

    bool dashButtonPressed = false;
    bool canDash = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(horizontalInput * Time.deltaTime * movementSpeed, 0, 0);

        Debug.DrawLine(checkGrounded.transform.position, new Vector3(checkGrounded.transform.position.x, checkGrounded.transform.position.y - distanceRaycast, checkGrounded.transform.position.z), UnityEngine.Color.green);
        isGrounded = Physics2D.Raycast(checkGrounded.transform.position, Vector3.down, distanceRaycast, groundLayerMask);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            jumpButtonPressed = true;
        }
        if (isGrounded && jumpButtonPressed)
        {
            canJump = false;
        }
        animator.SetFloat("horizontalInput", horizontalInput);
        animator.SetFloat("rbVelocityY", rb.velocity.y);
        animator.SetBool("isGrounded", isGrounded);
        if (horizontalInput < 0 || horizontalInput > 0)
        {
            characterSprite.localScale = new Vector3(Mathf.Sign(horizontalInput) * Mathf.Abs(characterSprite.localScale.x), characterSprite.localScale.y, characterSprite.localScale.z);
        }
        if (Physics2D.Raycast(checkGrounded.transform.position, Vector3.down, distanceRaycast, LayerMask.GetMask("Pente")))
        {
            rb.sharedMaterial.friction = 1f;
        }
        else
        {
            rb.sharedMaterial.friction = 0.6f;
        }

    }
    private void FixedUpdate()
    {
        if (jumpButtonPressed)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpButtonPressed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.collider.gameObject.layer == LayerMask.NameToLayer("Pente"))
        {
            canJump = true;
        }
    }
}
