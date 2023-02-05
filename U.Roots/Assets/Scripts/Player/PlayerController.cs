using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputs _inputs;

    private Rigidbody2D rb;

    [HideInInspector] public float movementInput;
    [HideInInspector] public bool isMoving;
    [Header("Movement")]
    [SerializeField] public float moveSpeed;

    [Header("Attack")]
    private bool canAttack = true;
    [SerializeField] private Collider2D meleeBox;
    [SerializeField] private float attackDelay;
    private float attackTimer;
    
    void OnEnable()
    {
        _inputs.Actions.Enable();
    }
    void OnDisable()
    {
        _inputs.Actions.Disable();
    }
    
    void SubscribeInputEvents()
    {
        _inputs = new PlayerInputs();
        // input events
        _inputs.Actions.HorizontalMovements.performed += OnMovement;
        _inputs.Actions.HorizontalMovements.canceled += OnMovement;
        _inputs.Actions.Attack.performed += OnAttack;
    }

    void AssignComponents()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Awake()
    {
        SubscribeInputEvents();
        AssignComponents();
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + movementInput * transform.right * moveSpeed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > attackDelay)
            {
                canAttack = true;
            }
        }
    }

    void OnMovement(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<float>();
        isMoving = movementInput != 0;
    }

    void OnAttack(InputAction.CallbackContext ctx)
    {
        if (canAttack)
        {
            meleeBox.gameObject.SetActive(true);
            Invoke("TurnOffMeleeBox", .1f);
            canAttack = false;
            attackTimer = 0;
        }
    }

    void TurnOffMeleeBox()
    {
        meleeBox.gameObject.SetActive(false);
    }
}
