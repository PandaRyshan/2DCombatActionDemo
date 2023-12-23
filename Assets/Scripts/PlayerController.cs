using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Vector2 movement;
    private Rigidbody2D rb;
    private PlayerControls playerControls;
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        AdjustFacingDirection();
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void AdjustFacingDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePosition.x < playerScreenPosition.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        PlayerInput();
    }
}
